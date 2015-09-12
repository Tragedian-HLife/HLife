using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public class LocationNetworkEdge
    {
        public string Node { get; set; }

        public double Cost { get; set; }

        public Point Point { get; set; }

        public Size Size { get; set; }

        public void AddMapButton(Location parent)
        {
            Form form = WindowController.Get<MapWindow>();

            Size offset = new Size(0, 0);

            LocationButton button = new LocationButton();
            button.target = Location.Get(this.Node);

            if (parent.LocationMode == MapSizeModes.Absolute)
            {
                button.Location = this.Point + offset;

                button.Size = this.Size;
            }
            else if (parent.LocationMode == MapSizeModes.Relative)
            {
                button.Location = new System.Drawing.Point(
                    (int)Math.Round((this.Point.X + offset.Width) * parent.AutoSizeMultiplier),
                    (int)Math.Round((this.Point.Y + offset.Height) * parent.AutoSizeMultiplier));

                button.Size = new Size(
                    (int)Math.Round(this.Size.Width * parent.AutoSizeMultiplier),
                    (int)Math.Round(this.Size.Height * parent.AutoSizeMultiplier));
            }
            else if (parent.LocationMode == MapSizeModes.Percent)
            {
                button.Location = new System.Drawing.Point(
                    (int)Math.Round(((double)this.Point.X / 100) * parent.Size.Width) + offset.Width,
                    (int)Math.Round(((double)this.Point.Y / 100) * parent.Size.Height) + offset.Height);

                button.Size = new Size(
                    (int)Math.Round(this.Size.Width * parent.AutoSizeMultiplier),
                    (int)Math.Round(this.Size.Height * parent.AutoSizeMultiplier));
            }

            button.Load();

            form.Controls.Add(button);
        }
    }
}
