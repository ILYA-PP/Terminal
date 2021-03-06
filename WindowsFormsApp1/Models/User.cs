﻿using System;
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
        public string Role { get; set; }
        public User(string l, string p, string r)
        {
            Login = l;
            Password = p;
            Role = r;
        }
        public User() { }
    }
}
