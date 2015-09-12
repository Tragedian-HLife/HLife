using AI.Fuzzy.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public class AIAgent
    {
        public Person Agent { get; set; }

        public NavAgent NavAgent { get; set; }

        public RelationalAgent RelationalAgent { get; set; }

        public AIAgent()
        {
            this.NavAgent = new NavAgent(this);
            this.RelationalAgent = new RelationalAgent(this);
        }

        public AIAgent(Person agent)
            : this()
        {
            this.Agent = agent;
        }

        public void Update()
        {
            this.NavAgent.Update();
            

            if (this.Agent.Status.GetValue<double>("Energy") < 20)
            {
                Prop bed = this.Agent.Location.Inventory.FindLast(e => e.Template.Categories.Contains("Bed"));

                this.UseProp(bed);
            }
        }

        protected Prop LookForProp(GameEntity target)
        {
            Prop temp = (Prop)target.Inventory[MiscUtilities.Rand.Next(target.Inventory.Count)];

            return temp;
        }

        protected Action UseProp(Prop prop)
        {
            Action action = null;

            if (prop.Template.Actions.Count > 0)
            {
                action = Action.Get(MiscUtilities.GetRandomEntry<string>(prop.Template.Actions));

                action.Perform(new ActionEventArgs(this.Agent, null, prop));
            }

            return action;
        }

        protected void TakeProp(GameEntity target, Prop prop)
        {
            this.Agent.Inventory.Add(prop);
            target.Inventory.Remove(prop);
        }

        protected void PlaceProp(GameEntity target, Prop prop)
        {
            this.Agent.Inventory.Remove(prop);
            target.Inventory.Add(prop);
        }

        protected void LookForLocation()
        { }

        private void FuzzyExample()
        {
            // Get the specified output variable.
            double result = FuzzyLogicUtilities.GetResult
            (
                // Add the ruleset.
                FuzzyLogicUtilities.AddRules
                (
                    // Create the fuzzy logic system.
                    FuzzyLogicUtilities.CreateMamdaniSystem
                    (
                        // Create the input sets.
                        new List<FuzzyVariable>()
                        {
                            FuzzyLogicUtilities.CreateSymmetricTriangularVariable("service", 0, 10, "poor", "good", "excellent"),
                            FuzzyLogicUtilities.CreateSymmetricTrapezoidalVariable("food", 0, 10, "rancid", "normal", "delicious")
                        },

                        // Create the output sets.
                        new List<FuzzyVariable>()
                        {
                            FuzzyLogicUtilities.CreateSymmetricTriangularVariable("tip", 0, 30, "cheap", "average", "generous")
                        }
                    ),

                    // Define the rules.
                    "if (service is poor)  or (food is rancid) then (tip is cheap)",
                    "if (service is good) then (tip is average)",
                    "if (service is excellent) or (food is delicious) then (tip is generous)"
                ),

                // Specify the output variable's name.
                "tip",

                // Set the input values.
                "service".Pair(5),
                "food".Pair(5)
            );

            Debug.WriteLine(result.ToString("f1"));
        }
    }
}
