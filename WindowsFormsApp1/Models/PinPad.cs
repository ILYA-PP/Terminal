using SBRFSRV;
using System;
using DrvFRLib;

namespace TerminalApp
{
    class PinPad
    {
        //private DrvFR Driver;
        public static Server Server { get; set; }
        private enum Operations
        {
            Purchase = 4000, // Покупка
            PinPadEnabled = 13, //статус пинпада
            Return = 4002, // Возврат покупки
            Cancel = 4003, // Отмена операции
            Total = 6000 // Итоги дня 
        }
        public PinPad()
        {
            //Driver = new DrvFR();
            //Driver.Password = 30;
            try
            {
                if (Server == null)
                    Server = new Server();
            }
            catch (Exception ex) { }
        }
        public string GetCheque()
        {
            try
            { 
                string cheque = Server.GParamString("Cheque");
                Print(cheque);
                return cheque;
            }
            catch (Exception ex) { return ex.Message; }
        }
        public bool IsEnabled()
        {
            try
            { 
                if (Server.NFun(13) == 0)
                    return true;
            }
            catch{ }
            return false;
        }

        public void Print(string cheque)
        {
            DrvFR Driver = new DrvFR();
            Driver.ConnectionType = 6;
            Driver.ProtocolType = 0;
            Driver.IPAddress = "192.168.137.111";
            Driver.UseIPAddress = true;

            Driver.TCPPort = 7778;
            Driver.Timeout = 1000;
            Driver.Password = 30;
            Driver.StringForPrinting = cheque;
            Driver.PrintString();
        }
        public string Purchase(double sum)
        {
            try
            {
                Server.Clear();
                Server.SParam("Amount", sum * 100);
                int result = Server.NFun((int)Operations.Purchase);
                if (result == 0)
                    return "Успешно!";
                else
                    return $"Операция НЕ выполнена. Код ошибки: {result}";
            }            
            catch(Exception ex) { return ex.Message; }
        }
        public string Cancel()
        {
            try
            { 
                Server.Clear();
                int result = Server.NFun((int)Operations.Cancel);
                if (result != 0)
                    return $"Операция НЕ отменена. Код ошибки: {result}";
                return "Операция отменена";
            }
            catch(Exception ex) { return ex.Message; }
        }
        public string Return()
        {
            try
            {
                Server.Clear();
                int result = Server.NFun((int)Operations.Return);
                if (result != 0)
                    return $"Средства НЕ возвращены. Код ошибки: {result}";
                return "Средства возвращены";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string CloseDay()
        {
            try
            {
                Server.Clear();
                int result = Server.NFun((int)Operations.Total);
                if (result != 0)
                    return $"День НЕ закрыт. Код ошибки: {result}";
                return "День закрыт";
            }
            catch(Exception ex) { return ex.Message; }
        }
    }
}
