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
            List<Mod> actionMods = ModController.ModsEnabled.Where(e => e.Type == "Actions").ToList();

            foreach (Mod mod in actionMods)
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
                    IEnumerable groups = ReflectiveUtilities.GetNamespacesInNamespace(assembly, "HLife.Actions");

                    // For each namespace within this assembly...
                    foreach (IGrouping<string, Type> group in groups)
                    {
                        // For each class in this namespace...
                        foreach (Type type in group)
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

        public void PopulateActionList()
        {
            /*
            GameController.game.windowController.GetWindow<ActionWindow>().actions.Items.Clear();

            GameController.game.windowController.GetWindow<ActionWindow>().actions.Items.Add("");

            GameController.game.windowController.GetWindow<ActionWindow>().actions.Items.AddRange(this.actions.Select(e => e.DisplayName).ToArray());
            */
        }
    }
}
