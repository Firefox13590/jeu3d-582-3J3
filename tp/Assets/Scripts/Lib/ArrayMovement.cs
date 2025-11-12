using UnityEngine;
using System.Linq;
using System;
using System.ComponentModel;

namespace Lib
{
    public class ArrayMovement
    {
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
        /// <param name="max">The maximum allowable value.</param>
        /// <param name="min">The value to return if <paramref name="value"/> exceeds <paramref name="max"/>. Defaults to 0.</param>
        /// <param name="comparaison">An enum representing comparaison rule when checking.</param>
        /// <returns>The original <paramref name="value"/> if it does not exceed <paramref name="max"/>; otherwise, <paramref
        /// name="min"/>.</returns>
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
