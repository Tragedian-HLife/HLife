using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows;
using System.Collections;
using HLife.Mods;
using HLife.GameEntities;

namespace HLife.Actions
{
    public class ActionController
        : Controller
    {
        public List<GameAction> Actions { get; set; }

        public ActionController()
        {
            this.Actions = new List<GameAction>();
        }

        public override void Initialize()
        {
            foreach (Mod mod in ModController.GetModsByType("Actions"))
            {
                // Get the external actions.
                // Loop over each file in the Resources\Actions\ directory...
                foreach (string file in Directory.EnumerateFiles(mod.Directory + @"\Actions\", "*.dll"))
                {
                    // Load the assembly using this file.
                    Assembly assembly = Assembly.LoadFrom(file);

                    // Add the new assembly to this domain.
                    AppDomain.CurrentDomain.Load(assembly.GetName());

                    // Get each of the namespaces and classes within this assembly.
                    IEnumerable groups = ReflectionUtilities.GetNamespacesInNamespace(assembly, "HLife.Actions");

                    // For each namespace within this assembly...
                    foreach (IGrouping<string, Type> group in groups)
                    {
                        // For each class in this namespace...
                        foreach (Type type in group.Where(e => e.BaseType == typeof(GameAction)))
                        {
                            // Create an instance of that class.
                            GameAction action = (GameAction)Activator.CreateInstance(type);
                            action.Source = mod;
                        }
                    }
                }
            }
        }

        public override void Update()
        { }

        public void Add(GameAction action)
        {
            this.Actions.Add(action);
        }

        public void Remove(GameAction action)
        {
            this.Actions.Remove(action);
        }

        public GameAction Find(Predicate<GameAction> match)
        {
            return this.Actions.Find(match);
        }

        public IEnumerable<GameAction> Where(Func<GameAction, bool> function)
        {
            return this.Actions.Where(function);
        }

        public List<GameAction> GetActionsByStat(string stat, ActionEventArgs args)
        {
            List<GameAction> actions = new List<GameAction>();

            foreach (GameAction action in Game.Instance.ActionController.Actions)
            {
                if(action.GetEffectsByStat(stat, args).Count > 0)
                {
                    actions.Add(action);
                }
            }

            return actions;
        }

        public GameAction GetMostEffectiveActionByStat(string stat, ActionEventArgs args, List<GameAction> actionPool = null)
        {
            List<GameAction> actions;

            if (actionPool == null)
            {
                actions = this.GetActionsByStat(stat, args);
            }
            else
            {
                actions = actionPool;
            }

            if(actions.Count == 0)
            {
                return null;
            }

            GameAction minAction = null;
            double minDiff = args.Doer.Stats.GetItem(stat).GetAbsoluteDifference(StatBasicStatuses.Fatal);

            foreach (GameAction action in actions)
            {
                ActionEventArgs previewArgs = action.Preview(args);

                double diff = previewArgs.Doer.Stats.GetItem(stat).GetAbsoluteDifference(StatBasicStatuses.Fatal);

                if(Math.Abs(diff) < Math.Abs(minDiff))
                {
                    minDiff = diff;
                    minAction = action;
                }
            }

            return minAction;
        }
    }
}
