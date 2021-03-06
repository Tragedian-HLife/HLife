﻿using HLife.Choices;
using HLife.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace HLife.Actions.Bed
{
    public partial class Sleep
        : GameAction
    {
        public Sleep()
            : base()
        {
            this.DisplayName = "Sleep";

            this.Description            = "Sleep for an hour to regain plenty of energy.";
            this.DisabledDescription    = "I'm full of enery. There's no way I could sleep now.";
            this.DisableVisible         = true;

            this.RequiresProp = true;
            this.RequiresTarget = false;
            
            this.DoerActionEffects.Add(new ActionEffectSet(new ActionEffect("Energy", 20)));

            this.PerformLogic = MyLogic;
        }

        public override bool CanPerformLogic(ActionEventArgs args)
        {
            if(args.Doer.Stats.GetValue<double>("Energy") < 100)
            {
                return true;
            }

            return false;
        }

        public void MyLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                DialogGroup group = new DialogGroup();

                Dialog dialog = new Dialog();
                dialog.RawText = "You crawl into the bed and fall asleep.";
                dialog.Image = Game.Instance.ResourceController.GetActionImage(this, @"bed\sleep\", true);
                dialog.AddBeginEffect(new GUI.Effects.FadeColorOut());
                dialog.AddEndEffect(new GUI.Effects.FadeColorIn());
                dialog.Choices.Add(new DialogChoice("Test", new Action(() => { Game.Instance.Player.Stats.SetValue("Happiness", 0.0); })));
                dialog.Choices.Add(new DialogChoice("Test2", new Action(() => 
                {
                    Game.Instance.Player.Stats.SetValue("Happiness", 50.0);

                    dialog.ClearEndEffects();

                    dialog.AddEndEffect(new GUI.Effects.Flash(Colors.White));
                    dialog.AddEndEffect(new GUI.Effects.Flash(Colors.Red), 1);
                    dialog.AddEndEffect(new GUI.Effects.Flash(Colors.Blue), 2);
                    dialog.AddEndEffect(new GUI.Effects.Flash(Colors.Yellow), 3);
                    dialog.AddEndEffect(new GUI.Effects.Flash(Colors.Green), 4);
                    dialog.AddEndEffect(new GUI.Effects.Flash(Colors.White), 5);

                    dialog.AddEndEffect(new GUI.Effects.FadeColorIn(), 10);
                })));
                dialog.Choices.Add(new DialogChoice("Test3", new Action(() => { Game.Instance.Player.Stats.SetValue("Happiness", 100.0); })));
                group.Entries.Add(dialog);
                
                Dialog dialog2 = new Dialog();
                dialog2.RawText = "You wake up.";
                dialog2.AddBeginEffect(new GUI.Effects.FadeColorOut());
                dialog2.AddEndEffect(new GUI.Effects.FadeColorIn());
                group.Entries.Add(dialog2);

                Dialog dialog3 = new Dialog();
                dialog3.RawText = "You crawl into the bed and fall asleep.";
                dialog3.Image = Game.Instance.ResourceController.GetActionImage(this, @"bed\sleep\", true);
                dialog3.AddBeginEffect(new GUI.Effects.FadeColorOut());
                dialog3.AddEndEffect(new GUI.Effects.FadeColorIn());
                group.Entries.Add(dialog3);

                Game.Instance.DialogController.DrawDialog(group);
            }
            else if (this.Witnesses.Contains(Game.Instance.Player))
            {
                //Game.Instance.DialogController.DrawDialog(new DialogControl(args.Doer.FirstName + " crawls into the bed and falls asleep.", true));
            }
        }
    }

    public partial class Rest
        : GameAction
    {
        public Rest()
            : base()
        {
            this.DisplayName = "Rest";
            this.Description = "Nap for an hour to regain some energy.";
            this.DisabledDescription = "I'm full of enery. There's no way I could nap now.";
            this.DisableVisible = false;

            this.RequiresProp = true;
            this.RequiresTarget = false;

            this.DoerActionEffects.Add(new ActionEffectSet(new ActionEffect("Energy", 10)));

            this.PerformLogic = MyLogic;
        }

        public override bool CanPerformLogic(ActionEventArgs args)
        {
            if ((double)args.Doer.Stats.GetItem("Energy").Value < 100)
            {
                return true;
            }

            return false;
        }

        public void MyLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                DialogGroup group = new DialogGroup();
                Dialog dialog = new Dialog();
                dialog.RawText = "You lie back on the bed and take a short nap.";
                dialog.Image = Game.Instance.ResourceController.GetActionImage(this, @"bed\sleep\", true);
                group.Entries.Add(dialog);

                Game.Instance.DialogController.DrawDialog(group);
            }
            else if(this.Witnesses.Contains(Game.Instance.Player))
            {
                //Game.Instance.DialogController.DrawDialog(new DialogControl(args.Doer.FirstName + " lies back on the bed and takes a short nap.", true));
            }
        }
    }

    public partial class Masturbate
        : GameAction
    {
        public Masturbate()
            : base()
        {
            this.DisplayName = "Masturbate";
            this.DisableVisible = true;

            this.RequiresProp = true;
            this.RequiresTarget = false;

            this.DoerActionEffects.Add(new ActionEffectSet(new ActionEffect("Energy", -10)));
            this.DoerActionEffects.Add(new ActionEffectSet(new ActionEffect("EjaculationsToday", 1)));
            this.DoerActionEffects.Add(new ActionEffectSet(new ActionEffect("CumVolume", 0.5, true, true)));

            this.PerformLogic = MyLogic;
        }

        public void MyLogic(ActionEventArgs args)
        {
            if (DoerIsPlayer)
            {
                Game.Instance.Output("You lie back on the bed and reach down to remove your pants.", true);

                if (args.Doer.Stats.GetValue<double>("EjaculationsToday") < 2)
                {
                    Game.Instance.Output("Your erect cock springs free.", true);
                }
                else if (args.Doer.Stats.GetValue<double>("EjaculationsToday") < 4)
                {
                    Game.Instance.Output("Your sore cock strains to rise.", true);
                }
                else
                {
                    Game.Instance.Output("Your beaten cock barely moves.", true);
                }

                Game.Instance.Output("You grip the shaft and begin masturbating.", true);

                if (args.Doer.Stats.GetValue<double>("EjaculationsToday") < 2)
                {
                    Game.Instance.Output("Before long, you feel yourself approaching climax.", true);
                }
                else if (args.Doer.Stats.GetValue<double>("EjaculationsToday") < 4)
                {
                    Game.Instance.Output("It takes longer than usual but you can finally feel yourself starting to climax.", true);
                }
                else
                {
                    Game.Instance.Output("You stroke your painful cock for a long time before finally giving up.", true);
                }

                //this.DisplayImage("bed.masturbate.jpg");
            }

            //args.Doer.Cum(Prepositions.On, args.Doer);
        }
    }
}
