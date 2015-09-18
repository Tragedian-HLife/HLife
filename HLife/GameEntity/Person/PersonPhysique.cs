using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public class PersonPhysique
    {
        /// <summary>
        /// Age in years.
        /// </summary>
        public double Age { get; set; }

        /// <summary>
        /// Biological sex.
        /// </summary>
        public Sexes Sex { get; set; }

        /// <summary>
        /// Relative height.
        /// </summary>
        public Heights Height { get; set; }

        /// <summary>
        /// General body build.
        /// </summary>
        public BodyTypes BodyType { get; set; }

        /// <summary>
        /// Hair color.
        /// </summary>
        public HairColors HairColor { get; set; }

        /// <summary>
        /// Hair cut length.
        /// </summary>
        public HairLengths HairLength { get; set; }

        /// <summary>
        /// Eye color.
        /// </summary>
        public EyeColors EyeColor { get; set; }

        /// <summary>
        /// Breast cup measurement.
        /// </summary>
        public BreastSizes BreastSize { get; set; }

        /// <summary>
        /// Relative waist size.
        /// </summary>
        public WaistSizes WaistSize { get; set; }

        /// <summary>
        /// Relative hip size.
        /// </summary>
        public HipSizes HipSize { get; set; }

        /// <summary>
        /// Erect penis size.
        /// </summary>
        public PenisSizes PenisSize { get; set; }

        /// <summary>
        /// Size of the veginal opening.
        /// </summary>
        public VaginalSizes VaginalSize { get; set; }

        /// <summary>
        /// Size of the anal opening.
        /// </summary>
        public AnalSizes AnalSize { get; set; }
    }
}
