using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public class DialogGroup
    {
        public string Id { get; set; }

        public List<Dialog> Entries { get; set; }

        public DialogGroup()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Entries = new List<Dialog>();
        }
    }
}
