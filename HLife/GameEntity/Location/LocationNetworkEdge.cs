using HLife.GUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLife
{
    public class LocationNetworkEdge
    {
        public string Node { get; set; }

        public double Cost { get; set; }

        public Point Point { get; set; }

        public Size Size { get; set; }

        public void AddMapButton(Location parent)
        {
            Window form = WindowController.Get<MapWindow>();

            Size offset = new Size(0, 0);

            LocationButton button = new LocationButton();
            button.target = Location.Get(this.Node);
            button.VerticalAlignment = VerticalAlignment.Top;
            button.HorizontalAlignment = HorizontalAlignment.Left;

            if (parent.LocationMode == MapSizeModes.Absolute)
            {
                button.Margin = new Thickness(this.Point.X + offset.Width, this.Point.Y + offset.Height, 0, 0);

                button.Width    = this.Size.Width;
                button.Height   = this.Size.Height;
            }
            else if (parent.LocationMode == MapSizeModes.Relative)
            {
                button.Margin = new Thickness(
                    (this.Point.X + offset.Width)   * parent.AutoSizeMultiplier, 
                    (this.Point.Y + offset.Height)  * parent.AutoSizeMultiplier, 
                    0, 
                    0);

                button.Width    = this.Size.Width    * parent.AutoSizeMultiplier;
                button.Height   = this.Size.Height   * parent.AutoSizeMultiplier;
            }
            else if (parent.LocationMode == MapSizeModes.Percent)
            {
                button.Margin = new Thickness(
                    (((double)this.Point.X / 100) * parent.Size.Width)    + offset.Width,
                    (((double)this.Point.Y / 100) * parent.Size.Height)   + offset.Height,
                    0,
                    0);

                button.Width    = this.Size.Width * parent.AutoSizeMultiplier;
                button.Height   = this.Size.Height * parent.AutoSizeMultiplier;
            }

            button.Load();

            ((Grid)LogicalTreeHelper.FindLogicalNode(form, "grid")).Children.Add(button);

            ToolTip tooltip = new ToolTip();
            tooltip.Content = this.Node;
            button.ToolTip = tooltip;
        }
    }
}
