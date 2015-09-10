using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public class LocationButton
        : Button
    {
        public Location target;

        public LocationButton()
            : base()
        {
            this.Click += this.OnClick;
        }

        public void OnClick(object sender, EventArgs e)
        {
            Game.Instance.Player.Location = this.target;
        }

        public void Load()
        {
            this.Name = Guid.NewGuid().ToString();
            this.TabIndex = 0;
            this.UseVisualStyleBackColor = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = System.Drawing.Color.Orange;
        }
    }
}
