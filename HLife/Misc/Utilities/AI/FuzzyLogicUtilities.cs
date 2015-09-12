using AI.Fuzzy.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    public static class FuzzyLogicUtilities
    {
        /// <summary>
        /// Creates a variable with evenly sized and spaced triangular functions.
        /// </summary>
        /// <param name="varName">The name of this variable.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maxiumum value.</param>
        /// <param name="termNames">The terms of this variable.</param>
        /// <returns>Symmetric variable.</returns>
        public static FuzzyVariable CreateSymmetricTriangularVariable(string varName, double min, double max, params string[] termNames)
        {
            FuzzyVariable fuzzyVar = new FuzzyVariable(varName, min, max);

            int count = 0;
            double spread = (max - min) / (termNames.Count() - 1);

            foreach (string term in termNames)
            {
                fuzzyVar.Terms.Add(new FuzzyTerm(term, new TriangularMembershipFunction(spread * --count, spread * ++count, spread * ++count)));
            }

            return fuzzyVar;
        }


        /// <summary>
        /// Creates a variable with evenly sized and spaced trapezoidal functions.
        /// </summary>
        /// <param name="varName">The name of this variable.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maxiumum value.</param>
        /// <param name="termNames">The terms of this variable.</param>
        /// <returns>Symmetric variable.</returns>
        public static FuzzyVariable CreateSymmetricTrapezoidalVariable(string varName, double min, double max, params string[] termNames)
        {
            FuzzyVariable fuzzyVar = new FuzzyVariable(varName, min, max);

            int count = 0;
            double spread = (max - min) / ((termNames.Count() * 2) - 1);

            foreach (string term in termNames)
            {
                if(count == 0)
                {
                    fuzzyVar.Terms.Add(new FuzzyTerm(term, new TrapezoidMembershipFunction(spread * count, spread * count, spread * ++count, spread * ++count)));
                }
                else if(count == (termNames.Count() * 2) - 2)
                {
                    fuzzyVar.Terms.Add(new FuzzyTerm(term, new TrapezoidMembershipFunction(spread * --count, spread * ++count, spread * ++count, spread * count)));
                }
                else
                {
                    fuzzyVar.Terms.Add(new FuzzyTerm(term, new TrapezoidMembershipFunction(spread * --count, spread * ++count, spread * ++count, spread * ++count)));
                }
            }

            return fuzzyVar;
        }

        public static MamdaniFuzzySystem CreateMamdaniSystem(List<FuzzyVariable> inputs, List<FuzzyVariable> outputs)
        {
            MamdaniFuzzySystem fuzzySystem = new MamdaniFuzzySystem();

            fuzzySystem.Input.AddRange(inputs);
            fuzzySystem.Output.AddRange(outputs);

            return fuzzySystem;
        }

        public static MamdaniFuzzySystem CreateMamdaniSystem(List<FuzzyVariable> inputs, List<FuzzyVariable> outputs, List<MamdaniFuzzyRule> rules)
        {
            MamdaniFuzzySystem fuzzySystem = FuzzyLogicUtilities.CreateMamdaniSystem(inputs, outputs);

            fuzzySystem.Rules.AddRange(rules);

            return fuzzySystem;
        }

        public static double GetResult(MamdaniFuzzySystem fuzzySystem, string outputVariable, params KeyValuePair<string, double>[] inputVariables)
        {
            Dictionary<FuzzyVariable, double> inputValues = new Dictionary<FuzzyVariable, double>();

            foreach(KeyValuePair<string, double> entry in inputVariables)
            {
                inputValues.Add(fuzzySystem.InputByName(entry.Key), entry.Value);
            }
            
            Dictionary<FuzzyVariable, double> result = fuzzySystem.Calculate(inputValues);
            
            return result[fuzzySystem.OutputByName(outputVariable)];
        }

        public static MamdaniFuzzyRule CreateRule(MamdaniFuzzySystem fuzzySystem, string rule)
        {
            try
            {
                return fuzzySystem.ParseRule(rule);
            }
            catch
            {
                return null;
            }
        }

        public static MamdaniFuzzySystem AddRules(MamdaniFuzzySystem fuzzySystem, params string[] rules)
        {
            try
            {
                foreach(string rule in rules)
                {
                    fuzzySystem.Rules.Add(FuzzyLogicUtilities.CreateRule(fuzzySystem, rule));
                }

                return fuzzySystem;
            }
            catch
            {
                return null;
            }
        }

        public static KeyValuePair<string, double> Pair(this string key, double value)
        {
            return new KeyValuePair<string, double>(key, value);
        }
    }
}
