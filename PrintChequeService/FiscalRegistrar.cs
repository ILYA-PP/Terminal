using DrvFRLib;
using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

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
                Driver.WaitForPrintingDelay = 20;
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
            }
        }
        //вывод, возвращаемых фискальником сообщений, в поле формы
        private void AddLog(string mes)
        {
            Console.Write(mes + " ");
        }

        private void CheckResult(int code, string n)
        {
            if (code != 0)
                Console.Write($"Метод: {n} Ошибка: {Driver.ResultCodeDescription} Код: {code} ");
            else
                Console.Write($"Метод {n}: Успешно ");
            Console.WriteLine($"Статус: {Driver.ECRModeDescription}");
        }

        private delegate int Func();
        private int executeAndHandleError(Func f)
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
                        return ret;
                }
            }
        }
        //печать чека
        public async void PrintChequeAsync(Cheque cheque)
        {
            if (CheckConnect() == 0)
            {
                prepareCheque();
                Driver.GetECRStatus();
                int state = Driver.ECRMode;
                if (state == 2 || state == 4 || state == 7 || state == 9)
                {
                    double result = 0;
                    Driver.CheckType = 0;
                    AddLog("Открытие чека: ");
                    executeAndHandleError(Driver.OpenCheck);
                    Driver.Password = 30;
                    if (cheque.Phone != null)
                        Driver.CustomerEmail = cheque.Phone;
                    else if (cheque.Email != null)
                        Driver.CustomerEmail = cheque.Email;
                    AddLog("Передача данных покупателя: ");
                    executeAndHandleError(Driver.FNSendCustomerEmail);
                    foreach (Product p in cheque.Products)
                    {
                        //add product
                        Driver.CheckType = 1;
                        Driver.StringForPrinting = p.Name;
                        Driver.Price = (decimal)p.Price;
                        Driver.Quantity = p.Quantity;
                        Driver.Summ1Enabled = false;
                        //Driver.Summ1 = (decimal)p.Row_Summ;
                        result += p.Row_Summ;
                        Driver.TaxValueEnabled = false;
                        Driver.Department = 1;
                        Driver.PaymentTypeSign = 4;
                        if (p.Row_Type == 1)
                            Driver.PaymentItemSign = 1;
                        else if (p.Row_Type == 2)
                            Driver.PaymentItemSign = 4;
                        //Driver.TaxValue = (decimal)p.NDS_Summ;
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
                        AddLog($"Фиксация операции: Товар: {p.Name} Количество: {p.Quantity} Сумма: {p.Row_Summ}");
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
                    if (executeAndHandleError(Driver.FNCloseCheckEx) == 0)
                    {
                        Thread t = new Thread(new ParameterizedThreadStart(ChequeFromWebService.ChequePrinted));
                        t.Start(cheque.ID);
                    }
                    AddLog("Ожидание печати чека: ");
                    executeAndHandleError(Driver.WaitForPrinting);
                    AddLog("Отрезка чека: ");
                    executeAndHandleError(Driver.CutCheck);
                    ChequeIsPrinted = true;
                }
                else
                    Console.WriteLine($"ККМ в режиме {state}. Печать не доступна");
            }
            else
                AddLog("Нет подключения");
        }
    }
}
