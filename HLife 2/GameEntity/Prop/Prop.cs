using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HLife_2
{
    /// <summary>
    /// Instance of a world object such as a bed or tv.
    /// </summary>
    public class Prop
        : GameEntity
    {
        /// <summary>
        /// Default settings from the prop template.
        /// </summary>
        public PropTemplate Template { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Prop()
            : base()
        {
            // Add this instance to the PropController.
            Game.Instance.PropController.Props.Add(this);
        }

        /// <summary>
        /// Default destructor.
        /// </summary>
        ~Prop()
        {
            // Remove this instance from the PropController.
            Game.Instance.PropController.Props.Remove(this);
        }

        /// <summary>
        /// Gets the Image object for this prop.
        /// </summary>
        /// <returns>Image object.</returns>
        public Image GetImage()
        {
            // Request the image from the ResourceController.
            return Game.Instance.ResourceController.GetPropImage(this.Template.Image);
        }

        public PictureBox GetContextMenuItem()
        {
            PictureBox pb = new PictureBox();
            pb.Size = new Size(50, 50);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Image = this.GetImage();
            pb.Margin = new Padding(10);

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.ShowItemToolTips = true;

            ToolStripMenuItem actions = new ToolStripMenuItem("Actions");

            menu.Items.Add(actions);

            foreach (string actionName in this.Template.Actions)
            {
                ToolStripMenuItem displayAction = Action.Get(actionName).GetContextMenuItem(new ActionEventArgs(Game.Instance.Player, null, this));

                if (displayAction != null)
                {
                    actions.DropDownItems.Add(displayAction);
                }
            }

            pb.ContextMenuStrip = menu;

            ToolTip tooltip = new ToolTip();
            tooltip.ToolTipTitle = this.Template.DisplayName;
            tooltip.SetToolTip(pb, this.Template.Description);

            return pb;
        }
    }
}
