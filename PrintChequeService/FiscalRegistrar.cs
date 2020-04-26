using DrvFRLib;
using System.Configuration.Assemblies;
using System.Configuration;
using System.IO;
using System;

namespace PrintChequeService
{
    class FiscalRegistrar
    {
        public bool ChequeIsPrinted { get; set; }
        private DrvFR Driver { get; set; }

        public FiscalRegistrar()
        {
            Connect();
        }

        private void prepareCheque()
        {
            Driver.Password = 30;
            executeAndHandleError(Driver.WaitForPrinting);
            executeAndHandleError(Driver.GetECRStatus);
            switch (Driver.ECRMode)
            {
                case 3:
                    AddLog("Снятие Z-отчёта: ");
                    executeAndHandleError(Driver.PrintReportWithCleaning);
                    executeAndHandleError(Driver.WaitForPrinting);
                    AddLog("Открытие смены: ");
                    executeAndHandleError(Driver.OpenSession); break;
                case 4:
                    AddLog("Открытие смены: ");
                    executeAndHandleError(Driver.OpenSession); break;
                case 8:
                    AddLog("Отмена чека: ");
                    executeAndHandleError(Driver.SysAdminCancelCheck);
                    break;
            }
            executeAndHandleError(Driver.WaitForPrinting);
        }

        public int CheckConnect()
        {
            return Driver.Connect();
        }
        //подключение к фискальному регистратору
        public void Connect()
        {
            var driverData = ConfigurationManager.AppSettings;
            try
            {
                Driver = new DrvFR()
                {
                    ConnectionType = int.Parse(driverData["ConnectionType"]),
                    ProtocolType = int.Parse(driverData["ProtocolType"]),
                    IPAddress = driverData["IPAddress"],
                    UseIPAddress = bool.Parse(driverData["UseIPAddress"]),
                    TCPPort = int.Parse(driverData["TCPPort"]),
                    Timeout = int.Parse(driverData["Timeout"]),
                    Password = int.Parse(driverData["Password"])
                };
                AddLog($"Подключение к фискальному регистратору (IP = {Driver.IPAddress}): ");
                executeAndHandleError(Driver.Connect);
                ChequeIsPrinted = false;
                //if(CheckConnect() == 0)
                //    executeAndHandleError(Driver.FNCloseSession);
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
            }
        }
        //вывод, возвращаемых фискальником сообщений, в поле формы
        private void AddLog(string mes)
        {
            Console.WriteLine(mes);
        }

        private void CheckResult(int code, string n)
        {
            if (code != 0)
                Console.WriteLine($"Метод: {n} Ошибка: {Driver.ResultCodeDescription} Код: {code}");
            else
                Console.WriteLine($"Метод {n}: Успешно");
            Console.WriteLine($"Статус: {Driver.ECRModeDescription}");
        }

        private delegate int Func();
        private void executeAndHandleError(Func f)
        {
            while (true)
            {
                var ret = f();
                switch (ret)
                {
                    case 0x50: 
                        continue;
                    default: 
                        CheckResult(ret, f.Method.Name);
                        return;
                }
            }
        }
        //печать чека
        public void PrintCheque(Cheque cheque)
        {
            if (Driver.Connect() == 0)
            {
                prepareCheque();
                int state = Driver.GetECRStatus();
                if (state == 2 || state == 4 || state == 7 || state == 9)
                {
                    double result = 0;
                    AddLog("Печать заголовка: ");
                    executeAndHandleError(Driver.PrintDocumentTitle);
                    Driver.CheckType = 1;
                    AddLog("Открытие чека: ");
                    executeAndHandleError(Driver.OpenCheck);
                    foreach (Product p in cheque.Products)
                    {
                        //add product
                        Driver.CheckType = 1;
                        Driver.StringForPrinting = p.Name;
                        Driver.Price = (decimal)p.Price;
                        Driver.Quantity = p.Quantity;
                        Driver.Summ1Enabled = true;
                        Driver.Summ1 = (decimal)p.Row_Summ;
                        result += p.Row_Summ;
                        Driver.TaxValue1Enabled = false;
                        Driver.PaymentTypeSign = 4;
                        if (p.Row_Type == 1)
                            Driver.PaymentItemSign = 1;
                        else if (p.Row_Type == 2)
                            Driver.PaymentItemSign = 4;
                        Driver.TaxValue = (decimal)p.NDS_Summ;
                        if (p.NDS == 18)
                        {
                            Driver.Tax1 = 1;
                            Driver.Tax2 = 0;
                        }
                        else if (p.NDS == 10)
                        {
                            Driver.Tax2 = 2;
                            Driver.Tax1 = 0;
                        }
                        AddLog("Фиксация операции: ");
                        executeAndHandleError(Driver.FNOperation);
                    }
                    if (cheque.Payment == 1)
                    {
                        Driver.Summ1 = (decimal)result;
                        Driver.Summ2 = 0;
                    }
                    else if (cheque.Payment == 2)
                    {
                        Driver.Summ2 = (decimal)result;
                        Driver.Summ1 = 0;
                    }
                    Driver.Summ3 = 0;
                    Driver.Summ4 = 0;
                    Driver.Summ5 = 0;
                    Driver.Summ6 = 0;
                    Driver.Summ7 = 0;
                    Driver.Summ8 = 0;
                    Driver.Summ9 = 0;
                    Driver.Summ10 = 0;
                    Driver.Summ11 = 0;
                    Driver.Summ12 = 0;
                    Driver.Summ13 = 0;
                    Driver.Summ14 = 0;
                    Driver.Summ15 = 0;
                    Driver.Summ16 = 0;
                    Driver.TaxValue3 = 0;
                    Driver.TaxValue4 = 0;
                    Driver.TaxValue5 = 0;
                    Driver.TaxValue6 = 0;
                    Driver.TaxType = 1;
                    AddLog("Закрытие чека: ");
                    executeAndHandleError(Driver.FNCloseCheckEx);
                    AddLog("Отрезка чека: ");
                    executeAndHandleError(Driver.CutCheck);
                    ChequeIsPrinted = true;
                }
                else
                {
                    Console.WriteLine($"ККМ в режиме {state}. Печать не доступна");
                    ChequeIsPrinted = false;
                }
            }
            else
                AddLog("Нет подключения");
        }

        private void GetQRCode()
        {
            Driver.BarcodeType = 3;
            Driver.BarCode = $"t={DateTime.Now}&s={Driver.Summ1}&" +
                $"fn={Driver.SerialNumber}&i={Driver.DocumentNumber}&fp={Driver.FiscalSignAsString}&n={Driver.TaxType}";
            Driver.BarCode = $@"http://check.egais.ru?id=000dede3-2553-4666a70a9e501bbe64df&dt=0612151654&cn=020000111111111";
            AddLog($"Строка для QR-кода: {Driver.BarCode}");
            Driver.BarcodeStartBlockNumber = 0;
            Driver.BarcodeParameter1 = 0;
            Driver.BarcodeParameter2 = 0;
            Driver.BarcodeParameter3 = 5;
            Driver.BarcodeParameter4 = 0;
            Driver.BarcodeParameter5 = 0;
            Driver.BarcodeAlignment = TBarcodeAlignment.baCenter;
            AddLog("Загрузка и печать QR-кода: ");
            executeAndHandleError(Driver.LoadAndPrint2DBarcode);
        }
    }
}
