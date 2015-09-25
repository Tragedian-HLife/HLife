using HLife.GameEntities.Locations;
using HLife.GameEntities.People.AI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace HLife.GameEntities.People
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
        /// Personal preferences.
        /// </summary>
        public PersonPreferences Preferences { get; set; }

        /// <summary>
        /// Affects a person's willingness to engage in sex with another person.
        /// </summary>
        public Sexualities Sexuality { get; set; }

        /// <summary>
        /// Personal house.
        /// </summary>
        public Location Home { get; set; }

        /// <summary>
        /// This Person's relationships with other iPersons.
        /// </summary>
        public List<Relationship> Relationships { get; set; }

        public AIAgent AIAgent { get; set; }
        
        public string HeadImage { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Person()
            : base()
        {
            this.Physique = new PersonPhysique();
            this.Stats = new Stats(typeof(Person));
            this.Preferences = new PersonPreferences();
            this.Relationships = new List<Relationship>();
            this.Sexuality = Sexualities.Heterosexual;

            this.AIAgent = new AIAgent(this);

            Game.Instance.PopulationController.People.Add(this);
        }

        ~Person()
        {
            Game.Instance.PopulationController.People.Remove(this);
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
            else if (this.Physique.Sex == Sexes.Female)
            {
                pronoun = "she";
            }
            else if (this.Physique.Sex == Sexes.Male)
            {
                pronoun = "he";
            }
            else if (this.Physique.Sex == Sexes.Futanari)
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
            else if (this.Physique.Sex == Sexes.Female)
            {
                pronoun = "her";
            }
            else if (this.Physique.Sex == Sexes.Male)
            {
                pronoun = "his";
            }
            else if (this.Physique.Sex == Sexes.Futanari)
            {
                pronoun = "her";
            }

            return pronoun;
        }

        public void Cum(Prepositions preposition, Person target)
        {
            string pronoun = this.GetPossessivePronoun();
            string capPronoun = StringUtilities.CaptializeWords(pronoun);

            if ((double)this.Stats.GetValue("EjaculationsToday") < this.Stats.GetValue<double>("Stamina") / 25.0)
            {
                if ((double)this.Stats.GetValue("CumVolume") <= 0)
                {
                    Game.Instance.Output("Not even a drop of cum is left to escape " + pronoun + " cock.");
                }
                else if ((double)this.Stats.GetValue("CumVolume") < 0.5)
                {
                    Game.Instance.Output("A dribble of cum escapes from the tip of " + pronoun + " cock.");
                }
                else if ((double)this.Stats.GetValue("CumVolume") <= 1)
                {
                    Game.Instance.Output("A few weak spurts of cum leak from the tip of " + pronoun + " cock.");
                }
                else if ((double)this.Stats.GetValue("CumVolume") <= 5)
                {
                    Game.Instance.Output("A few strong jets of cum erupt from " + pronoun + " cock.");
                }
                else if ((double)this.Stats.GetValue("CumVolume") <= 10)
                {
                    Game.Instance.Output(capPronoun + " body tenses as several large jets of thick cum fire from " + pronoun + " cock.");
                }
                else if ((double)this.Stats.GetValue("CumVolume") <= 20)
                {
                    Game.Instance.Output(capPronoun + " cock throbs hard as it keeps ejecting numerous thick ropes of cum.");
                }
                else if ((double)this.Stats.GetValue("CumVolume") <= 50)
                {
                    Game.Instance.Output(capPronoun + " aching cock sends hot streams of cum flying for what feels like ages.");
                }
                else
                {
                    Game.Instance.Output(capPronoun + " cock erupts over and over, sending a torrent of hot cum from " + pronoun + " throbing shaft.");
                }

                this.Stats.SetValue("CumVolume", Math.Floor((double)this.Stats.GetValue("CumVolume") / 2));
                this.Stats.SetValue("EjaculationsToday", (double)this.Stats.GetValue("EjaculationsToday") + 1);
            }
        }

        public void Update()
        {
            this.AIAgent.Update();
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
