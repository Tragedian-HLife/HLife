using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.IO;
using System.Windows.Controls;

namespace HLife
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
            foreach (Mod mod in ModController.GetModsByType("Props"))
            {
                List<PropTemplate> newProps = XmlUtilities.CreateInstances<PropTemplate>(mod.Directory + @"\Props\PropTemplates.xml");
                newProps.ForEach(e => e.Source = mod);
                
                this.PropTemplates.AddRange(newProps);
            }
        }

        public override void Update()
        { }

        public Prop CreateProp(string name)
        {
            Prop newProp = new Prop();
            newProp.Template = this.GetPropTemplate(name);
            newProp.Image = newProp.Template.Image;
            newProp.MaxOccupancy = newProp.Template.MaxOccupancy;
            return newProp;
        }

        public void PopulatePropList()
        {
            WrapPanel wrap_Inventory = (WrapPanel)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "wrap_Inventory");

            wrap_Inventory.Children.Clear();

            foreach (Prop prop in Game.Instance.Player.Location.Inventory)
            {
                wrap_Inventory.Children.Add(prop.GetContextMenuItem());
            }
            

            wrap_Inventory = (WrapPanel)LogicalTreeHelper.FindLogicalNode(WindowController.Get<PersonInformationWindow>(), "wrap_Inventory");

            wrap_Inventory.Children.Clear();

            foreach (Prop prop in Game.Instance.Player.Inventory)
            {
                wrap_Inventory.Children.Add(prop.GetContextMenuItem());
            }
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

        public List<PropTemplate> GetPropTemplatesByAction(Action action)
        {
            List<PropTemplate> props = new List<PropTemplate>();

            foreach(PropTemplate prop in this.PropTemplates)
            {
                if(prop.Actions.Contains(action.Name))
                {
                    props.Add(prop);
                }
            }

            return props;
        }

        public List<Prop> GetPropsByTemplate(PropTemplate template, Location zone = null)
        {
            List<Prop> props = new List<Prop>();

            List<Prop> haystack = this.Props;

            if (zone != null)
            {
                haystack = zone.Inventory;
            }

            foreach (Prop prop in haystack)
            {
                if (prop.Template == template)
                {
                    props.Add(prop);
                }
            }

            return props;
        }

        public List<Prop> GetPropsByAction(Action action, Location zone = null)
        {
            List<Prop> props = new List<Prop>();

            foreach (PropTemplate template in this.GetPropTemplatesByAction(action))
            {
                props.AddRange(this.GetPropsByTemplate(template, zone));
            }

            return props;
        }

        public Prop GetClosestProp(List<Prop> props, Location start)
        {
            double min = 0;
            Prop minLoc = null;

            foreach (Prop prop in props)
            {
                double distance = start.TravelTime(prop.Location);

                if (distance < min)
                {
                    min = distance;
                    minLoc = prop;
                }
            }

            return minLoc;
        }
    }
}
