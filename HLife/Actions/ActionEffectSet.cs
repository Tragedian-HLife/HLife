using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife.Actions
{
    /// <summary>
    /// Contains a set of related effects.
    /// These effects are mutually exclusive and should represent different
    /// conditions within each effect's Condition().
    /// </summary>
    public class ActionEffectSet
    {
        /// <summary>
        /// Effects in this set.
        /// </summary>
        public List<ActionEffect> Effects { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActionEffectSet()
        {
            this.Effects = new List<ActionEffect>();
        }

        /// <summary>
        /// Constructor to initialize the set with a single effect.
        /// </summary>
        /// <param name="effect">Effect to add.</param>
        public ActionEffectSet(ActionEffect effect)
            : this()
        {
            this.Effects.Add(effect);
        }

        /// <summary>
        /// Retrieve all effects, if any, that matches the conditions given.
        /// </summary>
        /// <param name="args">Conditions against which to check.</param>
        /// <returns>All effects, if any, that match the conditions.</returns>
        public List<ActionEffect> CheckCondition(ActionEventArgs args)
        {
            // If we have no effects...
            if (this.Effects == null || this.Effects.Count == 0)
            {
                // Fuck off.
                return null;
            }
            
            List<ActionEffect> results = new List<ActionEffect>();

            // Foreach of my effects...
            foreach (ActionEffect effect in this.Effects)
            {
                // If the effect matches the conditions given...
                if (effect.Condition(args))
                {
                    // Add it to the list.
                    results.Add(effect);
                }
            }

            return results;
        }
    }
}
