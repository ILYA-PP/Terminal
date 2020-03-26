using System;
using System.Collections.Generic;
using System.Text;

namespace TerminalApp.Models
{
    class User
    {
        public static string CurrentLogin { get; set; }
        public static string CurrentPassword { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public User(string l, string p)
        {
            Login = l;
            Password = p;
        }
        public User() { }
    }
}
