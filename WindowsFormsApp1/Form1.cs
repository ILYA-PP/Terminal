using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private PinPad pinPad;
        public Form1()
        {
            InitializeComponent();
            pinPad = new PinPad();
            if (!pinPad.IsEnabled())
            {
                MessageBox.Show("Пинпад НЕ подключен!");
                Close();
            }
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
    }
}
