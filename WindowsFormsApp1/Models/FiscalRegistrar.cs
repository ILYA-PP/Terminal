using DrvFRLib;
using System.Windows.Forms;
using TerminalApp.Models;
using System.Configuration.Assemblies;
using System.Configuration;
using System.IO;
using System;

namespace TerminalApp
{
    class FiscalRegistrar
    {
        private DrvFR Driver { get; set; }
        private ListBox List { get; set; }
        public FiscalRegistrar(ListBox l)
        {
            List = l;
            Connect();
        }
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
            if(List != null)
                List.Items.Add($"{Driver.ResultCode}: {Driver.ResultCodeDescription}");
        }
        //печать чека
        public void PrintCheque(string cheque)
        {
            Driver.StringForPrinting = cheque;
            Driver.PrintAttribute();
            Driver.PrintCliche();
            Driver.PrintDocumentTitle();
            Driver.PrintString();
            Driver.PrintTrailer();
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
                    File.WriteAllText(Application.StartupPath + $"\\Z-отчёты\\Z-отчёт {DateTime.Now.ToShortDateString()}.txt", Driver.StringForPrinting);
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
        public User GetUser(int row)
        {
            User user = new User();
            Driver.TableNumber = 2;
            Driver.GetFieldStruct();
            AddLog();
            Driver.RowNumber = row;
            Driver.FieldNumber = 1;
            Driver.ReadTable();
            user.Login = Driver.ValueOfFieldString;
            AddLog();
            Driver.RowNumber = row;
            Driver.FieldNumber = 2;
            Driver.ReadTable();
            user.Password = Driver.ValueOfFieldString;
            AddLog();
            return user;
        }
        //количество строк в таблице
        public int GetTableRowCount(int n)
        {
            Driver.TableNumber = n;
            Driver.GetTableStruct();
            return Driver.RowNumber;
        }
    }
}
