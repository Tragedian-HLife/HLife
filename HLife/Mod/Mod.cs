using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public class Mod
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string Date { get; set; }

        public double Version { get; set; }

        public double HLifeVersion { get; set; }

        public string Description { get; set; }

        public string Directory { get; set; }

        private bool _Enabled { get; set; }

        public bool Enabled
        {
            get
            {
                return this._Enabled;
            }

            set
            {
                if(value)
                {
                    ModController.EnableMod(this);
                }
                else
                {
                    ModController.DisableMod(this);
                }

                this._Enabled = value;
            }
        }

        public string Type { get; set; }

        public Mod()
        {
            ModController.ModsAvailable.Add(this);
        }
    }
}
