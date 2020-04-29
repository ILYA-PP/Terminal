using System;
using System.Configuration;
using System.Threading;

namespace PrintChequeService
{
    class Program
    {
        private static FiscalRegistrar fR;
        static void Main(string[] args)
        {
            var driverData = ConfigurationManager.AppSettings;
            try
            {
                int interval = int.Parse(driverData["Interval"]);
                TimerCallback tm = new TimerCallback(Method);
                Timer timer = new Timer(tm, null, 0, interval);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            Console.ReadKey();
        }

        private static void Method(object obj)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
                var cheque = ChequeFromWebService.GetCheque();
                if(cheque.Count > 0)
                {
                    fR = new FiscalRegistrar();
                    while(/*fR.CheckConnect()*/0 != 0)
                    {
                        Console.WriteLine("Ожидание подключения...");
                        fR.Connect();
                        Thread.Sleep(1000);
                    }
                    foreach (var c in cheque)
                    {
                        while (!fR.ChequeIsPrinted)
                        {
                            Console.WriteLine("Печать чека");
                            fR.PrintCheque(c);
                            Thread.Sleep(1000);
                        }
                        fR.ChequeIsPrinted = false;
                    }
                }
                else
                    Console.WriteLine("Ожидание чека...");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
