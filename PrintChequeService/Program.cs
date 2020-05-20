using System;
using System.Configuration;
using System.Threading;

namespace PrintChequeService
{
    class Program
    {
        private static FiscalRegistrar fR = new FiscalRegistrar();
        private static object locker = new object();
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
                lock (locker) 
                {
                    if (cheque.Count > 0)
                    {
                        while (fR.CheckConnect() != 0)
                        {
                            Console.WriteLine("Ожидание подключения...");
                            fR.Connect();
                            Thread.Sleep(1000);
                        }
                        foreach (var c in cheque)
                        {
                            Console.WriteLine("Печать чека");
                            fR.PrintCheque(c);
                        }
                    }
                    else
                        Console.WriteLine("Ожидание чека...");
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
