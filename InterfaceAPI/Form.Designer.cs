namespace InterfaceAPI
{
    partial class Form
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
            panel2 = new Panel();
            comb_Cod_Produto = new ComboBox();
            panel1 = new Panel();
            label_ID_Produto = new Label();
            label1 = new Label();
            label2 = new Label();
            button_Inserir_Teste = new Button();
            button_Inserir_Produto = new Button();
            label_Tempo_Producao = new Label();
            label_Codigo_Resultado = new Label();
            text_Data_Teste = new TextBox();
            label_Data_Teste = new Label();
            text_Codigo_Resultado = new TextBox();
            text_Tempo_Producao = new TextBox();
            text_Codigo_Peca = new TextBox();
            text_Hora_Producao = new TextBox();
            text_Data_Producao = new TextBox();
            label_Codigo_Peca = new Label();
            label_Hora_Producao = new Label();
            label_Data_Producao = new Label();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ScrollBar;
            panel2.BackgroundImageLayout = ImageLayout.Center;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(comb_Cod_Produto);
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(label_ID_Produto);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(button_Inserir_Teste);
            panel2.Controls.Add(button_Inserir_Produto);
            panel2.Controls.Add(label_Tempo_Producao);
            panel2.Controls.Add(label_Codigo_Resultado);
            panel2.Location = new Point(121, 121);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(673, 373);
            panel2.TabIndex = 30;
            // 
            // comb_Cod_Produto
            // 
            comb_Cod_Produto.DropDownStyle = ComboBoxStyle.DropDownList;
            comb_Cod_Produto.FormattingEnabled = true;
            comb_Cod_Produto.Location = new Point(510, 83);
            comb_Cod_Produto.Margin = new Padding(3, 4, 3, 4);
            comb_Cod_Produto.Name = "comb_Cod_Produto";
            comb_Cod_Produto.Size = new Size(143, 28);
            comb_Cod_Produto.TabIndex = 50;
            comb_Cod_Produto.SelectedIndexChanged += comb_Cod_Produto_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GrayText;
            panel1.Location = new Point(336, -1);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(6, 373);
            panel1.TabIndex = 47;
            // 
            // label_ID_Produto
            // 
            label_ID_Produto.AutoSize = true;
            label_ID_Produto.BackColor = SystemColors.ScrollBar;
            label_ID_Produto.Location = new Point(368, 87);
            label_ID_Produto.Name = "label_ID_Produto";
            label_ID_Produto.Size = new Size(113, 20);
            label_ID_Produto.TabIndex = 43;
            label_ID_Produto.Text = "Código da Peça";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(104, 17);
            label1.Name = "label1";
            label1.Size = new Size(123, 37);
            label1.TabIndex = 48;
            label1.Text = "Produto";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(465, 17);
            label2.Name = "label2";
            label2.Size = new Size(84, 37);
            label2.TabIndex = 49;
            label2.Text = "Teste";
            // 
            // button_Inserir_Teste
            // 
            button_Inserir_Teste.BackColor = SystemColors.Window;
            button_Inserir_Teste.Location = new Point(438, 311);
            button_Inserir_Teste.Name = "button_Inserir_Teste";
            button_Inserir_Teste.Size = new Size(146, 29);
            button_Inserir_Teste.TabIndex = 42;
            button_Inserir_Teste.Text = "Inserir Teste";
            button_Inserir_Teste.UseVisualStyleBackColor = false;
            button_Inserir_Teste.Click += button_Inserir_Teste_ClickAsync;
            // 
            // button_Inserir_Produto
            // 
            button_Inserir_Produto.BackColor = SystemColors.Window;
            button_Inserir_Produto.Location = new Point(78, 311);
            button_Inserir_Produto.Name = "button_Inserir_Produto";
            button_Inserir_Produto.Size = new Size(146, 29);
            button_Inserir_Produto.TabIndex = 41;
            button_Inserir_Produto.Text = "Inserir Produto";
            button_Inserir_Produto.UseVisualStyleBackColor = false;
            button_Inserir_Produto.Click += button_Inserir_Produto_ClickAsync;
            // 
            // label_Tempo_Producao
            // 
            label_Tempo_Producao.AutoSize = true;
            label_Tempo_Producao.BackColor = SystemColors.ScrollBar;
            label_Tempo_Producao.Location = new Point(16, 256);
            label_Tempo_Producao.Name = "label_Tempo_Producao";
            label_Tempo_Producao.Size = new Size(143, 20);
            label_Tempo_Producao.TabIndex = 34;
            label_Tempo_Producao.Text = "Tempo de Produção";
            // 
            // label_Codigo_Resultado
            // 
            label_Codigo_Resultado.AutoSize = true;
            label_Codigo_Resultado.BackColor = SystemColors.ScrollBar;
            label_Codigo_Resultado.Location = new Point(368, 148);
            label_Codigo_Resultado.Name = "label_Codigo_Resultado";
            label_Codigo_Resultado.Size = new Size(135, 20);
            label_Codigo_Resultado.TabIndex = 35;
            label_Codigo_Resultado.Text = "Resultado do Teste";
            // 
            // text_Data_Teste
            // 
            text_Data_Teste.Location = new Point(633, 321);
            text_Data_Teste.Name = "text_Data_Teste";
            text_Data_Teste.Size = new Size(143, 27);
            text_Data_Teste.TabIndex = 46;
            // 
            // label_Data_Teste
            // 
            label_Data_Teste.AutoSize = true;
            label_Data_Teste.BackColor = SystemColors.ScrollBar;
            label_Data_Teste.Location = new Point(491, 325);
            label_Data_Teste.Name = "label_Data_Teste";
            label_Data_Teste.Size = new Size(100, 20);
            label_Data_Teste.TabIndex = 45;
            label_Data_Teste.Text = "Data de Teste";
            // 
            // text_Codigo_Resultado
            // 
            text_Codigo_Resultado.Location = new Point(633, 261);
            text_Codigo_Resultado.Name = "text_Codigo_Resultado";
            text_Codigo_Resultado.Size = new Size(143, 27);
            text_Codigo_Resultado.TabIndex = 40;
            // 
            // text_Tempo_Producao
            // 
            text_Tempo_Producao.Location = new Point(288, 369);
            text_Tempo_Producao.Name = "text_Tempo_Producao";
            text_Tempo_Producao.Size = new Size(143, 27);
            text_Tempo_Producao.TabIndex = 39;
            // 
            // text_Codigo_Peca
            // 
            text_Codigo_Peca.Location = new Point(288, 200);
            text_Codigo_Peca.Name = "text_Codigo_Peca";
            text_Codigo_Peca.Size = new Size(143, 27);
            text_Codigo_Peca.TabIndex = 38;
            // 
            // text_Hora_Producao
            // 
            text_Hora_Producao.Location = new Point(288, 315);
            text_Hora_Producao.Name = "text_Hora_Producao";
            text_Hora_Producao.Size = new Size(143, 27);
            text_Hora_Producao.TabIndex = 37;
            // 
            // text_Data_Producao
            // 
            text_Data_Producao.Location = new Point(288, 261);
            text_Data_Producao.Name = "text_Data_Producao";
            text_Data_Producao.Size = new Size(143, 27);
            text_Data_Producao.TabIndex = 36;
            // 
            // label_Codigo_Peca
            // 
            label_Codigo_Peca.AutoSize = true;
            label_Codigo_Peca.BackColor = SystemColors.ScrollBar;
            label_Codigo_Peca.Location = new Point(139, 211);
            label_Codigo_Peca.Name = "label_Codigo_Peca";
            label_Codigo_Peca.Size = new Size(113, 20);
            label_Codigo_Peca.TabIndex = 33;
            label_Codigo_Peca.Text = "Código da Peça";
            // 
            // label_Hora_Producao
            // 
            label_Hora_Producao.AutoSize = true;
            label_Hora_Producao.BackColor = SystemColors.ScrollBar;
            label_Hora_Producao.Location = new Point(139, 325);
            label_Hora_Producao.Name = "label_Hora_Producao";
            label_Hora_Producao.Size = new Size(130, 20);
            label_Hora_Producao.TabIndex = 32;
            label_Hora_Producao.Text = "Hora de Produção";
            // 
            // label_Data_Producao
            // 
            label_Data_Producao.AutoSize = true;
            label_Data_Producao.BackColor = SystemColors.ScrollBar;
            label_Data_Producao.Location = new Point(139, 272);
            label_Data_Producao.Name = "label_Data_Producao";
            label_Data_Producao.Size = new Size(129, 20);
            label_Data_Producao.TabIndex = 31;
            label_Data_Producao.Text = "Data de Produção";
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(text_Data_Teste);
            Controls.Add(label_Data_Teste);
            Controls.Add(text_Codigo_Resultado);
            Controls.Add(text_Tempo_Producao);
            Controls.Add(text_Codigo_Peca);
            Controls.Add(text_Hora_Producao);
            Controls.Add(text_Data_Producao);
            Controls.Add(label_Codigo_Peca);
            Controls.Add(label_Hora_Producao);
            Controls.Add(label_Data_Producao);
            Controls.Add(panel2);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form";
            Text = "API";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel2;
        private Label label2;
        private Label label1;
        private Panel panel1;
        private TextBox text_Data_Teste;
        private Label label_Data_Teste;
        private Label label_ID_Produto;
        private Button button_Inserir_Teste;
        private Button button_Inserir_Produto;
        private TextBox text_Codigo_Resultado;
        private TextBox text_Tempo_Producao;
        private TextBox text_Codigo_Peca;
        private TextBox text_Hora_Producao;
        private TextBox text_Data_Producao;
        private Label label_Codigo_Resultado;
        private Label label_Tempo_Producao;
        private Label label_Codigo_Peca;
        private Label label_Hora_Producao;
        private Label label_Data_Producao;
        private ComboBox comb_Cod_Produto;
    }
}
