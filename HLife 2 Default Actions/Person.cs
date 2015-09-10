using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2.Actions.Person
{
    public partial class Hug
        : Action
    {
        public Hug()
            : base()
        {
            this.DisplayName = "Hug";
        }

        public override void PerformLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.Output("You give " + args.Target.FirstName + " a warm hug.");
            }

            args.Doer.Status.SetValue("Happiness", (double)args.Doer.Status.GetValue("Happiness") + 10);
            args.Target.Status.SetValue("Happiness", (double)args.Target.Status.GetValue("Happiness") + 10);

            this.DisplayImage("person.hug.png");

            Game.Instance.MoveTime(10);
        }
    }
    public partial class Chat
        : Action
    {
        public Chat()
            : base()
        {
            this.DisplayName = "Chat";
        }

        public override void PerformLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.Output("You talk to " + args.Target.FirstName + ".");
            }

            args.Doer.Status.SetValue("Happiness", (double)args.Doer.Status.GetValue("Happiness") + 10);
            args.Target.Status.SetValue("Happiness", (double)args.Target.Status.GetValue("Happiness") + 10);

            Game.Instance.MoveTime(10);
        }
    }
    public partial class Facefuck
        : Action
    {
        public Facefuck()
            : base()
        {
            this.DisplayName = "Face Fuck";
        }

        public override void PerformLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.Output("You talk to " + args.Target.FirstName + ".");
            }

            args.Doer.Status.SetValue("Happiness", (double)args.Doer.Status.GetValue("Happiness") + 10);
            args.Target.Status.SetValue("Happiness", (double)args.Target.Status.GetValue("Happiness") + 10);

            Panel panel = (Panel)((SplitContainer)WindowController.Get<Hlife2>().Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            this.DisplayImage("person.facefuck.gif");

            Game.Instance.MoveTime(10);
        }
    }
}
