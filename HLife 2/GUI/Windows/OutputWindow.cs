using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public partial class OutputWindow : Form
    {
        public RichTextBox output;
        public OutputWindow()
        {
            InitializeComponent();

            output = this.rtb_output;
        }

        private void OutputWindow_Load(object sender, EventArgs e)
        {

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.rtb_output.Clear();
        }

        private void rtb_output_TextChanged(object sender, EventArgs e)
        {
            if (this.chk_autoScroll.Checked)
            {
                this.rtb_output.SelectionStart = this.rtb_output.Text.Length;
                this.rtb_output.ScrollToCaret();
            }
        }

        private void chk_autoScroll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_autoScroll.Checked)
            {
                this.rtb_output.SelectionStart = this.rtb_output.Text.Length;
                this.rtb_output.ScrollToCaret();
            }
        }
    }
}
