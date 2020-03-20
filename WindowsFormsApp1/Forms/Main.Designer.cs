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
            this.printChequeB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.logLB = new System.Windows.Forms.ListBox();
            this.printXReportB = new System.Windows.Forms.Button();
            this.printZReportB = new System.Windows.Forms.Button();
            this.chequeBodyTB = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.propertiesB = new System.Windows.Forms.Button();
            this.openSessionB = new System.Windows.Forms.Button();
            this.closeSessionB = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьТоварToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beepB = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // payB
            // 
            this.payB.Location = new System.Drawing.Point(12, 78);
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
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сумма:";
            // 
            // sumTB
            // 
            this.sumTB.Location = new System.Drawing.Point(12, 52);
            this.sumTB.Name = "sumTB";
            this.sumTB.Size = new System.Drawing.Size(209, 20);
            this.sumTB.TabIndex = 2;
            // 
            // closeDayB
            // 
            this.closeDayB.Location = new System.Drawing.Point(107, 78);
            this.closeDayB.Name = "closeDayB";
            this.closeDayB.Size = new System.Drawing.Size(114, 23);
            this.closeDayB.TabIndex = 3;
            this.closeDayB.Text = "Закрыть день";
            this.closeDayB.UseVisualStyleBackColor = true;
            this.closeDayB.Click += new System.EventHandler(this.closeDayB_Click);
            // 
            // printChequeB
            // 
            this.printChequeB.Location = new System.Drawing.Point(12, 243);
            this.printChequeB.Name = "printChequeB";
            this.printChequeB.Size = new System.Drawing.Size(110, 23);
            this.printChequeB.TabIndex = 4;
            this.printChequeB.Text = "Печать чека";
            this.printChequeB.UseVisualStyleBackColor = true;
            this.printChequeB.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(519, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Лог:";
            // 
            // logLB
            // 
            this.logLB.FormattingEnabled = true;
            this.logLB.Location = new System.Drawing.Point(522, 52);
            this.logLB.Name = "logLB";
            this.logLB.Size = new System.Drawing.Size(281, 316);
            this.logLB.TabIndex = 6;
            // 
            // printXReportB
            // 
            this.printXReportB.Location = new System.Drawing.Point(12, 272);
            this.printXReportB.Name = "printXReportB";
            this.printXReportB.Size = new System.Drawing.Size(110, 23);
            this.printXReportB.TabIndex = 7;
            this.printXReportB.Text = "Печать X-отчёта";
            this.printXReportB.UseVisualStyleBackColor = true;
            this.printXReportB.Click += new System.EventHandler(this.printXReportB_Click);
            // 
            // printZReportB
            // 
            this.printZReportB.Location = new System.Drawing.Point(12, 301);
            this.printZReportB.Name = "printZReportB";
            this.printZReportB.Size = new System.Drawing.Size(110, 23);
            this.printZReportB.TabIndex = 8;
            this.printZReportB.Text = "Печать Z-отчёта";
            this.printZReportB.UseVisualStyleBackColor = true;
            this.printZReportB.Click += new System.EventHandler(this.printZReportB_Click);
            // 
            // chequeBodyTB
            // 
            this.chequeBodyTB.Location = new System.Drawing.Point(235, 52);
            this.chequeBodyTB.Name = "chequeBodyTB";
            this.chequeBodyTB.Size = new System.Drawing.Size(281, 316);
            this.chequeBodyTB.TabIndex = 9;
            this.chequeBodyTB.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Тело чека:";
            // 
            // propertiesB
            // 
            this.propertiesB.Location = new System.Drawing.Point(12, 348);
            this.propertiesB.Name = "propertiesB";
            this.propertiesB.Size = new System.Drawing.Size(110, 23);
            this.propertiesB.TabIndex = 11;
            this.propertiesB.Text = "Свойства";
            this.propertiesB.UseVisualStyleBackColor = true;
            this.propertiesB.Click += new System.EventHandler(this.propertiesB_Click);
            // 
            // openSessionB
            // 
            this.openSessionB.Location = new System.Drawing.Point(12, 132);
            this.openSessionB.Name = "openSessionB";
            this.openSessionB.Size = new System.Drawing.Size(110, 23);
            this.openSessionB.TabIndex = 12;
            this.openSessionB.Text = "Открыть смену";
            this.openSessionB.UseVisualStyleBackColor = true;
            this.openSessionB.Click += new System.EventHandler(this.openSessionB_Click);
            // 
            // closeSessionB
            // 
            this.closeSessionB.Location = new System.Drawing.Point(12, 161);
            this.closeSessionB.Name = "closeSessionB";
            this.closeSessionB.Size = new System.Drawing.Size(110, 23);
            this.closeSessionB.TabIndex = 13;
            this.closeSessionB.Text = "Закрыть смену";
            this.closeSessionB.UseVisualStyleBackColor = true;
            this.closeSessionB.Click += new System.EventHandler(this.closeSessionB_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(815, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьТоварToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // добавитьТоварToolStripMenuItem
            // 
            this.добавитьТоварToolStripMenuItem.Name = "добавитьТоварToolStripMenuItem";
            this.добавитьТоварToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.добавитьТоварToolStripMenuItem.Text = "Добавить товар";
            this.добавитьТоварToolStripMenuItem.Click += new System.EventHandler(this.добавитьТоварToolStripMenuItem_Click);
            // 
            // beepB
            // 
            this.beepB.Location = new System.Drawing.Point(87, 201);
            this.beepB.Name = "beepB";
            this.beepB.Size = new System.Drawing.Size(110, 23);
            this.beepB.TabIndex = 16;
            this.beepB.Text = "Проверка связи";
            this.beepB.UseVisualStyleBackColor = true;
            this.beepB.Click += new System.EventHandler(this.beepB_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 374);
            this.Controls.Add(this.beepB);
            this.Controls.Add(this.closeSessionB);
            this.Controls.Add(this.openSessionB);
            this.Controls.Add(this.propertiesB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chequeBodyTB);
            this.Controls.Add(this.printZReportB);
            this.Controls.Add(this.printXReportB);
            this.Controls.Add(this.logLB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.printChequeB);
            this.Controls.Add(this.closeDayB);
            this.Controls.Add(this.sumTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.payB);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button payB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sumTB;
        private System.Windows.Forms.Button closeDayB;
        private System.Windows.Forms.Button printChequeB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox logLB;
        private System.Windows.Forms.Button printXReportB;
        private System.Windows.Forms.Button printZReportB;
        private System.Windows.Forms.RichTextBox chequeBodyTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button propertiesB;
        private System.Windows.Forms.Button openSessionB;
        private System.Windows.Forms.Button closeSessionB;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьТоварToolStripMenuItem;
        private System.Windows.Forms.Button beepB;
    }
}

