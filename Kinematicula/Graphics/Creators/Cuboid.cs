namespace Kinematicula.Graphics.Creators;

using Kinematicula.Graphics.Creators.Creator;
using Kinematicula.Mathematics;

public static class Cuboid
{
    public static Body Create(double width, double height, double depth)
        => Create(width, height, depth, new Color(0, 0, 1), new Color(0,0,0));

    public static Body Create(double width, double height, double depth, Color edgeColor)
    {
        var color = new Color(0, 0, 1);
        return Create(width, height, depth, color, edgeColor);
    }

    public static Body Create(double width, double height, double depth, Color faceColor, Color edgeColor)
        => Create(width, height, depth, faceColor, faceColor, faceColor, faceColor, faceColor, faceColor, edgeColor);

    public static Body Create(
        double width,
        double height,
        double depth,
        Color colorSouth,
        Color colorEast,
        Color colorNorth,
        Color colorWest,
        Color colorTop,
        Color colorBottom,
        Color edgeColor)
    {
        width = width > 0 ? width / 2.0 : 0.5;
        height = height > 0 ? height / 2.0 : 0.5;
        depth = depth > 0 ? depth / 2.0 : 0.5;

        var p1 = new Position3D(-width, -depth, -height);
        var p2 = new Position3D(width, -depth, -height);
        var p3 = new Position3D(width, -depth, height);
        var p4 = new Position3D(-width, -depth, height);
        var p5 = new Position3D(-width, depth, -height);
        var p6 = new Position3D(width, depth, -height);
        var p7 = new Position3D(width, depth, height);
        var p8 = new Position3D(-width, depth, height);

        var creator = new GraphicsCreator();

        // South
        creator.AddFace(true, false, colorSouth);
        creator.AddTriangle(p1, p2, p3);
        creator.AddTriangle(p3, p4, p1);

        // East
        creator.AddFace(true, false, colorEast);
        creator.AddTriangle(p2, p6, p7);
        creator.AddTriangle(p7, p3, p2);

        // North
        creator.AddFace(true, false, colorNorth);
        creator.AddTriangle(p6, p5, p8);
        creator.AddTriangle(p8, p7, p6);

        // West
        creator.AddFace(true, false, colorWest);
        creator.AddTriangle(p5, p1, p4);
        creator.AddTriangle(p4, p8, p5);

        // Top
        creator.AddFace(true, false, colorTop);
        creator.AddTriangle(p4, p3, p7);
        creator.AddTriangle(p7, p8, p4);

        // Bottom
        creator.AddFace(true, false, colorBottom);
        creator.AddTriangle(p2, p1, p5);
        creator.AddTriangle(p5, p6, p2);

        var body = creator.CreateBody(edgeColor);

        return body;
    }
}