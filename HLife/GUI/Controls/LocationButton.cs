using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HLife
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
            //this.Name = Guid.NewGuid().ToString();
            this.TabIndex = 0;
            this.Background = Brushes.Transparent;
        }
    }
}
