namespace Kinematicula.Graphics.Creators;

using Kinematicula.Graphics.Creators.Creator;
using Kinematicula.Mathematics;

public static class Cylinder
{
    public static Body Create(int segments, double radius, double height)
        => Create(segments, radius, height, new Color(0, 0, 1), new Color(0, 0, 0));

    public static Body Create(int segments, double radius, double height, Color edgeColor)
    {
        var color = new Color(0, 0, 1);
        return Create(segments, radius, height, color, edgeColor);
    }

    public static Body Create(int segments, double radius, double height, Color faceColor, Color edgeColor)
        => Create(segments, radius, height, faceColor, faceColor, faceColor, edgeColor);

    public static Body Create(
        int segments,
        double radius,
        double height,
        Color sheatColor,
        Color topColor,
        Color bottomColor,
        Color edgeColor)
    {
        var creator = new GraphicsCreator();

        double alpha = 2.0 * Math.PI / segments;
        double half = height / 2.0;
        double z0 = -half;
        double z1 = half;

        creator.AddFace(false, false, sheatColor);
        for (int i = 0; i < segments; i++)
        {
            double x0 = (Math.Sin(i * alpha) * radius);
            double y0 = (Math.Cos(i * alpha) * radius);
            double x1 = (Math.Sin((i + 1) * alpha) * radius);
            double y1 = (Math.Cos((i + 1) * alpha) * radius);
            var p1 = new Position3D(x0, y0, z0);
            var p2 = new Position3D(x0, y0, z1);
            var p3 = new Position3D(x1, y1, z0);
            var p4 = new Position3D(x1, y1, z1);
            creator.AddTriangle(p1, p2, p3);
            creator.AddTriangle(p3, p2, p4);
        }

        creator.AddFace(true, false, topColor);
        for (int i = 0; i < segments; i++)
        {
            double x0 = (Math.Sin(i * alpha) * radius);
            double y0 = (Math.Cos(i * alpha) * radius);
            double x1 = (Math.Sin(((i + 1) * alpha).Modulo2Pi()) * radius);
            double y1 = (Math.Cos(((i + 1) * alpha).Modulo2Pi()) * radius);
            double x2 = 0.0;
            double y2 = 0.0;
            var p1 = new Position3D(x0, y0, z1);
            var p2 = new Position3D(x1, y1, z1);
            var p3 = new Position3D(x2, y2, z1);
            creator.AddTriangle(p1, p3, p2);
        }

        creator.AddFace(true, false, bottomColor);
        for (int i = 0; i < segments; i++)
        {
            double x0 = (Math.Sin(i * alpha) * radius);
            double y0 = (Math.Cos(i * alpha) * radius);
            double x1 = (Math.Sin(((i + 1) * alpha).Modulo2Pi()) * radius);
            double y1 = (Math.Cos(((i + 1) * alpha).Modulo2Pi()) * radius);
            double x2 = 0.0;
            double y2 = 0.0;
            var p1 = new Position3D(x0, y0, z0);
            var p2 = new Position3D(x1, y1, z0);
            var p3 = new Position3D(x2, y2, z0);
            creator.AddTriangle(p1, p2, p3);
        }

        var body = creator.CreateBody(edgeColor);
        return body;
    }
}