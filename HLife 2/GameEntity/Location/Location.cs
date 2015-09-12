using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using ImageMagick;

namespace HLife_2
{
    /// <summary>
    /// Occupiable location within the city.
    /// </summary>
    public partial class Location
        : GameEntity
    {
        /// <summary>
        /// Name presented to the player.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description of the location.
        /// </summary>
        public string Description { get; set; }

        public string BackgroundImage { get; set; }

        public Bitmap BackgroundImageBlurred { get; set; }

        public string MapImage { get; set; }

        public Size Size { get; set; }

        /// <summary>
        /// Used to deserialize the Location's inventory.
        /// </summary>
        public List<string> XmlProps { get; set; }

        public Location Parent { get; set; }

        public List<Location> Children { get; set; }

        public List<LocationNetworkEdge> Edges { get; set; }

        public bool AutoSize { get; set; }

        public double AutoSizeMultiplier { get; set; }

        public MapSizeModes LocationMode { get; set; }

        public Location()
        {
            this.Id = Guid.NewGuid();
            this.Inventory = new List<Prop>();
            this.XmlProps = new List<string>();
            this.Parent = null;
            this.Children = new List<Location>();
            this.Edges = new List<LocationNetworkEdge>();

            this.DisplayName = "{LOCATION}";

            this.BackgroundImage = "";
        }

        public static Location Get(string name)
        {
            string[] path = name.Split('.');

            Location loc = Game.Instance.City;

            if (loc.DisplayName == path.First())
            {
                return loc;
            }

            foreach (string pathElem in path)
            {
                loc = loc.Children.Find(e => e.DisplayName == pathElem);
            }

            return loc;
        }

        /// <summary>
        /// Loads the form layout for this location.
        /// </summary>
        public void LoadLayout()
        {
            /*
            WindowController.Get<Hlife2>().SuspendLayout();

            Control menuStrip = WindowController.Get<Hlife2>().Controls.Find("menuStrip", false)[0];
            Control lblDate = WindowController.Get<Hlife2>().Controls.Find("lbl_date", false)[0];

            WindowController.Get<Hlife2>().Controls.Clear();

            // Render order is first = top.
            WindowController.Get<Hlife2>().Controls.Add(lblDate);
            WindowController.Get<Hlife2>().Controls.Add(menuStrip);
            */

            this.CreateMap();

            this.LayoutLogic();

            WindowController.Get<Hlife2>().ResumeLayout(false);
        }

        /// <summary>
        /// Override this with the form layout code.
        /// </summary>
        public virtual void LayoutLogic()
        {
            Control container = ((SplitContainer)WindowController.Get<Hlife2>().Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            Image img = Game.Instance.ResourceController.GetBackgroundImage(this.BackgroundImage);

            Task.Factory.StartNew(() => CreateBlurredBackground());

            container.BackgroundImage = img;
        }

        private void CreateBlurredBackground()
        {
            this.BackgroundImageBlurred = ImageUtilities.FastBlur(
                new Bitmap(Game.Instance.ResourceController.GetBackgroundImage(this.BackgroundImage)), 2);
        }

        public void BlurBackground()
        {
            Control container = ((SplitContainer)WindowController.Get<Hlife2>().Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            Image img = Game.Instance.ResourceController.GetBackgroundImage(this.BackgroundImage);

            container.BackgroundImage = this.BackgroundImageBlurred;
        }

        public void UnblurBackground()
        {
            Control container = ((SplitContainer)WindowController.Get<Hlife2>().Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            Image img = Game.Instance.ResourceController.GetBackgroundImage(this.BackgroundImage);

            container.BackgroundImage = img;
        }

        public void LoadMap()
        {
            WindowController.Get<MapWindow>().BackgroundImage =
                Game.Instance.ResourceController.GetBackgroundImage(this.MapImage);
        }

        public void ConvertFromXml()
        {
            foreach (string prop in this.XmlProps)
            {
                this.Inventory.Add(Game.Instance.PropController.CreateProp(prop));
            }

            this.XmlProps.Clear();

            foreach(Location loc in this.Children)
            {
                loc.ConvertFromXml();
                loc.Parent = this;
            }
        }

        public void DoAutoSize()
        {
            // Get background image's native size.
            this.Size = Game.Instance.ResourceController.GetBackgroundImage(this.MapImage).Size;

            // Apply the multiplier.
            this.Size = new Size((int)Math.Round(this.Size.Width * AutoSizeMultiplier), (int)Math.Round(this.Size.Height * AutoSizeMultiplier));
        }

        public void SizeMap()
        {
            Label lblSize = new Label();
            lblSize.Name = "lbl_size";
            lblSize.Location = new System.Drawing.Point(this.Size.Width, this.Size.Height);
            lblSize.Size = new Size(0, 0);
            lblSize.Text = "";
            lblSize.BackColor = Color.Transparent;
            WindowController.Get<MapWindow>().Controls.Add(lblSize);

            if(lblSize.Location.X > 500)
            {
                WindowController.Get<MapWindow>().Size = new Size(500, WindowController.Get<MapWindow>().Size.Height);
            }
            else
            {
                WindowController.Get<MapWindow>().ClientSize = new Size(lblSize.Location.X, WindowController.Get<MapWindow>().Size.Height);
            }

            if (lblSize.Location.Y > 500)
            {
                WindowController.Get<MapWindow>().Size = new Size(WindowController.Get<MapWindow>().Size.Width, 500);
            }
            else
            {
                WindowController.Get<MapWindow>().ClientSize = new Size(WindowController.Get<MapWindow>().Size.Width, lblSize.Location.Y);
            }
        }

        public void CreateMap()
        {
            if (this.AutoSize)
            {
                this.DoAutoSize();
            }

            WindowController.Get<MapWindow>().Controls.Clear();

            // Pause the window's layout processing.
            WindowController.Get<MapWindow>().SuspendLayout();

            this.SizeMap();

            WindowController.Get<MapWindow>().Text = this.DisplayName;

            // Set the background image.
            WindowController.Get<MapWindow>().BackgroundImage =
                Game.Instance.ResourceController.GetBackgroundImage(this.MapImage);

            // Add the controls.
            this.Edges.ForEach(e => e.AddMapButton(this));

            WindowController.Get<MapWindow>().Focus();

            // Resume the window's layout processing.
            WindowController.Get<MapWindow>().ResumeLayout(true);
        }

        public static List<Location> Pathfind(Location start, Location end)
        {
            List<Location> locPath = new List<Location>();

            PathNode path = PathfindingUtilities.FindPath(PathfindingUtilities.PathfindingAlgorithms.BreadthFirst, Game.Instance.City.NavMap.Nodes[start], Game.Instance.City.NavMap.Nodes[end], Game.Instance.City.NavMap);

            while (path != null)
            {
                locPath.Add(path.Location);

                path = path.backPointer;
            }

            return locPath;
        }

        public List<Location> PathfindTo(Location end)
        {
            return Location.Pathfind(this, end);
        }

        public List<PathNode> CreateNode()
        {
            List<PathNode> nodes = new List<PathNode>();

            nodes.Add(new PathNode(Game.Instance.City.NavMap, this));

            if(this.Children != null)
            {
                foreach(Location child in this.Children)
                {
                    nodes.AddRange(child.CreateNode());
                }
            }

            return nodes;
        }

        public List<PathNode> CreateEdges(List<PathNode> nodes)
        {
            List<PathNode> edges = new List<PathNode>();

            if (this.Parent != null)
            {
                edges.Add(nodes.Where(e => e.Location == this.Parent).First());
            }

            if (this.Children != null)
            {
                foreach (Location child in this.Children)
                {
                    edges.Add(nodes.Where(e => e.Location == child).First());
                }
            }

            return edges;
        }
    }
}
