namespace Kinematicula.Mathematics;

using Kinematicula.Mathematics.Extensions;

public static class AngleMath
{
    // Normalized modulo 2 PI angles.
    public static double CounterClockwiseLength(double start, double end)
    {
        var length = end >= start ? end - start : ConstantsMath.Pi2 - start + end;
        return length;
    }


    // Normalized modulo 2 PI angles.
    public static double DistantAngleTo(this double angle, double targetAngle)
    {
        var deltaAngle = Math.Abs(targetAngle - angle);
        var distance = Math.Min(deltaAngle, deltaAngle.ToCounterAngle());
        return distance;
    }

    // Normalized modulo 2 PI angles.
    public static double ToCounterAngle(this double angle) => ConstantsMath.Pi2 - angle;


    public static double ToRadiant(this double degAngle)
    {
        var result = degAngle * Math.PI / 180.0f;
        return result;
    }

    public static double ToRadiant(this int degAngle)
    {
        return ToRadiant((double)degAngle);
    }

    public static double ToDegree(this double radAngle)
    {
        var result = radAngle * 180.0f / Math.PI;
        return result;
    }

    public static double ToDegree(this int radAlpha)
    {
        return ToDegree((double)radAlpha);
    }

    public static double Modulo2Pi(this double angle)
    {
        var alpha = angle % ConstantsMath.Pi2;
        if (alpha < 0)
        {
            alpha = ConstantsMath.Pi2 + alpha;
        }
        return alpha;
    }

    public static double ToAngle(this (double X,double Y) vector)
    {
        var x = vector.X;
        var y = vector.Y;

        if (x.EqualsTo(0.0) && y.EqualsTo(0.0))
        {
            return 0.0;
        }
        {
            var alpha = Math.Atan2(y, x);
            alpha = alpha >= 0.0 ? alpha : ConstantsMath.Pi2 + alpha;
            return alpha;
        }
    }

    public static double FindNextToPi2AngleBasingOnAngle(double angle, double targetCompareAngle)
    {
        var delta = targetCompareAngle - angle;
        var divideDeltaThrough2PiAbsolute = Math.Abs(Math.Truncate(delta / ConstantsMath.Pi2));
        var sign = delta > 0.0 ? 1.0 : -1.0;

        var alpha1 = angle + sign * divideDeltaThrough2PiAbsolute * ConstantsMath.Pi2;
        var alpha2 = angle + sign * (divideDeltaThrough2PiAbsolute + 1.0) * ConstantsMath.Pi2;

        var dist1 = Math.Abs(targetCompareAngle - alpha1);
        var dist2 = Math.Abs(targetCompareAngle - alpha2);

        var newAngle = dist1 <= dist2 ? alpha1 : alpha2;

        return newAngle;
    }

    public static double FindNextToPiAngleBasingOnAngle(double angle, double targetCompareAngle)
    {
        var delta = targetCompareAngle - angle;
        var divideDeltaThrough2PiAbsolute = Math.Abs(Math.Truncate(delta / ConstantsMath.Pi));
        var sign = delta > 0.0 ? 1.0 : -1.0;

        var alpha1 = angle + sign * divideDeltaThrough2PiAbsolute * ConstantsMath.Pi;
        var alpha2 = angle + sign * (divideDeltaThrough2PiAbsolute + 1.0) * ConstantsMath.Pi;

        var dist1 = Math.Abs(targetCompareAngle - alpha1);
        var dist2 = Math.Abs(targetCompareAngle - alpha2);

        var newAngle = dist1 <= dist2 ? alpha1 : alpha2;

        return newAngle;
    }
}