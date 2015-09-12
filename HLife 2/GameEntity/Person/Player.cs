using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
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

            this.Physique.CumVolume = 6;
            this.Status.SetValue("CumVolume", this.Physique.CumVolume);

            this.Attributes.Stamina = 4;

            //this.LoadPlayerStats();
        }

        /// <summary>
        /// Updates the PlayerInfoWindow.
        /// </summary>
        public void LoadPlayerStats()
        {
            Form frm = WindowController.Get<PlayerInfoWindow>(Game.Instance.Player);
            TabControl tabs = (TabControl)frm.Controls.Find("tabs_Container", false)[0];

            ((ProgressBar)tabs.TabPages[0].Controls.Find("pgb_CumVolume", false)[0]).Maximum = (int)this.Physique.CumVolume;
            //((ProgressBar)tabs.TabPages[0].Controls.Find("pgb_cumVolume", false)[0]).Value = (int)this.Status.CumVolume;
        }

        /// <summary>
        /// Override for MoveToLocation to handle UI changes that don't
        /// happen for AI agents.
        /// </summary>
        /// <param name="newLoc">The Location target.</param>
        protected override void MoveToLocation(Location newLoc)
        {
            base.MoveToLocation(newLoc);

            Game.Instance.Date = Game.Instance.Date.AddSeconds(300);

            newLoc.LoadLayout();
            newLoc.LoadMap();

            Game.Instance.Synchronize();
            
            Game.Instance.PropController.PopulatePropList();
            Game.Instance.PersonController.PopulatePersonList();
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
                Game.Instance.PersonController.GetPerson(target), 
                Game.Instance.PropController.GetProp(prop)));

            this.LoadPlayerStats();

            return false;
        }
    }
}
