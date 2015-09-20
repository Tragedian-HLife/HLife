﻿using AI.Fuzzy.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
            // Rankings:
            //  1. Needs
            //  2. Schedule
            //  3. Desires
            //
            // Needs:
            //  1. Fix any needs that are in dangerous zones.
            //  2. Else, fix any needs that are in uncomfortable zones.
            //  Proceedure:
            //      1. Look for any dangerous needs. If found, skip to 3.
            //      1. Look for any uncomfortable needs. If found, skip to 3.
            //      3. Get the most imminent need.
            //      4. Figure out what should happen to fix it.
            //      5. Plan the steps to make it happen.
            //
            // Schedule:
            //  1. Pop the next schedule item.
            //
            // Desires:
            //  1. Weigh a desire's preference against the time or resources needed to accomplish it.
            //  Proceedure:
            //      1. Loop through each desire, sorted by preference.
            //      2. Get the estimated cost to execute that desire.
            //      3. Pick the desire with the smallest cost/benefit ratio.
            //      4. Plan the steps to make it happen.


            
            // If the agent has a destination...
            if (this.NavAgent.Path.Count > 0)
            {
                // Keep going there.
                this.NavAgent.Update();
            }
            else
            {
                Stat worstStat = this.SearchForNeeds();

                if (worstStat != null)
                {
                    List<Action> candidates = this.GetCandidateActions(worstStat.Name);

                    Action bestCandidate = this.GetBestCandidateAction(worstStat.Name, candidates);

                    if(bestCandidate != null)
                    {
                        List<Prop> viableLocalProps = Game.Instance.PropController.GetPropsByAction(bestCandidate, this.Agent.Location);
                        viableLocalProps = viableLocalProps.Where(e => e.Occupants.Count < e.MaxOccupancy).ToList();

                        if(viableLocalProps.Count > 0)
                        {
                            bestCandidate.Perform(new ActionEventArgs(this.Agent, null, viableLocalProps.First()));
                        }
                    }
                }

                if (this.Agent.Stats.GetValue<double>("Energy") < 20)
                {
                    //Prop bed = this.Agent.Location.Inventory.FindLast(e => e.Template.Categories.Contains("Bed"));

                    //this.UseProp(bed);
                }
                else
                {
                    // Find a new destination.
                    //this.NavAgent.PathfindTo(this.LookForLocation());

                    // Start moving there.
                    //this.NavAgent.Update();
                }
            }
        }

        protected Stat SearchForNeeds()
        {
            Stat worstStat = this.Agent.Stats.GetWorstStat(true);

            return worstStat;
        }

        protected Prop LookForProp(GameEntity target)
        {
            Prop temp = (Prop)target.Inventory[MiscUtilities.Rand.Next(target.Inventory.Count)];

            return temp;
        }

        protected Action UseProp(Prop prop)
        {
            Action action = null;

            if (prop != null && prop.Template.Actions.Count > 0)
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

        protected Location LookForLocation()
        {
            return Location.Get(MiscUtilities.GetRandomEntry<LocationNetworkEdge>(this.Agent.Location.Edges).Node);
        }

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

        protected Action GetBestCandidateAction(string stat, List<Action> candidateActions)
        {
            return Game.Instance.ActionController.GetMostEffectiveActionByStat(stat, new ActionEventArgs(this.Agent, null, null), candidateActions);
        }

        protected List<Action> GetCandidateActions(string stat)
        {
            return Game.Instance.ActionController.GetActionsByStat(stat, new ActionEventArgs(this.Agent, null, null));
        }
    }
}
