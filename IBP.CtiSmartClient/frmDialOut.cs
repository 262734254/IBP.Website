using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBP.CtiSmartClient
{
    public partial class frmDialOut : Form
    {
        public string outPhone = "";
        public MainForm parentForm = null;

        public frmDialOut()
        {
            InitializeComponent();
        }

        public frmDialOut(MainForm parent)
        {
            InitializeComponent();
            this.parentForm = parent;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            outPhone = this.textBox1.Text;

            if (this.parentForm.lblStatus.Text == "AUX")
            {
                this.parentForm.axaOCX1.DialOut(outPhone);
                this.parentForm.NowInComeCallId = Guid.NewGuid().ToString();
            }
            else
            {
                this.parentForm.axaOCX1.CmdMakeCallStop();
            }

            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.btnOk.Focus();
            }
        }
    }
}
