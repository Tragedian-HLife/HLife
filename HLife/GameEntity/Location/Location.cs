using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using ImageMagick;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading;
using Newtonsoft.Json;
using static HLife.MiscUtilities;

namespace HLife
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

        [XmlIgnore]
        [JsonIgnore]
        public BitmapImage BackgroundImageBlurred { get; set; }

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

        public static List<Location> GetAll(Location root)
        {
            List<Location> locs = new List<Location>();

            locs.Add(root);

            foreach(Location child in root.Children)
            {
                locs.AddRange(Location.GetAll(child));
            }

            return locs;
        }

        /// <summary>
        /// Loads the form layout for this location.
        /// </summary>
        public void LoadLayout()
        {
            this.CreateMap();

            this.LayoutLogic();

            WindowController.Get<MainWindow>().UpdateLayout();
        }

        /// <summary>
        /// Override this with the form layout code.
        /// </summary>
        public virtual void LayoutLogic()
        {
            this.UnblurBackground();

            Task.Factory.StartNew(() => CreateBlurredBackground());
        }

        private void CreateBlurredBackground()
        {
            this.BackgroundImageBlurred = ImageUtilities.FastBlur(
                Game.Instance.ResourceController.GetBackgroundImage(this.BackgroundImage), 2);

            this.BackgroundImageBlurred.Freeze();
        }

        public void BlurBackground()
        {
            Application.Current.Dispatcher.BeginInvoke(new System.Action(() =>
            {
                Image container = (Image)WindowController.Get<MainWindow>().FindName("img_LocationBackground");
                container.Source = this.BackgroundImageBlurred;
            }));
        }

        public void UnblurBackground()
        {
            Image container = (Image)WindowController.Get<MainWindow>().FindName("img_LocationBackground");

            container.Source = Game.Instance.ResourceController.GetBackgroundImage(this.BackgroundImage);
        }

        public void LoadMap()
        {
            ((Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MapWindow>(), "grid")).Background =
                new ImageBrush(Game.Instance.ResourceController.GetBackgroundImage(this.MapImage));
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
            this.Size = new Size(
                Game.Instance.ResourceController.GetBackgroundImage(this.MapImage).PixelWidth,
                Game.Instance.ResourceController.GetBackgroundImage(this.MapImage).PixelHeight);

            // Apply the multiplier.
            this.Size = new Size(
                Math.Round(this.Size.Width * AutoSizeMultiplier),
                Math.Round(this.Size.Height * AutoSizeMultiplier));
        }

        public void SizeMap()
        {
            Window window = WindowController.Get<MapWindow>();
            Grid container = ((Grid)LogicalTreeHelper.FindLogicalNode(window, "grid"));

            container.Width = this.Size.Width;
            container.Height = this.Size.Height;
            
            if(this.Size.Width > 500)
            {
                window.Width = 500;
            }
            else
            {
                window.Width = this.Size.Width + 38;
            }

            if (this.Size.Height > 500)
            {
                window.Height = 500;
            }
            else
            {
                window.Height = this.Size.Height + 39;
            }

            container.UpdateLayout();
        }

        public void CreateMap()
        {
            if (this.AutoSize)
            {
                this.DoAutoSize();
            }
            
            Grid container = ((Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MapWindow>(), "grid"));
            container.Children.Clear();

            this.SizeMap();

            WindowController.Get<MapWindow>().Title = this.DisplayName;

            // Set the background image.
            container.Background = new ImageBrush(Game.Instance.ResourceController.GetBackgroundImage(this.MapImage));

            // Add the controls.
            this.Edges.ForEach(e => e.AddMapButton(this));

            WindowController.Get<MapWindow>().Focus();

            // Resume the window's layout processing.
            WindowController.Get<MapWindow>().UpdateLayout();
        }

        public static List<Location> Pathfind(Location start, Location end)
        {
            List<Location> locPath = new List<Location>();

            PathNode path = PathfindingUtilities.FindPath(
                PathfindingUtilities.PathfindingAlgorithms.BreadthFirst, 
                Game.Instance.City.NavMap.Nodes[start], 
                Game.Instance.City.NavMap.Nodes[end], 
                Game.Instance.City.NavMap);

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
