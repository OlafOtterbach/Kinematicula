namespace Kinematicula.Mathematics.Extensions;

public static class Vector3DExtensions
{
    public static Position3D ToPosition3D(this Vector3D vector)
    {
        return new Position3D(vector.X, vector.Y, vector.Z);
    }

    public static double SquaredLength(this Vector3D vector)
    {
        var squaredLength = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z;
        return squaredLength;
    }

    public static bool IsLinearTo(this Vector3D first, Vector3D second)
    {
        var normalFirst = first.Normalize();
        var normalSecond = second.Normalize();
        bool result = (normalFirst == normalSecond) || (normalFirst == (normalSecond * -1.0f));
        return result;
    }

    // Returns smallest angle between two vectors.
    public static double AngleWith(this Vector3D first, Vector3D second)
    {
        var alpha = 0.0;

        // Calculates coordinate system of first as X axis
        var ex = first.Normalize();
        second = second.Normalize();
        var ez = ex & second;
        if (ez.Length > ConstantsMath.Epsilon)
        {
            ez = ez.Normalize();

            // Vectors are linear independent
            var ey = ez & ex;
            ey = ey.Normalize();

            // Transforms second to this coordinate system
            var origin = new Position3D(0.0f, 0.0f, 0.0f);
            var matrix = Matrix44D.CreateCoordinateSystem(origin, ex, ey, ez).Inverse();
            second = matrix * second;

            // Angle between first as X-Axis and second
            alpha = (second.X, second.Y).ToAngle();
        }
        else
        {
            // Vectors lies on same line
            if (ex == second)
            {
                // Vectors have same direction
                alpha = 0.0;
            }
            else
            {
                // Vectors have opposite direction
                alpha = ConstantsMath.Pi;
            }
        }

        return alpha;
    }

    public static double CounterClockwiseAngleWith(this Vector3D first, Vector3D second, Vector3D axisDirection)
    {
        var alpha = 0.0;

        // Calculates coordinate system of first as X axis
        var ex = first.Normalize();
        second = second.Normalize();
        var ez = ex & second;
        if (ez.Length > ConstantsMath.Epsilon)
        {
            ez = axisDirection;

            // Vectors are linear independent
            var ey = ez & ex;
            ey = ey.Normalize();

            // Transforms second to this coordinate system
            var origin = new Position3D(0.0f, 0.0f, 0.0f);
            var matrix = Matrix44D.CreateCoordinateSystem(origin, ex, ey, ez).Inverse();
            second = matrix * second;

            // Angle between first as X-Axis and second
            alpha = (second.X, second.Y).ToAngle();
        }
        else
        {
            // Vectors lies on same line
            if (ex == second)
            {
                // Vectors have same direction
                alpha = 0.0;
            }
            else
            {
                // Vectors have opposite direction
                alpha = ConstantsMath.Pi;
            }
        }

        return alpha;
    }
}
