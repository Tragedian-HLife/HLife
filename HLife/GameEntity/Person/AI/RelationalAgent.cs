using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HLife
{
    public class RelationalAgent
    {
        public AIAgent AIAgent { get; set; }
        
        public SerializableDictionary<Person, Relationship> Relationships { get; set; }

        public Relationship this[Person index]
        {
            get
            {
                if (this.Relationships.ContainsKey(index))
                {
                    return this.Relationships[index];
                }

                return null;
            }

            set
            {
                if (this.Relationships.ContainsKey(index))
                {
                    this.Relationships.Add(index, value);
                }
            }
        }

        public RelationalAgent()
        {
            this.Relationships = new SerializableDictionary<Person, Relationship>();
        }

        public RelationalAgent(AIAgent aiAgent)
            : this()
        {
            this.AIAgent = aiAgent;
        }
    }
}
