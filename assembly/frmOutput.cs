﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assembly
{
    public partial class frmOutput : Form
    {
        public frmOutput(String output)
        {
            InitializeComponent();
            this.txOutput.Lines = output.Split('\n');
        }

        private void frmOutput_Load(object sender, EventArgs e)
        {
            txOutput.ScrollBars = ScrollBars.Both;
        }
    }
}
