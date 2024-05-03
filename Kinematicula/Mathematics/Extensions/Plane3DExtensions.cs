namespace Kinematicula.Mathematics.Extensions;
public static class Plane3DExtensions
{
    public static double PositiveDistanceTo(this Position3D position, Plane3D plane)
        => plane.PositiveDistanceTo(position);

    public static double PositiveDistanceTo(this Plane3D plane, Position3D position)
        => Math.Abs(plane.DistanceTo(position));


    public static double PositiveRelativeDistanceTo(this Position3D position, Plane3D plane)
        => Math.Abs(plane.RelativeDistanceTo(position));

    public static double PositiveRelativeDistanceTo(this Plane3D plane, Position3D position)
        => Math.Abs(plane.RelativeDistanceTo(position));


    public static double DistanceTo(this Position3D position, Plane3D plane)
        => plane.DistanceTo(position);

    public static double DistanceTo(this Plane3D plane, Position3D position)
    {
        var relativeDistance = plane.RelativeDistanceTo(position);
        var distance = relativeDistance / plane.Normal.Length;
        return distance;
    }


    public static double RelativeDistanceTo(this Position3D position, Plane3D plane)
        => plane.RelativeDistanceTo(position);

    public static double RelativeDistanceTo(this Plane3D plane, Position3D position)
    {
        var nx = plane.Normal.X;
        var ny = plane.Normal.Y;
        var nz = plane.Normal.Z;
        var x = plane.Offset.X;
        var y = plane.Offset.Y;
        var z = plane.Offset.Z;
        var px = position.X;
        var py = position.Y;
        var pz = position.Z;
        var realtiveDistance = (px - x) * nx + (py - y) * ny + (pz - z) * nz;
        return realtiveDistance;
    }
}
