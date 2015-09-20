using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace HLife
{
    public static class SaveLoadController
    {
        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Game.Instance, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
            });
            
            System.IO.File.WriteAllText(ResourceController.BuildRootPath(@"Saves\" + Game.Instance.City.DisplayName + ".json"), json);
        }

        public static void Load()
        {
            Game test;

            string save = System.IO.File.ReadAllText(ResourceController.BuildRootPath(@"Saves\" + Game.Instance.City.DisplayName + ".json"));

            test = JsonConvert.DeserializeObject<Game>(save, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
            });

            Game.Load(test);
        }
    }
}
