using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace HLife
{
    public partial class GameEntity
    {
        public Guid Id { get; set; }

        public string Image { get; set; }
        
        public List<Prop> Inventory { get; set; }

        public List<Person> Occupants { get; set; }

        public int MaxOccupancy { get; set; }

        public List<KeyValuePair<Action, ActionEventArgs>> ActionsReceived { get; set; }

        public List<KeyValuePair<Action, ActionEventArgs>> ActionsPerformed { get; set; }

        private Location _location;

        public Location Location
        {
            get
            {
                return this._location;
            }

            set
            {
                if(value != null)
                {
                    this.MoveToLocation(value);
                }
            }
        }

        /// <summary>
        /// Personal status.
        /// </summary>
        public Stats Stats { get; set; }

        public GameEntity()
        {
            this.Id = Guid.NewGuid();
            this.Inventory = new List<Prop>();
            this.Occupants = new List<Person>();
            this.ActionsReceived = new List<KeyValuePair<Action, ActionEventArgs>>();
            this.ActionsPerformed = new List<KeyValuePair<Action, ActionEventArgs>>();
            this.MaxOccupancy = 0;
        }

        protected virtual void MoveToLocation(Location newLoc)
        {
            if(this.GetType() == typeof(Prop))
            {
                if (this.Location != null)
                {
                    this.Location.Inventory.Remove((Prop)this);
                }

                newLoc.Inventory.Add((Prop)this);
            }
            else if (this.GetType() == typeof(Person))
            {
                if (this.Location != null)
                {
                    this.Location.Occupants.Remove((Person)this);
                }

                newLoc.Occupants.Add((Person)this);
            }
            
            this._location = newLoc;
        }

        protected void MoveToLocation(string displayName)
        {
            this.MoveToLocation(Location.Get(displayName));
        }
    }
}
