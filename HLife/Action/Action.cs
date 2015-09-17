using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLife
{
    public abstract class Action
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TimeNeeded { get; set; }

        public string DisabledDescription { get; set; }

        public string DisplayName { get; set; }

        public bool CanBeDoneByPlayer { get; set; }

        public bool CanBeDoneByOthers { get; set; }

        public Mod Source { get; set; }

        /// <summary>
        /// When action isn't performable, true to still add the
        /// context menu entry but disable it; false to not add
        /// it at all.
        /// </summary>
        public bool DisableVisible { get; set; }

        public bool DoerIsPlayer { get; set; }

        /// <summary>
        /// People at the location of the action, minus the doer and target.
        /// </summary>
        public List<Person> Witnesses { get; protected set; }

        /// <summary>
        /// Fired when the Perform method has completed execution.
        /// </summary>
        public event EventHandler<ActionEventArgs> Performed;

        /// <summary>
        /// Fired when the Perform method has been called but before it has executed.
        /// </summary>
        public event EventHandler<ActionEventArgs> Performed_PreExecution;

        /// <summary>
        /// Fired just before the Perform method calls the PerformLogic method.
        /// </summary>
        public event EventHandler<ActionEventArgs> Performed_PreCustomLogic;

        /// <summary>
        /// Fired just after the Perform method executes the PerformLogic method.
        /// </summary>
        public event EventHandler<ActionEventArgs> Performed_PostCustomLogic;

        public Action()
        {
            this.Name = this.GetType().FullName.Remove(this.GetType().FullName.IndexOf("HLife.Actions."), "HLife.Actions.".Length);
            this.DisplayName = "{ACTION}";
            this.TimeNeeded = 0;
            this.CanBeDoneByPlayer = true;
            this.CanBeDoneByOthers = true;
            this.DoerIsPlayer = false;
            this.Witnesses = new List<Person>();

            Game.Instance.ActionController.Add(this);
        }

        public static Action Get(string name)
        {
            return Game.Instance.ActionController.Find(e => e.GetType().FullName == "HLife.Actions." + name);
        }
        
        public static List<Action> GetAll(string nameSpace)
        {
            return Game.Instance.ActionController.Where(e => StringUtilities.TrimAfterLast(e.GetType().FullName, ".") == "HLife.Actions." + nameSpace).ToList();
        }

        public virtual bool CanPerform(ActionEventArgs args)
        {
            return true;
        }

        // TODO: Record the outcome/delta of the action.
        public void Perform(ActionEventArgs args)
        {
            // Tigger PrePerform event.
            this.PrePerform(args);

            // Figure out if the doer is the player, since many actions will likely need this.
            this.DoerIsPlayer = args.Doer.Id == Game.Instance.Player.Id;

            // Get the list of nearby people who aren't the doer or target.
            this.Witnesses = Game.Instance.PopulationController.GetPeopleAtLocation(args.Doer.Location).Where(e => e != args.Doer && e != args.Target && e != Game.Instance.Player).ToList();


            // Trigger the PreLogic event.
            this.PreLogicPerform(args);

            // Do custom action logic.
            this.PerformLogic(args);

            // Trigger the PostLogic event.
            this.PostLogicPerform(args);

            // If there is a target Person...
            if (args.Target != null)
            {
                // If the target doesn't have a Relationship with the doer...
                if (args.Target.AIAgent.RelationalAgent[args.Doer] == null)
                {
                    // Add the relationship.
                    args.Target.AIAgent.RelationalAgent.Relationships.Add(args.Doer, new Relationship(args.Doer));
                }

                // Add the action to the target's Relationship history.
                args.Target.AIAgent.RelationalAgent[args.Doer].ActionHistoryReceived.Add(this);



                // If the doer doesn't have a Relationship with the target...
                if (args.Doer.AIAgent.RelationalAgent[args.Target] == null)
                {
                    // Add the relationship.
                    args.Doer.AIAgent.RelationalAgent.Relationships.Add(args.Target, new Relationship(args.Target));
                }

                // Add the action to the doer's Relationship history.
                args.Doer.AIAgent.RelationalAgent[args.Target].ActionHistoryReceived.Add(this);
            }

            if (DoerIsPlayer)
            {
                Game.Instance.MoveTime(this.TimeNeeded);

                // Update the game state.
                Game.Instance.Update();
            }

            // Trigger the PostPerform event.
            this.PostPerform(args);
        }

        public abstract void PerformLogic(ActionEventArgs argss);

        protected virtual void PrePerform(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> handler = Performed_PreExecution;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void PreLogicPerform(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> handler = Performed_PreCustomLogic;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void PostLogicPerform(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> handler = Performed_PostCustomLogic;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void PostPerform(ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> handler = Performed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void DisplayImage(string image)
        {
            Grid panel = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            MediaElement pb = new MediaElement();
            pb.Width = panel.Width;
            pb.Height = panel.Height;
            pb.Stretch = System.Windows.Media.Stretch.Uniform;
            pb.Source = Game.Instance.ResourceController.GetActionImage(this, image, true);
            //pb.Click += (sender, e) => panel.Controls.Remove(pb);
            panel.Children.Add(pb);
        }
        
        public MenuItem GetContextMenuItem(ActionEventArgs args)
        {
            MenuItem displayAction = new MenuItem();
            displayAction.Header = this.DisplayName;

            displayAction.Click += (sender, e) => Game.Instance.PropController.HandlePropAction(args.Doer, args.Prop, this, args.Target);

            if (this.CanPerform(args))
            {
                //displayAction.Text = this.Description;
            }
            else if (this.DisableVisible)
            {
                //displayAction.Header = this.DisabledDescription;

                //displayAction.Enabled = false;
            }
            else
            {
                displayAction = null;
            }

            return displayAction;
        }

        public MenuItem GetContextMenuItemPerson(ActionEventArgs args)
        {
            MenuItem displayAction = new MenuItem();
            displayAction.Header = this.DisplayName;
            
            displayAction.Click += (sender, e) => Game.Instance.Player.HandlePropAction(Game.Instance.Player, args.Prop, this, args.Target);

            if (this.CanPerform(args))
            {
                //displayAction.Text = this.Description;
            }
            else if (this.DisableVisible)
            {
                //displayAction.Header = this.DisabledDescription;

                //displayAction.Enabled = false;
            }
            else
            {
                displayAction = null;
            }

            return displayAction;
        }
    }
}
