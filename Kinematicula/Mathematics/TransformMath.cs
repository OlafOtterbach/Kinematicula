using Kinematicula.Mathematics.Extensions;

namespace Kinematicula.Mathematics
{
    public static class TransformMath
    {
        public static void CalculateAxesAndRotation
        (
           Matrix44D start,
           Matrix44D end,
           out Vector3D axisAlpha,
           out double resalpha,
           out Vector3D axisBeta,
           out double resbeta
        )
        {
            // Calculates distant vector between start and destination
            var origin = new Position3D(0.0, 0.0, 0.0);
            var startEx = start.Ex;
            var startEy = start.Ey;
            var startEz = start.Ez;
            var endEx = end.Ex;
            var endEy = end.Ey;
            var endEz = end.Ez;
            var normal = endEx & startEx;
            double len = normal.Length;
            double alpha = 0.0;
            if (!len.EqualsTo(0.0))
            { // startEx and endEx are not colinear

                // Creates plane coordinatesystem with startEx as Ex.
                normal = normal.Normalize();
                var ey = normal & startEx;
                ey.Normalize();
                var plane = Matrix44D.CreateCoordinateSystem(origin, startEx, ey, normal).Inverse();

                // Transform endEx in plane coordinatesystem
                endEx = plane * endEx;

                // Calculates alpha
                alpha = AngleMath.ToAngle((endEx.X, endEx.Y));
                alpha = alpha.Modulo2Pi();
                if (alpha > ConstantsMath.Pi)
                {
                    alpha = -(ConstantsMath.Pi2 - alpha);
                }

                // Rotate Ez back in start system
                var backRot = Matrix44D.CreateRotation(origin, normal, -alpha);
                endEz = backRot * endEz;
            }
            else
            {
                normal = startEz;
                if (!(startEx == endEx))
                {
                    alpha = ConstantsMath.Pi;

                    // Rotate Ez back in start system
                    var backRot = Matrix44D.CreateRotation(origin, startEz, -alpha);
                    endEz = backRot * endEz;
                }
            }

            // Calculates beta by comparing endEz with startEz in start system
            start = start.Inverse();
            endEz = start * endEz;
            var beta = -AngleMath.ToAngle((endEz.Z, endEz.Y));
            beta = beta.Modulo2Pi();
            if (beta > ConstantsMath.Pi)
            {
                beta = -(ConstantsMath.Pi2 - beta);
            }

            // returning values
            resalpha = alpha;
            resbeta = beta;
            axisAlpha = normal;
            axisBeta = startEx;
        }
    }
}
