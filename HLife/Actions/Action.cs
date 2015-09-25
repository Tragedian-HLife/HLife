using HLife.GameEntities.People;
using HLife.GameEntities.Props;
using HLife.Mods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace HLife.Actions
{
    public class GameAction
    {
        /// <summary>
        /// Reference name. Should be Namespace.Action.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name presented to the player.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description to show the player.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Description to display when the action cannot be performed.
        /// eg. A reason why it cannot be performed.
        /// </summary>
        public string DisabledDescription { get; set; }

        /// <summary>
        /// When action isn't performable, true to still add the
        /// context menu entry but disable it; false to not add
        /// it at all.
        /// </summary>
        public bool DisableVisible { get; set; }

        /// <summary>
        /// Time, in seconds, required to complete this action.
        /// </summary>
        public int TimeNeeded { get; set; }

        /// <summary>
        /// True if this action will be visible to the player.
        /// False if the player cannot directly choose to do this.
        /// </summary>
        public bool CanBeDoneByPlayer { get; set; }

        /// <summary>
        /// True if people can choose to do this.
        /// False if this action is only to be triggered by manual code.
        /// </summary>
        public bool CanBeDoneByAnyone { get; set; }

        /// <summary>
        /// True if the action can only be performed with a Prop.
        /// </summary>
        public bool RequiresProp { get; set; }

        /// <summary>
        /// True if the action can only be performed with a Target.
        /// </summary>
        public bool RequiresTarget { get; set; }

        /// <summary>
        /// The mod from which this action came.
        /// Useful for retrieving images.
        /// </summary>
        public Mod Source { get; set; }

        /// <summary>
        /// Effects this action will have on the doer.
        /// </summary>
        public List<ActionEffectSet> DoerActionEffects { get; set; }
        
        /// <summary>
        /// Effects this action will have on the target.
        /// </summary>
        public List<ActionEffectSet> TargetActionEffects { get; set; }
        
        /// <summary>
        /// Effects this action will have on witnesses.
        /// </summary>
        public List<ActionEffectSet> WitnessActionEffects { get; set; }

        /// <summary>
        /// Shorthand flag to check if the player is performing this action.
        /// </summary>
        protected bool DoerIsPlayer { get; set; }

        /// <summary>
        /// People at the location of the action, minus the doer and target.
        /// </summary>
        protected List<Person> Witnesses { get; set; }

        /// <summary>
        /// Custom logic to execute when this action is performed.
        /// </summary>
        [XmlIgnore]
        public System.Action<ActionEventArgs> PerformLogic { get; set; }

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

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameAction()
        {
            this.Name = this.GetType().FullName.Remove(this.GetType().FullName.IndexOf("HLife.Actions."), "HLife.Actions.".Length);
            this.DisplayName = "{ACTION}";
            this.TimeNeeded = 0;
            this.CanBeDoneByPlayer = true;
            this.CanBeDoneByAnyone = true;
            this.DoerIsPlayer = false;
            this.Witnesses = new List<Person>();

            this.DoerActionEffects = new List<ActionEffectSet>();
            this.TargetActionEffects = new List<ActionEffectSet>();
            this.WitnessActionEffects = new List<ActionEffectSet>();

            Game.Instance.ActionController.Add(this);
        }

        /// <summary>
        /// Retrieve an action by name.
        /// </summary>
        /// <param name="name">The fully-qualified type of the desired action.</param>
        /// <returns>Requested action.</returns>
        public static GameAction Get(string name)
        {
            return Game.Instance.ActionController.Find(e => e.GetType().FullName == "HLife.Actions." + name);
        }
        
        /// <summary>
        /// Retrieves all actions within a namespace.
        /// </summary>
        /// <param name="nameSpace">The namespace under HLife.Actions.</param>
        /// <returns>Requested actions.</returns>
        public static List<GameAction> GetAll(string nameSpace)
        {
            return Game.Instance.ActionController.Where(e => StringUtilities.TrimAfterLast(e.GetType().FullName, ".") == "HLife.Actions." + nameSpace).ToList();
        }

        public ActionEventArgs GetData(ActionEventArgs args)
        {
            args.Data = this.GetDataLogic(args);

            return args;
        }

        public virtual object GetDataLogic(ActionEventArgs args)
        {
            return null;
        }

        /// <summary>
        /// Custom logic to check if this action can be performed under the given conditions.
        /// This can be overridden by each action.
        /// </summary>
        /// <param name="args">Conditions to check.</param>
        /// <returns>True if can be performed.</returns>
        public bool CanPerform(ActionEventArgs args)
        {
            args.Stage = ActionStages.CanPerform;
            args = this.GetData(args);

            return this.CanPerformLogic(args);
        }

        public virtual bool CanPerformLogic(ActionEventArgs args)
        {
            return true;
        }

        /// <summary>
        /// Perform the action under the given conditions.
        /// </summary>
        /// <param name="args">Conditions under which to perform the action.</param>
        public void Perform(ActionEventArgs args)
        {
            args.Stage = ActionStages.Perform;
            args = this.GetData(args);

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


            this.DoerActionEffects.ForEach(e => e.CheckCondition(args).First().Apply(args.Doer));
            this.TargetActionEffects.ForEach(e => e.CheckCondition(args).First().Apply(args.Target));
            this.Witnesses.ForEach(w => this.WitnessActionEffects.ForEach(e => e.CheckCondition(args).First().Apply(w)));


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

        /// <summary>
        /// Preview the effects that this action will have when performed.
        /// </summary>
        /// <param name="args">Conditions to preview.</param>
        /// <returns>Conditions after the action.</returns>
        public ActionEventArgs Preview(ActionEventArgs args, bool includeWitnesses = false)
        {
            args.Stage = ActionStages.Preview;
            args = this.GetData(args);

            // Deep copy the arguments.
            ActionEventArgs previewArgs = new ActionEventArgs(
                MiscUtilities.CloneJson<Person>(args.Doer), 
                MiscUtilities.CloneJson<Person>(args.Target),
                MiscUtilities.CloneJson<Prop>(args.Prop));

            // Apply the action to the copied arguments.
            this.DoerActionEffects.ForEach(e => e.CheckCondition(previewArgs).First().Apply(previewArgs.Doer));
            this.TargetActionEffects.ForEach(e => e.CheckCondition(previewArgs).First().Apply(previewArgs.Target));

            // If we should preview all of the witnesses, as well...
            if (includeWitnesses)
            {
                // TODO: This will require creating publically-accessible copies of the witness list.

                // Apply the action to each witness.
                //this.Witnesses.ForEach(w => this.WitnessActionEffects.ForEach(e => e.CheckCondition(args).First().Apply(MiscUtilities.CloneJson<Person>(w))));
            }
            
            // Return the affected arguments.
            return previewArgs;
        }

        #region Events

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

        #endregion
        
        /// <summary>
        /// Retrieve the MenuItem for Prop context menus.
        /// </summary>
        /// <param name="args">The conditions to check for validity.</param>
        /// <returns>MenuItem of this action.</returns>
        public MenuItem GetMenuItemForProp(ActionEventArgs args)
        {
            args.Stage = ActionStages.Preview;

            // If the player can't choose to do this action...
            if (!this.CanBeDoneByPlayer)
            {
                // We don't want to add anything to the list.
                return null;
            }

            MenuItem displayAction = new MenuItem();
            displayAction.Header = this.DisplayName;

            // This action should be performed with the provided arguments when clicked.
            displayAction.Click += (sender, e) => this.Perform(args);

            ToolTip tooltip = new ToolTip();

            // If the arguments allow for the action to be performed...
            if (this.CanPerform(args))
            {
                // Set the tooltip to the performable description.
                tooltip.Content = this.Description;
            }
            // Else, if we should display the action despite being unperformable...
            else if (this.DisableVisible)
            {
                // Set the tooltip to the disabled description.
                tooltip.Content = this.DisabledDescription;

                // Disable the MenuItem.
                displayAction.IsEnabled = false;
            }
            // Else, we don't want to add this action at all.
            else
            {
                // Don't add anything to the list.
                displayAction = null;
            }

            // Set the tooltip.
            displayAction.ToolTip = tooltip;

            // Return the MenuItem.
            return displayAction;
        }

        /// <summary>
        /// Retrieve the MenuItem for Person context menus.
        /// </summary>
        /// <param name="args">The conditions to check for validity.</param>
        /// <returns>MenuItem of this action.</returns>
        public MenuItem GetMenuItemForPerson(ActionEventArgs args)
        {
            args.Stage = ActionStages.Preview;

            // If the player can't choose to do this action...
            if (!this.CanBeDoneByPlayer)
            {
                // We don't want to add anything to the list.
                return null;
            }

            MenuItem displayAction = new MenuItem();
            displayAction.Header = this.DisplayName;

            // This action should be performed with the provided arguments when clicked.
            displayAction.Click += (sender, e) => this.Perform(args);

            ToolTip tooltip = new ToolTip();

            // If the arguments allow for the action to be performed...
            if (this.CanPerform(args))
            {
                // Set the tooltip to the performable description.
                tooltip.Content = this.Description;
            }
            // Else, if we should display the action despite being unperformable...
            else if (this.DisableVisible)
            {
                // Set the tooltip to the disabled description.
                tooltip.Content = this.DisabledDescription;

                // Disable the MenuItem.
                displayAction.IsEnabled = false;
            }
            // Else, we don't want to add this action at all.
            else
            {
                // Don't add anything to the list.
                displayAction = null;
            }

            // Set the tooltip.
            displayAction.ToolTip = tooltip;

            // Return the MenuItem.
            return displayAction;
        }

        /// <summary>
        /// Retrieve all of the ActionEffects that affect the given stat.
        /// </summary>
        /// <param name="stat">Name of the stat.</param>
        /// <param name="args">Conditions to check. Used in EffectSet.Condition().</param>
        /// <returns>All actions that can affect the stat.</returns>
        public List<ActionEffect> GetEffectsByStat(string stat, ActionEventArgs args = null)
        {
            List<ActionEffect> effects = new List<ActionEffect>();

            // For each of the effect sets in this action...
            foreach(ActionEffectSet effectSet in this.DoerActionEffects)
            {
                // For each of the effects in this set...
                foreach (ActionEffect effect in effectSet.Effects)
                {
                    // If this effect deals with the given stat...
                    if(effect.ItemName == stat)
                    {
                        // If we have arguments to check against...
                        if(args != null)
                        {
                            args.Stage = ActionStages.Passive;

                            // If the effect is performable under these conditions...
                            if (effect.Condition(args))
                            {
                                // Add the effect to the list.
                                effects.Add(effect);
                            }
                        }
                        // Else, assume the effect is performable...
                        else
                        {
                            // Add the effect to the list.
                            effects.Add(effect);
                        }
                    }
                }
            }

            return effects;
        }

        /// <summary>
        /// Calculate a set of estimated costs required to perform this action.
        /// </summary>
        /// <param name="args">Conditions to check.</param>
        /// <returns>Set of estimated costs.</returns>
        public ActionCost EstimateCostToPerform(ActionEventArgs args)
        {
            args.Stage = ActionStages.Preview;
            args = this.GetData(args);

            ActionCost cost = new ActionCost();

            // If we were given a prop...
            if (args.Prop != null)
            {
                // Get number of nodes between the doer and the prop.
                cost.NodalDistance = Math.Abs(args.Doer.Location.NodalDistance(args.Prop.Location));

                // Get shortest travel time.
                cost.TravelTime = args.Doer.Location.TravelTime(args.Prop.Location);
            }
            // Else, if this action requires a prop...
            else if (this.RequiresProp)
            {
                // Search for any usable props within the Doer's location.
                List<Prop> viableLocalProps = Game.Instance.PropController.GetPropsByAction(this, args.Doer.Location)
                    .Where(e => e.Occupants.Count < e.MaxOccupancy).ToList();

                // If there are no viable props at this location...
                if (viableLocalProps.Count == 0)
                {
                    // Search for any usable props anywhere.
                    List<Prop> viableRemoteProps = Game.Instance.PropController.GetPropsByAction(this)
                        .Where(e => e.Occupants.Count < e.MaxOccupancy).ToList();

                    // If there are no viable props at any location...
                    if (viableRemoteProps.Count == 0)
                    {
                        // We're fucked. Let the requester know this action cannot be performed.
                        return null;
                    }
                    // If there are viable props at any location...
                    else
                    {
                        // Get the closest prop to the Doer, by travel time.
                        Prop closestProp = Game.Instance.PropController.GetClosestProp(viableRemoteProps, args.Doer.Location);

                        // We don't have any travel time.
                        cost.NodalDistance = args.Doer.Location.NodalDistance(closestProp.Location);
                        cost.TravelTime = args.Doer.Location.TravelTime(closestProp.Location);
                    }
                }
                // If there are viable props at this location...
                else
                {
                    // We don't have any travel time.
                    cost.NodalDistance = 0;
                    cost.TravelTime = 0;
                }
            }

            // If we are given a Target...
            if (args.Target != null)
            {
                // Get number of nodes between the doer and the target.
                cost.NodalDistance = Math.Abs(args.Doer.Location.NodalDistance(args.Target.Location));

                // Get shortest travel time.
                cost.TravelTime = args.Doer.Location.TravelTime(args.Target.Location);
            }
            // Else, if this action requires a target...
            else if (this.RequiresTarget)
            {
                // TODO: Ability to find a viable Target.
                //      This will eventually need to account for
                //      relationship status, avoiding fatal outcomes
                //      (eg. don't do something that will get the cops
                //      involved.), etc.
                throw new NotImplementedException();
            }

            // Get the time required to complete this action.
            cost.PerformanceTime = this.TimeNeeded;

            return cost;
        }
    }
}
