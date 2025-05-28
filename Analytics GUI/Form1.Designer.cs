namespace Analytics_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            TextBox_total_pecas = new TextBox();
            label2 = new Label();
            TextBox_total_pecas_falha = new TextBox();
            label3 = new Label();
            label4 = new Label();
            TextBox_total_pecas_OK = new TextBox();
            textBox_Tempo_Medio = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 75);
            label1.Name = "label1";
            label1.Size = new Size(239, 20);
            label1.TabIndex = 0;
            label1.Text = "Número total de peças produzidas";
            // 
            // TextBox_total_pecas
            // 
            TextBox_total_pecas.Location = new Point(384, 75);
            TextBox_total_pecas.Name = "TextBox_total_pecas";
            TextBox_total_pecas.Size = new Size(125, 27);
            TextBox_total_pecas.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 160);
            label2.Name = "label2";
            label2.Size = new Size(309, 20);
            label2.TabIndex = 2;
            label2.Text = "Número total de peças produzidas com falha\r\n";
            // 
            // TextBox_total_pecas_falha
            // 
            TextBox_total_pecas_falha.Location = new Point(384, 160);
            TextBox_total_pecas_falha.Name = "TextBox_total_pecas_falha";
            TextBox_total_pecas_falha.PlaceholderText = "0";
            TextBox_total_pecas_falha.Size = new Size(125, 27);
            TextBox_total_pecas_falha.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 241);
            label3.Name = "label3";
            label3.Size = new Size(185, 20);
            label3.TabIndex = 4;
            label3.Text = "Número total de peças OK\r\n";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 322);
            label4.Name = "label4";
            label4.Size = new Size(191, 20);
            label4.TabIndex = 5;
            label4.Text = "Tempo médio de produção\r\n";
            // 
            // TextBox_total_pecas_OK
            // 
            TextBox_total_pecas_OK.Location = new Point(384, 241);
            TextBox_total_pecas_OK.Name = "TextBox_total_pecas_OK";
            TextBox_total_pecas_OK.PlaceholderText = "0";
            TextBox_total_pecas_OK.Size = new Size(125, 27);
            TextBox_total_pecas_OK.TabIndex = 6;
            // 
            // textBox_Tempo_Medio
            // 
            textBox_Tempo_Medio.Location = new Point(384, 322);
            textBox_Tempo_Medio.Name = "textBox_Tempo_Medio";
            textBox_Tempo_Medio.Size = new Size(125, 27);
            textBox_Tempo_Medio.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox_Tempo_Medio);
            Controls.Add(TextBox_total_pecas_OK);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(TextBox_total_pecas_falha);
            Controls.Add(label2);
            Controls.Add(TextBox_total_pecas);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Analytics";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox TextBox_total_pecas;
        private Label label2;
        private TextBox TextBox_total_pecas_falha;
        private Label label3;
        private Label label4;
        private TextBox TextBox_total_pecas_OK;
        private TextBox textBox_Tempo_Medio;
    }
}
