namespace Kinematicula.LogicViewing.Services.FitInView.Services;

using Kinematicula.Mathematics;

public static class DistanceMathmatics
{
    public static Position3D MinDistanceToPlane(this IEnumerable<Position3D> positions, Position3D offsetPlane, Vector3D normalPlane)
    {

        (double Distance, Position3D Position) firstAcc = new(double.MaxValue, new Position3D());
        var minPosition
            = positions.AsParallel()
                       .Aggregate(firstAcc, (acc, pos) => acc.AccumulateMinDistanceAndPositionToPlane(pos, offsetPlane, normalPlane));

        return minPosition.Position;
    }

    private static (double Distance, Position3D Position)
    AccumulateMinDistanceAndPositionToPlane(
        this (double Distance, Position3D Position) acc,
        Position3D position,
        Position3D offsetPlane,
        Vector3D normalPlane)
    {
        var distance = DistancePositionToPlane(position, offsetPlane, normalPlane);
        if(distance < acc.Distance)
        {
            acc = (distance, position);
        }

        return acc;
    }

    public static double DistancePositionToPlane(this Position3D position, Position3D offsetPlane, Vector3D normalPlane)
    {
        var nx = normalPlane.X;
        var ny = normalPlane.Y;
        var nz = normalPlane.Z;
        var x = offsetPlane.X;
        var y = offsetPlane.Y;
        var z = offsetPlane.Z;
        var px = position.X;
        var py = position.Y;
        var pz = position.Z;
        var direction = (px - x) * nx + (py - y) * ny + (pz - z) * nz;
        var dist = Math.Abs(direction);
        return dist;
    }

    public static Matrix44D GetNonIntersectionDistanceOfBoundedBox(this BoundedBox boundedBox, double cameraAngle)
    {
        // Get position vor minima Y or maxima Y for no collision with view
        var heightXy1 = GetNonIntersectionDistance(boundedBox.MinY, cameraAngle);
        var heightXy2 = GetNonIntersectionDistance(boundedBox.MaxY, cameraAngle);
        var heightXy = Math.Max(heightXy1, heightXy2);

        // Get position vor minima Z or maxima Z for no collision with view
        var heightXz1 = GetNonIntersectionDistance(boundedBox.MinZ, cameraAngle);
        var heightXz2 = GetNonIntersectionDistance(boundedBox.MaxZ, cameraAngle);
        var heightXz = Math.Max(heightXz1, heightXz2);

        // Get position vor Y or Z for no collision with view
        var height = Math.Max(heightXy, heightXz);

        // Get shifting way in X for no collision bounded box with view.
        var shift = height - boundedBox.MinX;

        var translation = Matrix44D.CreateTranslation(new Vector3D(shift, 0.0, 0.0));

        return translation;
    }

    //   |\        / view sight
    //   | \alpha /
    //  h|  \    /
    //   |   \  /
    //   |beta\/   with beta = 90° - alpha/2.0
    // --|------------------------------------------------
    //   | w
    private static double GetNonIntersectionDistance(double position, double cameraAngle)
    {
        var w = Math.Abs(position);
        var h = w / (Math.PI / 2 - cameraAngle / 2);

        return h;
    }

}
