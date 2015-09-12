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
