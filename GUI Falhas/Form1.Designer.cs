using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI_Falhas
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            DataProd = new TextBox();
            HoraProd = new TextBox();
            Codigo = new TextBox();
            TempoProd = new TextBox();
            ResultadoTeste = new TextBox();
            DescricaoTeste = new TextBox();
            DataTeste = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 52);
            label1.Name = "label1";
            label1.Size = new Size(129, 20);
            label1.TabIndex = 0;
            label1.Text = "Data de Produção";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 115);
            label2.Name = "label2";
            label2.Size = new Size(130, 20);
            label2.TabIndex = 1;
            label2.Text = "Hora de Produção";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 182);
            label3.Name = "label3";
            label3.Size = new Size(113, 20);
            label3.TabIndex = 2;
            label3.Text = "Código da Peça";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 251);
            label4.Name = "label4";
            label4.Size = new Size(143, 20);
            label4.TabIndex = 3;
            label4.Text = "Tempo de Produção";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(38, 319);
            label5.Name = "label5";
            label5.Size = new Size(135, 20);
            label5.TabIndex = 4;
            label5.Text = "Resultado do Teste";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(38, 379);
            label6.Name = "label6";
            label6.Size = new Size(101, 20);
            label6.TabIndex = 5;
            label6.Text = "Data do Teste";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(361, 319);
            label7.Name = "label7";
            label7.Size = new Size(74, 20);
            label7.TabIndex = 6;
            label7.Text = "Descrição";
            // 
            // DataProd
            // 
            DataProd.Location = new Point(200, 49);
            DataProd.Name = "DataProd";
            DataProd.Size = new Size(125, 27);
            DataProd.TabIndex = 7;
            // 
            // HoraProd
            // 
            HoraProd.Location = new Point(200, 115);
            HoraProd.Name = "HoraProd";
            HoraProd.Size = new Size(125, 27);
            HoraProd.TabIndex = 8;
            // 
            // Codigo
            // 
            Codigo.Location = new Point(200, 182);
            Codigo.Name = "Codigo";
            Codigo.Size = new Size(125, 27);
            Codigo.TabIndex = 9;
            // 
            // TempoProd
            // 
            TempoProd.Location = new Point(200, 248);
            TempoProd.Name = "TempoProd";
            TempoProd.Size = new Size(125, 27);
            TempoProd.TabIndex = 10;
            // 
            // ResultadoTeste
            // 
            ResultadoTeste.Location = new Point(200, 312);
            ResultadoTeste.Name = "ResultadoTeste";
            ResultadoTeste.Size = new Size(125, 27);
            ResultadoTeste.TabIndex = 11;
            // 
            // DescricaoTeste
            // 
            DescricaoTeste.Location = new Point(458, 316);
            DescricaoTeste.Name = "DescricaoTeste";
            DescricaoTeste.Size = new Size(125, 27);
            DescricaoTeste.TabIndex = 12;
            // 
            // DataTeste
            // 
            DataTeste.Location = new Point(200, 372);
            DataTeste.Name = "DataTeste";
            DataTeste.Size = new Size(125, 27);
            DataTeste.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DataTeste);
            Controls.Add(DescricaoTeste);
            Controls.Add(ResultadoTeste);
            Controls.Add(TempoProd);
            Controls.Add(Codigo);
            Controls.Add(HoraProd);
            Controls.Add(DataProd);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox DataProd;
        private TextBox HoraProd;
        private TextBox Codigo;
        private TextBox TempoProd;
        private TextBox ResultadoTeste;
        private TextBox DescricaoTeste;
        private TextBox DataTeste;
    }
}
