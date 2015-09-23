using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public class DialogGroup
    {
        public List<Dialog> Entries { get; set; }

        public int Index { get; protected set; }

        public DialogGroup()
        {
            this.Entries = new List<Dialog>();
            this.Index = 0;
        }

        public void Reset()
        {
            this.Index = 0;
        }

        public Dialog Current()
        {
            return this.Entries[this.Index];
        }

        public Dialog Next()
        {
            return this.Entries[this.Index++];
        }

        public Dialog First()
        {
            return this.Entries[0];
        }

        public Dialog Get(int index)
        {
            this.Index = index--;

            return this.Next();
        }

        public bool IsLastDialog()
        {
            if(this.Index >= this.Entries.Count - 1)
            {
                return true;
            }

            return false;
        }
    }
}
