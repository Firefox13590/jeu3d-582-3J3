using System.Linq;
using System;

namespace Lib
{
    public static class ArrayMovement
    {
        /// <summary>
        /// Enum representing the type of comparison to perform.
        /// </summary>
        public enum ComparaisonType
        {
            LowerThan = -2,
            LessThanOrEqualTo = -1,
            EqualTo = 0,
            GreaterThanOrEqualTo = 1,
            GreaterThan = 2
        }

        /// <summary>
        /// Checks whether the specified value exceeds the maximum limit and resets it if necessary.
        /// </summary>
        /// <param name="value">The value to check against the limit.</param>
        /// <param name="max">The maximum allowable value (or last array index).</param>
        /// <param name="min">The minimum allowable value (or first array index). Defaults to 0.</param>
        /// <param name="comparaison">An enum representing comparaison rule when checking. Defaults to <c>ComparaisonType.GreaterThanOrEqualTo</c></param>
        /// <param name="reverse">Determines if checking has to be done in reverse (checking agaisnt min instead of max). Defaults to false.</param>
        /// <returns><paramref name="value"/> if it does not exceed <paramref name="max"/> or <paramref name="min"/> limits.</returns>
        public static int CheckForResetLoop(int value, int max, int min = 0, ComparaisonType comparaison = ComparaisonType.GreaterThanOrEqualTo, bool reverse = false)
        {
            //Debug.Log($"value: {value}, max: {max}");
            if (new[] { -2, -1 }.Contains((int)comparaison) ^ reverse)
            {
                throw new Exception($"Invalid comparaison rule for the given direction (value).\n" +
                    $"Value: {value}    ComparaisonType: {comparaison}    Reverse: {reverse}");
            }

            //if (!(new[] { -2, -1 }.Contains((int)comparaison) && reverse))
            //{
            //    //throw new Exception("Invalid comparaison rule for the given direction.");
            //    Debug.LogWarning("Mismatch in comparaison rule and direction (value). defaults to value\'s direction");
            //    reverse = (value < 0);
            //    comparaison = (ComparaisonType)(-(int)comparaison);
            //}

            if ((int)comparaison == 0 && value == max) return min;
            if ((int)comparaison == 0 && value == min) return max;

            if (!reverse)
            {
                if ((int)comparaison == 2 && value > max ||
                    (int)comparaison == 1 && value >= max)
                {
                    //Debug.Log($"reset from {value} to {min}");
                    return min;
                }
            }
            else
            {
                if ((int)comparaison == -1 && value <= min ||
                    (int)comparaison == -2 && value < min)
                {
                    //Debug.Log($"reset from {value} to {min}");
                    return max;
                }
            }
            //Debug.Log($"no reset, value stays {value}");
            return value;
        }

        /// <summary>
        /// Loops method <c>CheckForLoopback()</c> for a number of iterations.
        /// </summary>
        /// <param name="baseValue">Base value before starting loop</param>
        /// <param name="max">The maximum allowable value (or last array index).</param>
        /// <param name="iterations">Number of iterations to perform.</param>
        /// <param name="min">The minimum allowable value (or first array index). Defaults to 0.</param>
        /// <param name="comparaison">An enum representing comparaison rule when checking. Defaults to <c>ComparaisonType.GreaterThanOrEqualTo</c></param>
        /// <param name="reverse">Determines if checking has to be done in reverse (checking agaisnt min instead of max). Defaults to false.</param>
        /// <returns>Output of <c>CheckForLoopback()</c> after <paramref name="iterations"/> iterations.</returns>
        public static int CheckForLoopback(int baseValue, int max, int iterations, int min = 0, ComparaisonType comparaison = ComparaisonType.GreaterThan, bool reverse = false)
        {
            for(int i = iterations; i > 0; i--)
            {
                baseValue += reverse ? -1 : 1;
                baseValue = CheckForResetLoop(baseValue, max, min, comparaison, reverse);
            }

            return baseValue;
            //return CheckForResetLoop(baseValue, max, min, comparaison, reverse);
        }
    }
}
