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

            this.RequiresProp = false;
            this.RequiresTarget = true;

            this.DoerActionEffects.Add(new ActionEffectSet(new ActionEffect("Happiness", 10)));
            this.TargetActionEffects.Add(new ActionEffectSet(new ActionEffect("Happiness", 10)));

            this.PerformLogic = MyLogic;
        }

        public override object GetDataLogic(ActionEventArgs args)
        {
            switch(args.Stage)
            {
                default:
                case ActionStages.Passive:
                    return null;

                case ActionStages.CanPerform:
                    return args.Target.AIAgent.RelationalAgent.Relationships;

                case ActionStages.Preview:
                    return null;

                case ActionStages.Perform:
                    return args.Target.AIAgent.RelationalAgent.Relationships;
            }
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

            this.RequiresProp = false;
            this.RequiresTarget = true;

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

    public partial class Move
        : Action
    {
        public Move()
            : base()
        {
            this.CanBeDoneByPlayer = false;
            this.CanBeDoneByAnyone = false;

            this.DisplayName = "Move";
            this.TimeNeeded = 0;

            this.RequiresProp = false;
            this.RequiresTarget = false;

            this.PerformLogic = MyLogic;
        }

        public void MyLogic(ActionEventArgs args)
        {
            //args.Doer.AIAgent.NavAgent.PathfindTo()
        }
    }
}
