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

            this.DoerActionEffects.Add(new ActionEffectSet(new ActionEffect("Happiness", 10)));
            this.TargetActionEffects.Add(new ActionEffectSet(new ActionEffect("Happiness", 10)));

            this.PerformLogic = MyLogic;
        }

        public void MyLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.DialogController.DrawDialog(new DialogControl("You give " + args.Target.FirstName + " a warm hug.", this, @"person\hug\",  true));
            }
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

            this.DoerActionEffects.Add(new ActionEffectSet(new ActionEffect("Happiness", 5)));
            this.TargetActionEffects.Add(new ActionEffectSet(new ActionEffect("Happiness", 5)));

            this.PerformLogic = MyLogic;
        }

        public void MyLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.DialogController.DrawDialog(new DialogControl("You talk to " + args.Target.FirstName + ".", true));
            }
        }
    }
}
