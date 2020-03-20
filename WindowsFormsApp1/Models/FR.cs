using DrvFRLib;
using System.Windows.Forms;

namespace TerminalApp
{
    class FR
    {
        private DrvFR Driver;
        private ListBox List;
        public FR(ListBox l)
        {
            List = l;
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
        public FR()
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
    }
}
