namespace assembly
{
    partial class frmPseudoCodeCompiler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPseudoCodeCompiler));
            this.txInput = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSeparator = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txInput
            // 
            this.txInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txInput.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txInput.ForeColor = System.Drawing.Color.DarkOrange;
            this.txInput.Location = new System.Drawing.Point(0, 39);
            this.txInput.Multiline = true;
            this.txInput.Name = "txInput";
            this.txInput.Size = new System.Drawing.Size(426, 326);
            this.txInput.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnHelp);
            this.panel1.Controls.Add(this.btnGo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 365);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 43);
            this.panel1.TabIndex = 4;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnHelp.Location = new System.Drawing.Point(21, 10);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 1;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGo.Location = new System.Drawing.Point(333, 10);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Compile";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cbSeparator);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 39);
            this.panel2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(231, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
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
            this.cbSeparator.Location = new System.Drawing.Point(293, 9);
            this.cbSeparator.Name = "cbSeparator";
            this.cbSeparator.Size = new System.Drawing.Size(121, 21);
            this.cbSeparator.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Insert pseudo code";
            // 
            // frmPseudoCodeCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(426, 408);
            this.Controls.Add(this.txInput);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(442, 447);
            this.Name = "frmPseudoCodeCompiler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pseudo code compiler";
            this.Load += new System.EventHandler(this.frmTransformPseudoCode_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSeparator;
        private System.Windows.Forms.Button btnHelp;
    }
}