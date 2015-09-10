using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife_2
{
    /// <summary>
    /// The static attributes a person can have.
    /// </summary>
    public class PersonAttributes
    {
        /// <summary>
        /// Affects a person's willingness to engage in sex with another person.
        /// </summary>
        public Sexualities Sexuality { get; set; }

        /// <summary>
        /// Affects a person's likability and pursuasion.
        /// </summary>
        public float Charisma { get; set; }

        /// <summary>
        /// Affects how quickly a person can learn and adapt.
        /// </summary>
        public float Intelligence { get; set; }

        /// <summary>
        /// Affects how much force a person can apply.
        /// eg. Overpowering another person requires greater strength than them.
        /// </summary>
        public float Strength { get; set; }

        /// <summary>
        /// Determines maximal energy levels and rejuvination rates.
        /// eg. Affects the max number of ejaculations per day.
        /// </summary>
        public float Stamina { get; set; }

        /// <summary>
        /// Affects how susceptable a person is to pursuasion.
        /// eg. How likely they are to agree to something they might not actually want to do.
        /// </summary>
        public float Willpower { get; set; }

        /// <summary>
        /// Affects how likely a person is to engage in sexual activity and how much.
        /// eg. A higher labido will make a person more likely to have sex and more likely to 
        /// engage in extreme sex.
        /// </summary>
        public float Labido { get; set; }

        /// <summary>
        /// Affects how likely a person is to do something unusual or reckless.
        /// ie. A lower inhibition will make a person more likely to engage in sex.
        /// </summary>
        public float Inhibition { get; set; }
    }
}
