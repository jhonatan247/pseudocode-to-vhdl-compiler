using System;
using System.Collections.Generic;
using System.Windows.Forms;
using assembly.Model;
using assembly.Enums;
using assembly.Data;
using assembly.Logic;
using System.Diagnostics;

namespace assembly
{
    public partial class frmPseudoCodeCompiler : Form
    {
        PseudoCodeToHarvardCompiler harvardComplier;

        public frmPseudoCodeCompiler()
        {
            InitializeComponent();
            harvardComplier = new PseudoCodeToHarvardCompiler();
            harvardComplier.onSyntaxError += onSyntaxError;
            harvardComplier.onCompileFinally += onCompileFinally;
        }
        void onSyntaxError(int line, int initialCharacter, string errorMessage) {
            MessageBox.Show("An error has occurred:\n" + errorMessage + "\nAt the line: " + line, "PseudoCode compiler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txInput.SelectionStart = initialCharacter;
            txInput.SelectionLength = txInput.Lines[line].Length;
            txInput.Focus();
        }
        void onCompileFinally(string harvardCode) {
            Hide();
            new frmAssemblyTraducer(harvardCode, cbSeparator.SelectedIndex).ShowDialog();
            Show();
        }
        private void frmTransformPseudoCode_Load(object sender, EventArgs e)
        {
            txInput.ScrollBars = ScrollBars.Both;
            cbSeparator.SelectedIndex = 0;
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/jhonatan247/pseudocode-to-vhdl-compiler/blob/master/README.md");
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            harvardComplier.Compile(GetSeparator(), this.txInput.Lines);
        }
        char GetSeparator()
        {
            if (cbSeparator.SelectedItem == null)
            {
                cbSeparator.SelectedIndex = 0;
            }
            switch (cbSeparator.SelectedIndex)
            {
                case 0: return '\t';
                case 1: return ';';
                case 2: return ',';
            }

            return '\t';
        }
    }
}