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
        /// Convert an fps value to the value needed to be written to memory (all credit go to Cameri from TProgramming)
        /// </summary>
        /// <param name="fps"></param>
        /// <returns></returns>
        public static double ConvertFPSforMemory(double fps)
        {
            return Math.Round((1110 / fps) - 5, 1);
        }

        /// <summary>
        /// Convert a double read from memory into an FPS value (all credit go to Cameri from TProgramming)
        /// </summary>
        /// <param name="memfps"></param>
        /// <returns></returns>
        public static double ConvertMemoryToFPS(double memfps)
        {
            return Math.Round(1110 / (memfps + 5), 1);
        }
    }
}
