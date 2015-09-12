using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace HLife_2
{
    public class PropController
        : Controller
    {
        public List<PropTemplate> PropTemplates { get; set; }

        public List<Prop> Props { get; set; }

        public PropController()
        {
            this.Props = new List<Prop>();
            this.PropTemplates = new List<PropTemplate>();
        }

        public override void Initialize()
        {
            this.PropTemplates = XmlUtilities.CreateInstances<PropTemplate>(@"Cities\" + Game.Instance.City.DisplayName + @"\Resources\PropTemplates.xml");
        }

        public override void Update()
        { }

        public Prop CreateProp(string name)
        {
            Prop newProp = new Prop();
            newProp.Template = this.GetPropTemplate(name);
            return newProp;
        }

        public void PopulatePropList()
        {
            FlowLayoutPanel objectsPanel = (FlowLayoutPanel)((TabControl)((SplitContainer)WindowController.Get<Hlife2>().Controls.Find("split", false)[0]).Panel1
                .Controls.Find("tabs_Location", false)[0]).TabPages[2].Controls.Find("flow_objects", false)[0];

            objectsPanel.Controls.Clear();

            foreach (Prop prop in Game.Instance.Player.Location.Inventory)
            {
                PictureBox pb = prop.GetContextMenuItem();

                pb.Parent = objectsPanel;
                objectsPanel.Controls.Add(pb);
            }





            FlowLayoutPanel inventory = (FlowLayoutPanel)((TabControl)WindowController.Get<PlayerInfoWindow>(Game.Instance.Player)
                .Controls.Find("tabs_Container", false)[0]).TabPages[2].Controls.Find("flow_inventory", false)[0];

            inventory.Controls.Clear();

            foreach (Prop prop in Game.Instance.Player.Inventory)
            {
                PictureBox pb = prop.GetContextMenuItem();

                pb.Parent = inventory;
                inventory.Controls.Add(pb);
            }
        }

        // TODO: Remove this.
        public virtual void HandlePropAction(Person doer, Prop prop, Action action, Person target)
        {
            Game.Instance.Player.TryAction(action.Name, Guid.Empty, prop.Template.DisplayName);
        }

        public Prop GetProp(string displayName)
        {
            foreach (Prop prop in this.Props)
            {
                if (prop.Template.DisplayName == displayName)
                {
                    return prop;
                }
            }

            return null;
        }

        public PropTemplate GetPropTemplate(string name)
        {
            foreach (PropTemplate prop in this.PropTemplates)
            {
                if (prop.Name == name)
                {
                    return prop;
                }
            }

            return null;
        }
    }
}
