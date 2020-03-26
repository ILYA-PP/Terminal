using DrvFRLib;
using System.Windows.Forms;
using TerminalApp.Models;

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
        private void Connect()
        {
            Driver = new DrvFR();
            AddLog();
            Driver.ConnectionType = 6;
            Driver.ProtocolType = 0;
            Driver.IPAddress = "192.168.137.111";
            Driver.UseIPAddress = true;
            Driver.TCPPort = 7778;
            Driver.Timeout = 1000;
            Driver.Password = 30;
            CheckConnect();
        }
        private void CheckConnect()
        {
            if (Driver.Connect() != 0)
                AddLog();
        }
        public void Beep()
        {
            if (Driver.Connect() == 0)
                Driver.Beep();
            AddLog();
        }
        private void AddLog()
        {
            if(List != null)
                List.Items.Add($"{Driver.ResultCode}: {Driver.ResultCodeDescription}");
        }

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

        public void PrintZReport()
        {
            Driver.PrintReportWithCleaning();
            AddLog();
        }
        public void PrintXReport()
        {
            Driver.PrintReportWithoutCleaning();
            AddLog();
        }
        public void OpenProperties()
        {
            Driver.ShowProperties();
            AddLog();
        }
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

        public int GetTableRowCount(int n)
        {
            Driver.TableNumber = n;
            Driver.GetTableStruct();
            return Driver.RowNumber;
        }
    }
}
