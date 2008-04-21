using System;

namespace Tibia
{
    class Calculate
    {
        /// <summary>
        /// Calculate the experience needed for any level.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static long ExpForLevel(int level)
        {
            return (long) (((50.0 / 3.0) * Math.Pow(level, 3)) - (100.0 * Math.Pow(level, 2)) + ((850.0 / 3.0) * level) - 200);
        }

        /// <summary>
        /// Convert an fps value to the value needed to be written to memory (thanks to Stiju at tpforums.org for the formula)
        /// </summary>
        /// <param name="fps"></param>
        /// <returns></returns>
        public static double ConvertFPS(double fps)
        {
            return Math.Round((1000 / fps), 1);
        }
    }
}
