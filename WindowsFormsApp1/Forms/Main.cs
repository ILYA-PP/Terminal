using System;
using System.Windows.Forms;
using DrvFRLib;


namespace TerminalApp
{
    public partial class Main : Form
    {
        private PinPad pinPad;
        private FR fR;
        public Main()
        {
            InitializeComponent();
            fR = new FR(logLB);
            //pinPad = new PinPad();
            //if (!pinPad.IsEnabled())
            //{
            //    MessageBox.Show("Пинпад НЕ подключен!");
            //    this.Close();
            //}
        }
        private void payB_Click(object sender, EventArgs e)
        {
            try
            {
                if (sumTB.Text != "")
                {
                    double sum = Math.Round(double.Parse(sumTB.Text), 2);
                    MessageBox.Show(pinPad.Purchase(sum));
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
            try
            {
                MessageBox.Show(pinPad.CloseDay());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //pinPad.Cancel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chequeBodyTB.Text == "")
                fR.PrintCheque("Тело чека");
            else
                fR.PrintCheque(chequeBodyTB.Text);
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

        private void addProductB_Click(object sender, EventArgs e)
        {
            new Products().ShowDialog();
        }
    }
}
