using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows;
using System.Collections;

namespace HLife
{
    public class ActionController
        : Controller
    {
        public List<Action> Actions { get; set; }

        public ActionController()
        {
            this.Actions = new List<Action>();
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
                        foreach (Type type in group.Where(e => e.BaseType == typeof(Action)))
                        {
                            // Create an instance of that class.
                            Action action = (Action)Activator.CreateInstance(type);
                            action.Source = mod;
                        }
                    }
                }
            }
        }

        public override void Update()
        { }

        public void Add(Action action)
        {
            this.Actions.Add(action);
        }

        public void Remove(Action action)
        {
            this.Actions.Remove(action);
        }

        public Action Find(Predicate<Action> match)
        {
            return this.Actions.Find(match);
        }

        public IEnumerable<Action> Where(Func<Action, bool> function)
        {
            return this.Actions.Where(function);
        }

        public List<Action> GetActionsByStat(string stat, ActionEventArgs args)
        {
            List<Action> actions = new List<Action>();

            foreach (Action action in Game.Instance.ActionController.Actions)
            {
                if(action.GetEffectsByStat(stat, args).Count > 0)
                {
                    actions.Add(action);
                }
            }

            return actions;
        }

        public Action GetMostEffectiveActionByStat(string stat, ActionEventArgs args, List<Action> actionPool = null)
        {
            List<Action> actions;

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

            Action minAction = null;
            double minDiff = args.Doer.Stats.GetItem(stat).GetAbsoluteDifference(StatBasicStatuses.Fatal);

            foreach (Action action in actions)
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
