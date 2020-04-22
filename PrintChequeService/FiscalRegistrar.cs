using DrvFRLib;
using System.Configuration.Assemblies;
using System.Configuration;
using System.IO;
using System;

namespace PrintChequeService
{
    class FiscalRegistrar
    {
        private DrvFR Driver { get; set; }

        public FiscalRegistrar()
        {
            Connect();
        }
        //подключение к фискальному регистратору
        private void Connect()
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
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
            }
            CheckConnect();
        }
        //проверка соединения
        public void CheckConnect()
        {
            AddLog("Подключение:");
            executeAndHandleError(Driver.Connect);
        }
        //вывод, возвращаемых фискальником сообщений, в поле формы
        private void AddLog(string mes)
        {
            Console.WriteLine(mes);
        }

        private void CheckResult(int code, string n)
        {
            if (code != 0)
                Console.WriteLine($"Метод: {n}\nОшибка: {Driver.ResultCodeDescription}\nКод: {code}\n");
            else
                Console.WriteLine($"Метод {n}: Успешно\n");
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
                double result = 0;
                AddLog("Печать заголовка: ");
                executeAndHandleError(Driver.PrintDocumentTitle);
                AddLog("Открытие чека: ");
                executeAndHandleError(Driver.OpenCheck);
                Driver.CheckType = 1;
                foreach (Product p in cheque.Products)
                {
                    //add product
                    Driver.StringForPrinting = p.Name;
                    Driver.Price = (decimal)p.Price;
                    Driver.Quantity = p.Quantity;
                    Driver.Summ1Enabled = true;
                    Driver.Summ1 = (decimal)p.Row_Summ;
                    result += p.Row_Summ;
                    Driver.TaxValue1Enabled = false;
                    Driver.PaymentTypeSign = 4;
                    Driver.PaymentItemSign = p.Row_Type;
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
                AddLog("Закрытие чека: ");
                executeAndHandleError(Driver.FNCloseCheckEx);

                AddLog("Отрезка чека: ");
                executeAndHandleError(Driver.CutCheck);
            }
            else
                AddLog("Нет подключения");
        }
    }
}
