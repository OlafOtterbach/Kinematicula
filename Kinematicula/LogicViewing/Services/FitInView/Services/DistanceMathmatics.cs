using Kinematicula.Mathematics;

namespace Kinematicula.LogicViewing.Services.FitInView.Services;
public static class DistanceMathmatics
{
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
}
