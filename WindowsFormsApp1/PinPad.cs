using SBRFSRV;
using System;

namespace TerminalApp
{
    class PinPad
    {
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
