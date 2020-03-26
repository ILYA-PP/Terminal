using System;
using System.Windows.Forms;

namespace TerminalApp
{
    public partial class Main : Form
    {
        private PinPad pinPad;
        private FiscalRegistrar fR;
        public Main()
        {
            InitializeComponent();
            try
            {
                fR = new FiscalRegistrar(logLB);
                pinPad = PinPad.GetServer();
                if (!pinPad.IsEnabled())
                {
                    MessageBox.Show("Пинпад НЕ подключен!");
                    //this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void payB_Click(object sender, EventArgs e)
        {
            try
            {
                if (sumTB.Text != "")
                {
                    double sum = Math.Round(double.Parse(sumTB.Text), 2);
                    pinPad.Purchase(sum);
                    if(pinPad.GetCheque() != null)
                        if (pinPad.GetCheque() != null)
                        MessageBox.Show(pinPad.GetCheque(), "Чек");
                }
                else
                    MessageBox.Show("Введите сумму!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private void closeDayB_Click(object sender, EventArgs e)
        {
            pinPad.CloseDay();
        }
        private void printXReportB_Click(object sender, EventArgs e)
        {
            fR.PrintXReport();
        }
        private void printZReportB_Click(object sender, EventArgs e)
        {
            fR.PrintZReport();
        }

        private void propertiesB_Click(object sender, EventArgs e)
        {
            fR.OpenProperties();
        }

        private void closeSessionB_Click(object sender, EventArgs e)
        {
            fR.CloseSession();
        }

        private void openSessionB_Click(object sender, EventArgs e)
        {
            fR.OpenSession();
        }

        private void beepB_Click(object sender, EventArgs e)
        {
            fR.Beep();
        }

        private void printChequeB_Click(object sender, EventArgs e)
        {
            if (chequeBodyTB.Text == "")
                fR.PrintCheque("Тело чека");
            else
                fR.PrintCheque(chequeBodyTB.Text);
        }
    }
}
