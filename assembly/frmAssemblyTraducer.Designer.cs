namespace assembly
{
    partial class frmAssemblyTraducer
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssemblyTraducer));
            this.txInput = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBits = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSeparator = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txInput
            // 
            this.txInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txInput.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txInput.Location = new System.Drawing.Point(0, 39);
            this.txInput.Multiline = true;
            this.txInput.Name = "txInput";
            this.txInput.Size = new System.Drawing.Size(798, 316);
            this.txInput.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnGo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 355);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 43);
            this.panel1.TabIndex = 1;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGo.Location = new System.Drawing.Point(705, 10);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Traduce";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbBits);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cbSeparator);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 39);
            this.panel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(443, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Bits:";
            // 
            // cbBits
            // 
            this.cbBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbBits.FormattingEnabled = true;
            this.cbBits.Items.AddRange(new object[] {
            "8",
            "4"});
            this.cbBits.Location = new System.Drawing.Point(473, 9);
            this.cbBits.Name = "cbBits";
            this.cbBits.Size = new System.Drawing.Size(121, 21);
            this.cbBits.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(600, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Separator:";
            // 
            // cbSeparator
            // 
            this.cbSeparator.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbSeparator.FormattingEnabled = true;
            this.cbSeparator.Items.AddRange(new object[] {
            "TAB",
            ";",
            ","});
            this.cbSeparator.Location = new System.Drawing.Point(662, 9);
            this.cbSeparator.Name = "cbSeparator";
            this.cbSeparator.Size = new System.Drawing.Size(121, 21);
            this.cbSeparator.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Insert commands CSV";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnBack.Location = new System.Drawing.Point(21, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Go back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmAssemblyTraducer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(798, 398);
            this.Controls.Add(this.txInput);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(555, 260);
            this.Name = "frmAssemblyTraducer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assembly traducer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBits;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSeparator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBack;
    }
}

