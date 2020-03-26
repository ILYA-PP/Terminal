using System.Windows.Forms;

namespace TerminalApp.Models
{
    class Authorize
    {
        public static bool LogIn(string login, string password)
        {
            FiscalRegistrar fr = new FiscalRegistrar();
            User user = null;
            for(int i = 0; i < fr.GetTableRowCount(2); i++)
            {
                user = fr.GetUser(i);
                if (login == user.Login && password == user.Password)
                    break;
                else
                    user = null;
            }
            if (user != null)
            {
                MessageBox.Show("Вход выполнен!");
                User.CurrentLogin = user.Login;
                User.CurrentPassword = user.Password;
                return true;
            }
            else
                MessageBox.Show("Неверный логин или пароль!");
            return false;
        }
    }
}
