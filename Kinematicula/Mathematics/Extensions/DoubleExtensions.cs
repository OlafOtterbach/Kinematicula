using System;

namespace Kinematicula.Mathematics.Extensions
{
    public static class DoubleExtensions
    {
        public static double DistantLengthTo(this double first, double second) => Math.Abs(first - second);
    }
}
