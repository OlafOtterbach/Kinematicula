namespace Kinematicula.Graphics.Creators;

using System;
using System.Collections.Generic;

public static class Sphere
{
    public static Body Create(int circleSegments, double radius)
    {
        var faceColor = new Color(0, 0, 1);
        var edgeColor = new Color(0, 0, 0);
        return Create(circleSegments, radius, faceColor, edgeColor);
    }

    public static Body Create(int circleSegments, double radius, Color edgeColor)
    {
        var faceColor = new Color(0, 0, 1);
        return Create(circleSegments, radius, faceColor, edgeColor);
    }

    public static Body Create(int circleSegments, double radius, Color faceColor, Color edgeColor)
    {
        var segment = new List<double>();

        double alpha = Math.PI / circleSegments;
        for (int i = 0; i <= circleSegments; i++)
        {
            double x0 = (Math.Sin(i * alpha) * radius);
            double y0 = -(Math.Cos(i * alpha) * radius);
            segment.Add(x0);
            segment.Add(y0);
        }

        var segments = new double[][]
        {
            segment.ToArray()
        };
        var borderFlags = new bool[] { true };
        var facetsFlags = new bool[] { true };

        var body = RotationBody.Create(circleSegments, segments, borderFlags, facetsFlags);

        return body;
    }
}
