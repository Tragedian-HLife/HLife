using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public static class ModController
    {
        public static List<Mod> ModsAvailable { get; set; }

        public static List<Mod> ModsEnabled { get; set; }

        static ModController()
        {
            ModController.ModsAvailable = new List<Mod>();
            ModController.ModsEnabled = new List<Mod>();
        }

        public static void EnableMod(Mod mod)
        {
            ModController.ModsEnabled.Add(mod);
        }

        public static void DisableMod(Mod mod)
        {
            ModController.ModsEnabled.Remove(mod);
        }
    }
}
