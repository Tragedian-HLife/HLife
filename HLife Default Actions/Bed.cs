using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife.Actions.Bed
{
    public partial class Sleep
        : Action
    {
        public Sleep()
            : base()
        {
            this.DisplayName = "Sleep";
            this.Description = "Sleep for an hour to regain plenty of energy.";
            this.DisabledDescription = "I'm full of enery. There's no way I could sleep now.";
            this.DisableVisible = true;
        }

        public override bool CanPerform(ActionEventArgs args)
        {
            if((double)args.Doer.Status.GetItem("Energy").Value < 100)
            {
                return true;
            }

            return false;
        }

        public override void PerformLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.DialogController.DrawDialog(new DialogControl("You crawl into the bed and fall asleep.", this, @"bed\sleep\", true));
            }
            else if (this.Witnesses.Contains(Game.Instance.Player))
            {
                //Game.Instance.DialogController.DrawDialog(new DialogControl(args.Doer.FirstName + " crawls into the bed and falls asleep.", true));
            }

            args.Doer.Status.SetValue("Energy", (double)args.Doer.Status.GetValue("Energy") + 20);
        }
    }

    public partial class Rest
        : Action
    {
        public Rest()
            : base()
        {
            this.DisplayName = "Rest";
            this.Description = "Nap for an hour to regain some energy.";
            this.DisabledDescription = "I'm full of enery. There's no way I could nap now.";
            this.DisableVisible = false;
        }

        public override bool CanPerform(ActionEventArgs args)
        {
            if ((double)args.Doer.Status.GetItem("Energy").Value < 100)
            {
                return true;
            }

            return false;
        }

        public override void PerformLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.DialogController.DrawDialog(new DialogControl("You lie back on the bed and take a short nap.", this, @"bed\sleep\", true));
            }
            else if(this.Witnesses.Contains(Game.Instance.Player))
            {
                //Game.Instance.DialogController.DrawDialog(new DialogControl(args.Doer.FirstName + " lies back on the bed and takes a short nap.", true));
            }

            args.Doer.Status.SetValue("Energy", (double)args.Doer.Status.GetValue("Energy") + 10);
        }
    }

    public partial class Masturbate
        : Action
    {
        public Masturbate()
            : base()
        {
            this.DisplayName = "Masturbate";
            this.DisableVisible = true;
        }

        public override void PerformLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.Output("You lie back on the bed and reach down to remove your pants.", true);

                if ((double)args.Doer.Status.GetValue("EjaculationsToday") < 2)
                {
                    Game.Instance.Output("Your erect cock springs free.", true);
                }
                else if ((double)args.Doer.Status.GetValue("EjaculationsToday") < 4)
                {
                    Game.Instance.Output("Your sore cock strains to rise.", true);
                }
                else
                {
                    Game.Instance.Output("Your beaten cock barely moves.", true);
                }

                Game.Instance.Output("You grip the shaft and begin masturbating.", true);

                if ((double)args.Doer.Status.GetValue("EjaculationsToday") < 2)
                {
                    Game.Instance.Output("Before long, you feel yourself approaching climax.", true);
                }
                else if ((double)args.Doer.Status.GetValue("EjaculationsToday") < 4)
                {
                    Game.Instance.Output("It takes longer than usual but you can finally feel yourself starting to climax.", true);
                }
                else
                {
                    Game.Instance.Output("You stroke your painful cock for a long time before finally giving up.", true);
                }

                this.DisplayImage("bed.masturbate.jpg");
            }

            args.Doer.Cum(Prepositions.On, args.Doer);
        }
    }
}
