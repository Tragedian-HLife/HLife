using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife.Choices
{
    public class DialogChoice
    {
        public string Text { get; set; }

        public Action Choose { get; set; }

        public DialogChoice()
        {

        }

        public DialogChoice(string text, Action choose)
        {
            this.Text = text;
            this.Choose = choose;
        }
    }
}
