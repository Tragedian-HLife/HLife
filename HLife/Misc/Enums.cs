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
        Heterosexual,
        Homosexual,
        Bisexual
    }

    /// <summary>
    /// Biological sex.
    /// </summary>
    public enum Sex
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
        Blue
    }

    /// <summary>
    /// Breast sizes in UK cup.
    /// </summary>
    public enum BreastSizes
    {
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
        Absolute,
        Relative,
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
}
