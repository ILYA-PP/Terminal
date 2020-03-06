namespace TerminalApp
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.payB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sumTB = new System.Windows.Forms.TextBox();
            this.closeDayB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // payB
            // 
            this.payB.Location = new System.Drawing.Point(77, 67);
            this.payB.Name = "payB";
            this.payB.Size = new System.Drawing.Size(75, 23);
            this.payB.TabIndex = 0;
            this.payB.Text = "Оплатить";
            this.payB.UseVisualStyleBackColor = true;
            this.payB.Click += new System.EventHandler(this.payB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сумма:";
            // 
            // sumTB
            // 
            this.sumTB.Location = new System.Drawing.Point(12, 25);
            this.sumTB.Name = "sumTB";
            this.sumTB.Size = new System.Drawing.Size(209, 20);
            this.sumTB.TabIndex = 2;
            // 
            // closeDayB
            // 
            this.closeDayB.Location = new System.Drawing.Point(119, 178);
            this.closeDayB.Name = "closeDayB";
            this.closeDayB.Size = new System.Drawing.Size(114, 23);
            this.closeDayB.TabIndex = 3;
            this.closeDayB.Text = "Закрыть день";
            this.closeDayB.UseVisualStyleBackColor = true;
            this.closeDayB.Click += new System.EventHandler(this.closeDayB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 213);
            this.Controls.Add(this.closeDayB);
            this.Controls.Add(this.sumTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.payB);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button payB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sumTB;
        private System.Windows.Forms.Button closeDayB;
    }
}

