using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HLife
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
                    this.Paths.Add("Action Images", @"Resources\Actions\Images\");
        }

        public override void Initialize()
        { }

        public override void Update()
        { }

        public string BuildPath(string path = "")
        {
            return Path.Combine(Environment.CurrentDirectory, @"Cities\" + Game.Instance.City.DisplayName + @"\" + path);
        }

        public string BuildPath(string pathName, string file)
        {
            return this.BuildPath(this.Paths[pathName] + file);
        }

        public BitmapImage GetImage(string path)
        {
            try
            {
                return new BitmapImage(new Uri(path));
            }
            catch(FileNotFoundException e)
            {
                Debug.WriteLine(e);
            }

            return null;
        }

        public BitmapImage GetBackgroundImage(string path)
        {
            return this.GetImage(this.BuildPath("Backgrounds", path));
        }

        public BitmapImage GetPropImage(string path)
        {
            return this.GetImage(this.BuildPath("Props", path));
        }

        public Uri GetActionImage(string path, bool randomize)
        {
            if (randomize)
            {
                List<string> files = Directory.EnumerateFiles(this.BuildPath("Action Images", path), "*").ToList();

                return new Uri(files[MiscUtilities.Rand.Next(files.Count)]);
            }

            return new Uri(this.BuildPath("Action Images", path));
        }
    }
}
