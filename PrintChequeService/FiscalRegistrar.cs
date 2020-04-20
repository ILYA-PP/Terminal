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
            AddLog();
            CheckConnect();
        }
        //проверка соединения
        public void CheckConnect()
        {
            if (Driver.Connect() != 0)
                AddLog();
        }
        //подать звуковой сигнал
        public void Beep()
        {
            if (Driver.Connect() == 0)
                Driver.Beep();
            AddLog();
        }
        //вывод, возвращаемых фискальником сообщений, в поле формы
        private void AddLog()
        {
            //сделать логгирование
        }
        //печать чека
        public void PrintCheque(Cheque cheque)
        {
            //добавить печать qr кода, рекламы и прочего
            double result = 0;
            Driver.PrintDocumentTitle();
            Driver.CheckType = 1;
            foreach(Product p in cheque.Products)
            {
                //add product!!!!
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
                if (Driver.FNOperation() != 0)
                    AddLog();
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
            if (Driver.FNCloseCheckEx() != 0)
                AddLog();
            Driver.FNPrintDocument();
            Driver.CutCheck();
            AddLog();
        }
        //печать отчетов
        public void PrintZReport()
        {
            //operatornumber
            if(Driver.PrintZReportInBuffer() == 0)
            {
                Driver.PrintZReportFromBuffer();
                AddLog();
                if (Driver.ReadReportBufferLine() == 0)
                    File.WriteAllText(Environment.CurrentDirectory + $"\\Z-отчёты\\Z-отчёт {DateTime.Now.ToShortDateString()}.txt", Driver.StringForPrinting);
                else
                    AddLog();
            }
            else
                AddLog();
            Driver.PrintReportWithCleaning();
            AddLog();
        }
        public void PrintXReport()
        {
            Driver.PrintReportWithoutCleaning();
            AddLog();
        }
        //показать свойства
        public void OpenProperties()
        {
            Driver.ShowProperties();
            AddLog();
        }
        //открытие/закрытие смены
        public void OpenSession()
        {
            Driver.FNOpenSession();
            AddLog();
        }
        public void CloseSession()
        {
            Driver.FNCloseSession();
            AddLog();
        }
        //получить пользователя из регистра
        //по номеру строки
        //количество строк в таблице
        public int GetTableRowCount(int n)
        {
            Driver.TableNumber = n;
            Driver.GetTableStruct();
            return Driver.RowNumber;
        }
    }
}
