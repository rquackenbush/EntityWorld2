using System;
namespace EntityWorld.Library
{
    public static class DistanceCalculator
    {
        /// <summary>
        /// Calculates the distance between two points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <remarks>https://www.omnicalculator.com/math/distance</remarks>
        public static double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}
