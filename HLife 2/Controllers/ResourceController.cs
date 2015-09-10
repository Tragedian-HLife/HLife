using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public class ResourceController
        : Controller
    {
        public Dictionary<string, string> Paths { get; set; }

        public ResourceController()
        {
            this.Paths = new Dictionary<string, string>();

            this.Paths.Add("Assets", @"Assets\");
                this.Paths.Add("Images", this.Paths["Assets"] + @"Images\");
                    this.Paths.Add("Backgrounds", this.Paths["Images"] + @"Backgrounds\");
                    this.Paths.Add("Characters", this.Paths["Images"] + @"Characters\");
                    this.Paths.Add("Props", this.Paths["Images"] + @"Props\");
        }

        public override void Initialize()
        { }

        public override void Update()
        { }

        public string BuildPath(string path = "")
        {
            return Path.Combine(Application.StartupPath, @"Cities\" + Game.Instance.City.DisplayName + @"\" + path);
        }

        public string BuildPath(string pathName, string file)
        {
            return this.BuildPath(this.Paths[pathName] + file);
        }

        public Image GetImage(string path)
        {
            try
            {
                return Image.FromFile(path);
            }
            catch(FileNotFoundException e)
            {
                Debug.WriteLine(e);
            }

            return null;
        }

        public Image GetBackgroundImage(string path)
        {
            return this.GetImage(this.BuildPath("Backgrounds", path));
        }

        public Image GetPropImage(string path)
        {
            return this.GetImage(this.BuildPath("Props", path));
        }
    }
}
