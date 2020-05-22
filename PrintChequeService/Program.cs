using System;
using System.Configuration;
using System.Threading;

namespace PrintChequeService
{
    class Program
    {
        //класс для работы с фискальным регистратором
        private static FiscalRegistrar fR = new FiscalRegistrar();
        //объект для блокировки потока
        private static object locker = new object();
        static void Main(string[] args)
        {
            var driverData = ConfigurationManager.AppSettings;
            try
            {
                //интервал между запросами на получение чеков
                int interval = int.Parse(driverData["Interval"]);
                TimerCallback tm = new TimerCallback(Method);
                Timer timer = new Timer(tm, null, 0, interval);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Console.ReadKey();
        }

        private static void Method(object obj)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
                var cheque = ChequeFromWebService.GetCheque(); //получение чеков с сервера
                lock (locker) //блокировка кода для синхронизации потоков
                {
                    if (cheque != null && cheque.Count > 0)
                    {
                        while (fR.CheckConnect() != 0)
                        {
                            //подключение к ККТ
                            Console.WriteLine("Ожидание подключения...");
                            fR.Connect();
                            Thread.Sleep(1000);
                        }
                        foreach (var c in cheque)
                        {
                            Console.WriteLine("Печать чека");
                            fR.PrintCheque(c);
                        }
                        fR.Disconnect(); //отключение от ККТ
                    }
                    else
                        Console.WriteLine("Ожидание чека...");
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
