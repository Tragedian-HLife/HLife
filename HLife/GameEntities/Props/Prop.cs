using HLife.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace HLife.GameEntities.Props
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
            this.Stats = new Stats(typeof(Prop));

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
        public BitmapImage GetImage()
        {
            // Request the image from the ResourceController.
            return new BitmapImage(Game.Instance.ResourceController.GetPropImage(this));
        }

        public Image GetContextMenuItem()
        {
            Image pb = new Image();
            pb.Width = 50;
            pb.Height = 50;
            pb.Stretch = System.Windows.Media.Stretch.Uniform;
            pb.Source = this.GetImage();
            //pb.Margin = new Padding(10);
            
            ContextMenu menu = new ContextMenu();

            MenuItem actions = new MenuItem();
            actions.Header = "Actions";

            menu.Items.Add(actions);

            foreach (string actionName in this.Template.Actions)
            {
                MenuItem displayAction = GameAction.Get(actionName).GetMenuItemForProp(new ActionEventArgs(Game.Instance.Player, null, this));

                if (displayAction != null)
                {
                    actions.Items.Add(displayAction);
                }
            }

            pb.ContextMenu = menu;

            ToolTip tooltip = new ToolTip();
            tooltip.Content = this.Template.Description;
            pb.ToolTip = tooltip;

            return pb;
        }
    }
}
