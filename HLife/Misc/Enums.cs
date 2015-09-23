using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    /// <summary>
    /// To which gender(s) is a Person attracted.
    /// </summary>
    public enum Sexualities
    {
        /// <summary>
        /// Attracted exclusively to members outside of their own sex.
        /// </summary>
        Heterosexual,

        /// <summary>
        /// Attracted to members exclusively inside of their own sex.
        /// </summary>
        Homosexual,

        /// <summary>
        /// Attracted to both apparent males and females.
        /// </summary>
        Bisexual,

        /// <summary>
        /// Attracted to anyone.
        /// </summary>
        Pansexual
    }

    /// <summary>
    /// Biological sex.
    /// </summary>
    public enum Sexes
    {
        Male,
        Female,
        Futanari
    }

    /// <summary>
    /// Hair colors.
    /// </summary>
    public enum HairColors
    {
        Brown,
        Black,
        Red,
        Green,
        Blue,
        White,
        Blonde,
        Pink
    }

    /// <summary>
    /// Hair lengths.
    /// </summary>
    public enum HairLengths
    {
        Bald,
        Short,
        Medium,
        Long
    }

    /// <summary>
    /// Eye colors.
    /// </summary>
    public enum EyeColors
    {
        Brown,
        Green,
        Blue,
        White,
        Black
    }

    /// <summary>
    /// Breast sizes in UK cup.
    /// </summary>
    public enum BreastSizes
    {
        /// <summary>
        /// Equivalent to no breasts, such as for males.
        /// </summary>
        Flat,

        AA,
        A,
        B,
        C,
        D,
        DD,
        E,
        F,
        FF,
        G,
        GG
    }

    /// <summary>
    /// Penis sizes (combined length and girth).
    /// </summary>
    public enum PenisSizes
    {
        None,
        Tiny,
        Small,
        Average,
        AboveAverage,
        Large,
        Huge,
        Enormous
    }

    /// <summary>
    /// Vaginal diameters.
    /// </summary>
    public enum VaginalSizes
    {
        None,
        Tiny,
        Small,
        Average,
        AboveAverage,
        Large,
        Huge,
        Enormous
    }

    /// <summary>
    /// Anal diameters.
    /// </summary>
    public enum AnalSizes
    {
        Tiny,
        Small,
        Average,
        AboveAverage,
        Large,
        Huge,
        Enormous
    }

    /// <summary>
    /// Spacial relationships.
    /// </summary>
    public enum Prepositions
    {
        On,
        In,
        At,
        Under,
        Over,
        Beside,
        Near,
        Behind,
        InFront
    }

    /// <summary>
    /// Person heights.
    /// </summary>
    public enum Heights
    {
        Tiny,
        Short,
        Average,
        Tall,
        Gigantic
    }

    /// <summary>
    /// Person body builds.
    /// </summary>
    public enum BodyTypes
    {
        Skinny,
        Average,
        Pudgy,
        Fat,
        Obese,
        Athletic,
        Muscular
    }

    /// <summary>
    /// A Person's social tendancies.
    /// </summary>
    public enum Sociabilities
    {
        ExtremeIntrovert,
        Introvert,
        Average,
        Extrovert,
        ExtremeExtrovert
    }

    /// <summary>
    /// Relative waist (midsection) sizes.
    /// </summary>
    public enum WaistSizes
    {
        Thin,
        Average,
        Wide
    }

    /// <summary>
    /// Relative hip sizes.
    /// </summary>
    public enum HipSizes
    {
        Thin,
        Average,
        Wide
    }

    /// <summary>
    /// Education levels.
    /// </summary>
    public enum EducationLevels
    {
        None,
        Elementary,
        Secondary,
        Vocational,
        Associate,
        Bachelor,
        Master,
        Doctorate
    }

    /// <summary>
    /// Methods to scale the map and place locations.
    /// </summary>
    public enum MapSizeModes
    {
        /// <summary>
        /// All values are in pixels from the origin with no scaling applied.
        /// </summary>
        Absolute,

        /// <summary>
        /// All values are in pixels, relative to the map's display dimensions.
        /// </summary>
        Relative,

        /// <summary>
        /// All values are decimal percentages, relative to the map's display dimensions.
        /// </summary>
        Percent
    }

    /// <summary>
    /// Relative familial relations.
    /// </summary>
    public enum FamilyRelations
    {
        None,
        Parent,
        Child,
        Sibling,
        Spouse
    }

    /// <summary>
    /// Official sexual relationship types.
    /// </summary>
    public enum SexualRelations
    {
        None,
        Dating,
        Married,
        ExDating,
        Divorced
    }
    
    /// <summary>
    /// Describes each possible stage or mode of an Action.
    /// </summary>
    public enum ActionStages
    {
        /// <summary>
        /// Used when previewing the effects of the action.
        /// </summary>
        Preview,

        /// <summary>
        /// Used when checking if the action can be performed.
        /// </summary>
        CanPerform,

        /// <summary>
        /// Used when performing the action.
        /// </summary>
        Perform,

        /// <summary>
        /// Used when processing the action for clerical purposes.
        /// This is the default state.
        /// </summary>
        Passive,
    }
}
