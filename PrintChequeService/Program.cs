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
                fR = new FiscalRegistrar();
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
                    foreach (var c in cheque)
                    {
                        Console.WriteLine("Печать чека");
                        fR.PrintCheque(c);
                    }
                else
                    Console.WriteLine("Ожидание чека...");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            Thread.CurrentThread.Join();
        }
    }
}
