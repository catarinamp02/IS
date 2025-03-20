namespace Sistema_Legado
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
            dataProd = new TextBox();
            horaProd = new TextBox();
            codigoPeca = new TextBox();
            tempoProd = new TextBox();
            resultadoTeste = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 54);
            label1.Name = "label1";
            label1.Size = new Size(130, 20);
            label1.TabIndex = 0;
            label1.Text = "Data de produção";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 122);
            label2.Name = "label2";
            label2.Size = new Size(131, 20);
            label2.TabIndex = 1;
            label2.Text = "Hora de produção";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(50, 193);
            label3.Name = "label3";
            label3.Size = new Size(115, 20);
            label3.TabIndex = 2;
            label3.Text = "Código da peça";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(48, 250);
            label4.Name = "label4";
            label4.Size = new Size(144, 20);
            label4.TabIndex = 3;
            label4.Text = "Tempo de produção";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(48, 316);
            label5.Name = "label5";
            label5.Size = new Size(133, 20);
            label5.TabIndex = 4;
            label5.Text = "Resultado do teste";
            // 
            // dataProd
            // 
            dataProd.Location = new Point(216, 54);
            dataProd.Name = "dataProd";
            dataProd.Size = new Size(144, 27);
            dataProd.TabIndex = 5;
            // 
            // horaProd
            // 
            horaProd.Location = new Point(216, 122);
            horaProd.Name = "horaProd";
            horaProd.Size = new Size(144, 27);
            horaProd.TabIndex = 6;
            // 
            // codigoPeca
            // 
            codigoPeca.Location = new Point(216, 193);
            codigoPeca.Name = "codigoPeca";
            codigoPeca.Size = new Size(144, 27);
            codigoPeca.TabIndex = 7;
            // 
            // tempoProd
            // 
            tempoProd.Location = new Point(216, 250);
            tempoProd.Name = "tempoProd";
            tempoProd.Size = new Size(144, 27);
            tempoProd.TabIndex = 8;
            // 
            // resultadoTeste
            // 
            resultadoTeste.Location = new Point(216, 313);
            resultadoTeste.Name = "resultadoTeste";
            resultadoTeste.Size = new Size(144, 27);
            resultadoTeste.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(resultadoTeste);
            Controls.Add(tempoProd);
            Controls.Add(codigoPeca);
            Controls.Add(horaProd);
            Controls.Add(dataProd);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox dataProd;
        private TextBox horaProd;
        private TextBox codigoPeca;
        private TextBox tempoProd;
        private TextBox resultadoTeste;
    }
}
