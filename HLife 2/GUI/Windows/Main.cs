using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace HLife_2
{
    public partial class Hlife2 : Form
    {
        public Hlife2()
        {
            InitializeComponent();

            this.KeyPreview = true;

            this.Activated += new EventHandler(Hlife2_Activated);

            this.KeyPress += new KeyPressEventHandler(Hlife2_KeyPress);
        }

        private void Hlife2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'q')
            {
                Game.Instance.WindowController.BringAllWindowsToFront(this);
            }
        }

        private void Hlife2_Load(object sender, EventArgs e)
        {
            Game.Instance.Player.Location = HLife_2.Location.Get("Home");
        }

        private void Hlife2_Activated(object sender, EventArgs e)
        {
        }
    }
}
