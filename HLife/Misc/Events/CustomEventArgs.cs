using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public class ActionEventArgs
    {
        public Person Doer { get; set; }

        public Person Target { get; set; }

        public Prop Prop { get; set; }

        public ActionEventArgs(Person doer, Person target, Prop prop)
        {
            this.Doer = doer;
            this.Target = target;
            this.Prop = prop;
        }
    }
}
