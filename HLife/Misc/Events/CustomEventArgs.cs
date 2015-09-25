using HLife.GameEntities.People;
using HLife.GameEntities.Props;
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

        public object Data { get; set; }

        public ActionStages Stage { get; set; }

        public ActionEventArgs(Person doer, Person target, Prop prop, object data = null)
        {
            this.Doer = doer;
            this.Target = target;
            this.Prop = prop;
            this.Data = data;
            this.Stage = ActionStages.Passive;
        }
    }

    public class DialogChoiceEventArgs
    {
        public int Choice { get; set; }

        public DialogChoiceEventArgs(int choice)
        {
            this.Choice = choice;
        }
    }
}
