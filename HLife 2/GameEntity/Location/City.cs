using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HLife_2
{
    public class City
        : Location
    {
        public string SpawnLocation { get; set; }

        [XmlIgnore]
        public NavMap NavMap { get; set; }

        public City()
            : base()
        {
            this.NavMap = new NavMap();
        }

        public void Initialize()
        {
            City.LoadXml(this.DisplayName);
            this.CreateNavMap();
        }

        public static City LoadXml(string name)
        {
            return XmlUtilities.CreateInstance<City>(@"Cities\" + name + @"\Resources\Map\Locations.xml");
        }

        public void CreateNavMap()
        {
            List<PathNode> nodes = this.CreateNode();

            Dictionary<PathNode, List<PathNode>> edges = new Dictionary<PathNode, List<PathNode>>();

            foreach (PathNode node in nodes)
            {
                edges.Add(node, node.Location.CreateEdges(nodes));
            }

            this.NavMap.GenerateArbitraryMap(edges);
        }
    }
}
