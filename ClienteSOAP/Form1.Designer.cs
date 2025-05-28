namespace ClienteSOAP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbMetodo = new System.Windows.Forms.ComboBox();
            this.pnlParametros = new System.Windows.Forms.Panel();
            this.btnExecutar = new System.Windows.Forms.Button();
            this.textResultado = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbMetodo
            // 
            this.cbMetodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMetodo.FormattingEnabled = true;
            this.cbMetodo.Location = new System.Drawing.Point(79, 52);
            this.cbMetodo.Name = "cbMetodo";
            this.cbMetodo.Size = new System.Drawing.Size(201, 21);
            this.cbMetodo.TabIndex = 0;
            this.cbMetodo.SelectedIndexChanged += new System.EventHandler(this.cbMetodo_SelectedIndexChanged);
            // 
            // pnlParametros
            // 
            this.pnlParametros.AutoScroll = true;
            this.pnlParametros.Location = new System.Drawing.Point(69, 79);
            this.pnlParametros.Name = "pnlParametros";
            this.pnlParametros.Size = new System.Drawing.Size(234, 258);
            this.pnlParametros.TabIndex = 1;
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(338, 52);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 2;
            this.btnExecutar.Text = "Selecionar";
            this.btnExecutar.UseVisualStyleBackColor = true;
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // textResultado
            // 
            this.textResultado.Location = new System.Drawing.Point(441, 79);
            this.textResultado.Name = "textResultado";
            this.textResultado.ReadOnly = true;
            this.textResultado.Size = new System.Drawing.Size(280, 258);
            this.textResultado.TabIndex = 3;
            this.textResultado.Text = "";
            this.textResultado.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(438, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Resultados:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Opções:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textResultado);
            this.Controls.Add(this.btnExecutar);
            this.Controls.Add(this.pnlParametros);
            this.Controls.Add(this.cbMetodo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMetodo;
        private System.Windows.Forms.Panel pnlParametros;
        private System.Windows.Forms.Button btnExecutar;
        private System.Windows.Forms.RichTextBox textResultado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

