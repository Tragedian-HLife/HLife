using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public class Person 
        : GameEntity
    {
        /// <summary>
        /// Given name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Family name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        /// <summary>
        /// Personal physical attributes.
        /// </summary>
        public PersonPhysique Physique { get; set; }

        /// <summary>
        /// Personal attributes.
        /// </summary>
        public PersonAttributes Attributes { get; set; }

        /// <summary>
        /// Personal status.
        /// </summary>
        public PersonStatus Status { get; set; }

        /// <summary>
        /// Personal preferences.
        /// </summary>
        public PersonPreferences Preferences { get; set; }

        /// <summary>
        /// Personal house.
        /// </summary>
        public Location Home { get; set; }

        /// <summary>
        /// This Person's relationships with other iPersons.
        /// </summary>
        public List<Relationship> Relationships { get; set; }

        public AIAgent AIAgent { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Person()
            : base()
        {
            this.Physique = new PersonPhysique();
            this.Attributes = new PersonAttributes();
            this.Status = new PersonStatus();
            this.Preferences = new PersonPreferences();
            this.Relationships = new List<Relationship>();

            this.AIAgent = new AIAgent(this);

            Game.Instance.PersonController.People.Add(this);
        }

        /// <summary>
        /// Determines whether the Person has a penis.
        /// </summary>
        /// <returns>True for penis.</returns>
        public bool HasPenis()
        {
            if(this.Physique.PenisSize != PenisSizes.None)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the Person has a vagina.
        /// </summary>
        /// <returns>True for vagina.</returns>
        public bool HasVagina()
        {
            if (this.Physique.VaginalSize != VaginalSizes.None)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the Person has breasts.
        /// </summary>
        /// <returns>True for breasts.</returns>
        public bool HasBreasts()
        {
            if (this.Physique.BreastSize != BreastSizes.Flat)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the Person's pronoun.
        /// </summary>
        /// <returns>Proper pronoun.</returns>
        public string GetPronoun()
        {
            string pronoun = "you";

            if (this == Game.Instance.Player)
            {
                pronoun = "you";
            }
            else if (this.Physique.Sex == Sex.Female)
            {
                pronoun = "she";
            }
            else if (this.Physique.Sex == Sex.Male)
            {
                pronoun = "he";
            }
            else if (this.Physique.Sex == Sex.Futanari)
            {
                pronoun = "she";
            }

            return pronoun;
        }

        /// <summary>
        /// Gets the Person's possessive pronoun.
        /// </summary>
        /// <returns>Proper possessive pronoun.</returns>
        public string GetPossessivePronoun()
        {
            string pronoun = "your";

            if (this == Game.Instance.Player)
            {
                pronoun = "your";
            }
            else if (this.Physique.Sex == Sex.Female)
            {
                pronoun = "her";
            }
            else if (this.Physique.Sex == Sex.Male)
            {
                pronoun = "his";
            }
            else if (this.Physique.Sex == Sex.Futanari)
            {
                pronoun = "her";
            }

            return pronoun;
        }

        public void Cum(Prepositions preposition, Person target)
        {
            string pronoun = this.GetPossessivePronoun();
            string capPronoun = StringUtilities.CaptializeWords(pronoun);

            if ((double)this.Status.GetValue("EjaculationsToday") < this.Attributes.Stamina)
            {
                if ((double)this.Status.GetValue("CumVolume") <= 0)
                {
                    Game.Instance.Output("Not even a drop of cum is left to escape " + pronoun + " cock.");
                }
                else if ((double)this.Status.GetValue("CumVolume") < 0.5)
                {
                    Game.Instance.Output("A dribble of cum escapes from the tip of " + pronoun + " cock.");
                }
                else if ((double)this.Status.GetValue("CumVolume") <= 1)
                {
                    Game.Instance.Output("A few weak spurts of cum leak from the tip of " + pronoun + " cock.");
                }
                else if ((double)this.Status.GetValue("CumVolume") <= 5)
                {
                    Game.Instance.Output("A few strong jets of cum erupt from " + pronoun + " cock.");
                }
                else if ((double)this.Status.GetValue("CumVolume") <= 10)
                {
                    Game.Instance.Output(capPronoun + " body tenses as several large jets of thick cum fire from " + pronoun + " cock.");
                }
                else if ((double)this.Status.GetValue("CumVolume") <= 20)
                {
                    Game.Instance.Output(capPronoun + " cock throbs hard as it keeps ejecting numerous thick ropes of cum.");
                }
                else if ((double)this.Status.GetValue("CumVolume") <= 50)
                {
                    Game.Instance.Output(capPronoun + " aching cock sends hot streams of cum flying for what feels like ages.");
                }
                else
                {
                    Game.Instance.Output(capPronoun + " cock erupts over and over, sending a torrent of hot cum from " + pronoun + " throbing shaft.");
                }

                this.Status.SetValue("CumVolume", Math.Floor((double)this.Status.GetValue("CumVolume") / 2));
                this.Status.SetValue("EjaculationsToday", (double)this.Status.GetValue("EjaculationsToday") + 1);
            }
        }

        public void Update()
        {
            this.AIAgent.Update();

            //Prop temp = this.LookForProp(this.Location);
            //this.UseProp(temp);
        }

        public string GetApparentAge()
        {
            if(this.Physique.Age < 14)
            {
                return "adolescent";
            }
            else if(this.Physique.Age < 20)
            {
                return "teenage";
            }
            else if (this.Physique.Age < 30)
            {
                return "young";
            }
            else if (this.Physique.Age < 45)
            {
                return "adult";
            }
            else if (this.Physique.Age < 55)
            {
                return "middle aged";
            }
            else if (this.Physique.Age < 65)
            {
                return "senior";
            }
            else
            {
                return "elderly";
            }
        }
    }
}
