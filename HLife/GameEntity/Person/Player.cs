using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HLife
{
    /// <summary>
    /// Class to handle the player character.
    /// </summary>
    public class Player 
        : Person
    {
        /// <summary>
        /// Current money.
        /// </summary>
        public float Money { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Player()
            : base()
        {
            this.Money = 0;
            
            this.Stats.SetValue("CumVolume", 6.0);
            this.Stats.SetValue("Stamina", 4.0);

            //this.LoadPlayerStats();
        }

        /// <summary>
        /// Override for MoveToLocation to handle UI changes that don't
        /// happen for AI agents.
        /// </summary>
        /// <param name="newLoc">The Location target.</param>
        protected override void MoveToLocation(Location newLoc)
        {
            base.MoveToLocation(newLoc);

            newLoc.LoadLayout();
            newLoc.LoadMap();

            Game.Instance.Date = Game.Instance.Date.AddSeconds(300);

            Game.Instance.Synchronize();
        }

        /// <summary>
        /// Override for MoveToLocation to handle UI changes that don't
        /// happen for AI agents.
        /// </summary>
        /// <param name="newLoc">The Location target.</param>
        public void MoveToLocation(Location newLoc, bool doUpdate = true)
        {
            base.MoveToLocation(newLoc);

            newLoc.LoadLayout();
            newLoc.LoadMap();

            if (doUpdate)
            {
                Game.Instance.Date = Game.Instance.Date.AddSeconds(300);

                Game.Instance.Synchronize();
            }
        }

        // TODO: Remove this.
        /// <summary>
        /// Attempts to perform an action.
        /// </summary>
        /// <param name="action">Name of action.</param>
        /// <param name="target">Target of action.</param>
        /// <param name="prop">Prop for action.</param>
        /// <returns>True if action was completed.</returns>
        public bool TryAction(string action, Guid target, string prop)
        {
            Action.Get(action).Perform(new ActionEventArgs(
                this, 
                Game.Instance.PopulationController.GetPerson(target), 
                Game.Instance.PropController.GetProp(prop)));

            return false;
        }
    }
}
