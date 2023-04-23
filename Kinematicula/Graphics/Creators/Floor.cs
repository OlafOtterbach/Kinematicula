namespace Kinematicula.Graphics.Creators;

using Kinematicula.Mathematics;
using Kinematicula.Graphics.Creators.Creator;

public static class Floor
{
    public static Body Create(int count, double size)
    {
        var oddColor = new Color(0.8, 0.8, 0.8);
        var evenColor = new Color(0.2, 0.2, 0.2);
        var edgeColor = new Color(0, 0, 0);

        return Create(count, size, oddColor, evenColor, edgeColor);
    }

    public static Body Create(int count, double size, Color edgeColor)
    {
        var oddColor = new Color(0.8, 0.8, 0.8);
        var evenColor = new Color(0.2, 0.2, 0.2);
        return Create(count, size, oddColor, evenColor, edgeColor);
    }

    public static Body Create(int count, double size, Color oddColor, Color evenColor, Color edgeColor)
    {
        var creator = new GraphicsCreator();

        var positions = CreatePositions(count, size);
        for (var y = 0; y < count; y++)
        {
            for (var x = 0; x < count; x++)
            {
                var color = ((y % 2) + (x % 2)) % 2 == 0 ? evenColor : oddColor;

                var point1 = positions[x][y];
                var point2 = positions[x][y + 1];
                var point3 = positions[x + 1][y + 1];
                var point4 = positions[x + 1][y];
                creator.AddFace(true, false, color);
                creator.AddTriangle(point1, point2, point4);
                creator.AddTriangle(point3, point4, point2);
            }
        }

        size = size * count;
        size = size > 0 ? size / 2.0 : 0.5;
        var hight = 2;

        var p1 = new Position3D(-size, -size, -hight);
        var p2 = new Position3D(size, -size, -hight);
        var p3 = new Position3D(size, -size, 0);
        var p4 = new Position3D(-size, -size, 0);

        var p5 = new Position3D(-size, size, -hight);
        var p6 = new Position3D(size, size, -hight);
        var p7 = new Position3D(size, size, 0);
        var p8 = new Position3D(-size, size, 0);

        var body = creator.CreateBody(edgeColor);

        return body;
    }

    private static Position3D[][] CreatePositions(int count, double size)
    {
        return CreatePositionGrid(count, size).ToArray();
    }

    private static IEnumerable<Position3D[]> CreatePositionGrid(int count, double size)
    {
        var center = count * size / 2.0;
        for (var y = 0; y <= count; y++)
        {
            yield return CreatePositionLine(y * size - center, center, count, size).ToArray();
        }
    }

    private static IEnumerable<Position3D> CreatePositionLine(double ypos, double center, int count, double size)
    {
        for (var x = 0; x <= count; x++)
        {
            yield return new Position3D(x * size - center, ypos, 0.0);
        }
    }
}