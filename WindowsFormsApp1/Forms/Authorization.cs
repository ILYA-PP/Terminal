using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TerminalApp.Models;

namespace TerminalApp.Forms
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void logInB_Click(object sender, EventArgs e)
        {
            if (Authorize.LogIn(loginTB.Text, passwordTB.Text))
            {
                new Main().Show();
                Hide();
            }
        }
    }
}
