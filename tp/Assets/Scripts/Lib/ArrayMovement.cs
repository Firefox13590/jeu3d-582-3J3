using UnityEngine;
using System.Linq;
using System;

namespace Lib
{
    public class ArrayMovement
    {
        /// <summary>
        /// Checks whether the specified value exceeds the maximum limit and resets it if necessary.
        /// </summary>
        /// <param name="value">The value to check against the maximum limit.</param>
        /// <param name="max">The maximum allowable value.</param>
        /// <param name="resetValue">The value to return if <paramref name="value"/> exceeds <paramref name="max"/>. Defaults to 0.</param>
        /// <param name="comparaison">A character indicating the comparison operation. 'g' = '>=', 'e' = '==', 'o' = '>', 'u' = '<', 'l' = '<='.</param>
        /// <returns>The original <paramref name="value"/> if it does not exceed <paramref name="max"/>; otherwise, <paramref
        /// name="resetValue"/>.</returns>
        public static int CheckForResetLoop(int value, int max, int resetValue = 0, char comparaison = 'o', bool reverse = false)
        {
            //Debug.Log($"value: {value}, max: {max}");
            if (!(new[] { 'u', 'l' }.Contains(comparaison) && reverse))
            {
                throw new Exception("Invalid comparaison character for the given direction.");
            }

            if (comparaison == 'e' && value == max) return resetValue;

            if (!reverse)
            {
                if (comparaison == 'o' && value > max ||
                    comparaison == 'g' && value >= max)
                {
                    //Debug.Log($"reset from {value} to {resetValue}");
                    return resetValue;
                }
            }
            else
            {
                if (comparaison == 'u' && value < max ||
                    comparaison == 'l' && value <= max)
                {
                    //Debug.Log($"reset from {value} to {resetValue}");
                    return resetValue;
                }
            }
            //Debug.Log($"no reset, value stays {value}");
            return value;
        }

        public static int CheckForLoopback(int value, int max, int iterations, int resetValue = 0, char comparaison = 'o', bool reverse = false)
        {
            for(int i = iterations; i > 0; i--)
            {
                value = CheckForResetLoop(value, max, resetValue, comparaison, reverse);
            }

            return value;
        }
    }
}
