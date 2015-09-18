using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HLife.Actions.Person
{
    public partial class Hug
        : Action
    {
        public Hug()
            : base()
        {
            this.DisplayName = "Hug";
            this.TimeNeeded = 10;
        }

        public override void PerformLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.DialogController.DrawDialog(new DialogControl("You give " + args.Target.FirstName + " a warm hug.", this, @"person\hug\",  true));
            }

            args.Doer.Stats.SetValue("Happiness", 10, true);
            args.Target.Stats.SetValue("Happiness", 10, true);
        }
    }

    public partial class Chat
        : Action
    {
        public Chat()
            : base()
        {
            this.DisplayName = "Chat";
            this.TimeNeeded = 60;
        }

        public override void PerformLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.DialogController.DrawDialog(new DialogControl("You talk to " + args.Target.FirstName + ".", true));
            }

            args.Doer.Stats.SetValue("Happiness", 10, true);
            args.Target.Stats.SetValue("Happiness", 10, true);
        }
    }
}
