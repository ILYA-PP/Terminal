using SBRFSRV;
using System;
using DrvFRLib;
using System.Windows.Forms;

namespace TerminalApp
{
    class PinPad
    {
        private DrvFR Driver { get; set; }
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
            Driver = new DrvFR();
            Driver.Password = 30;
            if (Server == null)
                Server = new Server();
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
        public void Purchase(double sum)
        {
            try
            {
                Server.Clear();
                Server.SParam("Amount", sum * 100);
                int result = Server.NFun((int)Operations.Purchase);
                if (result == 0)
                    MessageBox.Show("Успешно!");
                else
                    MessageBox.Show($"Операция НЕ выполнена. Код ошибки: {result}");
            }            
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void Cancel()
        {
            try
            { 
                Server.Clear();
                int result = Server.NFun((int)Operations.Cancel);
                if (result != 0)
                    MessageBox.Show($"Операция НЕ отменена. Код ошибки: {result}");
                else
                    MessageBox.Show("Операция отменена");
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void Return()
        {
            try
            {
                Server.Clear();
                int result = Server.NFun((int)Operations.Return);
                if (result != 0)
                    MessageBox.Show($"Средства НЕ возвращены. Код ошибки: {result}");
                else
                    MessageBox.Show("Средства возвращены");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void CloseDay()
        {
            try
            {
                Server.Clear();
                int result = Server.NFun((int)Operations.Total);
                if (result != 0)
                    MessageBox.Show($"День НЕ закрыт. Код ошибки: {result}");
                else
                    MessageBox.Show("День закрыт");
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
