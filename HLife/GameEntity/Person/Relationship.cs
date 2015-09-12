using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public class Relationship 
    {
        /// <summary>
        /// The other iPerson in the relationship.
        /// </summary>
        public Person Subject { get; set; }

        /// <summary>
        /// Timestamp for when the relationship was first formed (first contact).
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// History of actions done to this iPerson by the Subject.
        /// </summary>
        public List<Action> ActionHistoryReceived { get; set; }

        /// <summary>
        /// History of actions done to the subject by this iPerson.
        /// </summary>
        public List<Action> ActionHistoryGiven { get; set; }

        /// <summary>
        /// If and how the two iPersons are related as family.
        /// </summary>
        public FamilyRelations FamilialRelation { get; set; }

        /// <summary>
        /// If and in what capacity the two iPersons are sexually related.
        /// </summary>
        public SexualRelations SexualRelationship { get; set; }

        /// <summary>
        /// How much attachment the iPerson feels towards the Subject.
        /// Affects the iPersons' overall relationship status.
        /// ie. A family member would have high Affection. An enemy would have low Affection.
        /// </summary>
        public double Affection { get; set; }

        /// <summary>
        /// How much sexual attraction the iPerson feels towrds the Subject.
        /// Affects how likely the iPerson is to engage in sex with the Subject.
        /// ie. A lover would have high Lust. Someone attracted to a different sex would have a low Lust.
        /// </summary>
        public double Lust { get; set; }

        /// <summary>
        /// How much the iPerson trusts the Subject.
        /// Affects how likely the iPerson is to do what the Subject asks or to believe what they say.
        /// ie. A lover would have high Trust.
        /// </summary>
        public double Trust { get; set; }

        public Relationship()
        {
            this.CreatedOn = Game.Instance.Date;
            this.ActionHistoryGiven = new List<Action>();
            this.ActionHistoryReceived = new List<Action>();
        }

        public Relationship(Person subject)
        {
            this.CreatedOn = Game.Instance.Date;
            this.Subject = subject;
            this.ActionHistoryGiven = new List<Action>();
            this.ActionHistoryReceived = new List<Action>();
        }
    }
}
