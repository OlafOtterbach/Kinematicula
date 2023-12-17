namespace Kinematicula.LogicViewing.Services.FitInView;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.Mathematics;
using Kinematicula.Scening;

public static class SceneInViewFitting
{
    private struct BoundedBox
    {
        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MinZ { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }
        public double MaxZ { get; set; }
    }

    private struct CardinalDirections
    {
        public Position3D North { get; set; }
        public Position3D West { get; set; }
        public Position3D East { get; set; }
        public Position3D South { get; set; }
    }

    public static void FitInView(this Scene scene, Camera camera)
    {
        var cameraSystem = camera.Frame.Inverse();
        var pointCloude = scene.Bodies.GetPointCloude(cameraSystem).ToList();



    }

    private static BoundedBox MergeToBoundedBox(BoundedBox first, Position3D pos)
    {
        var boundedBox = new BoundedBox()
        {
            MinX = Math.Min(first.MinX, pos.X),
            MinY = Math.Min(first.MinY, pos.Y),
            MinZ = Math.Min(first.MinY, pos.Z),
            MaxX = Math.Max(first.MaxX, pos.X),
            MaxY = Math.Max(first.MaxY, pos.Y),
            MaxZ = Math.Max(first.MaxZ, pos.Z),
        };

        return boundedBox;
    }

    /// <summary>
    /// Calculates distance from position to face.
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="offset">Offset of face</param>
    /// <param name="normal">Normal of face</param>
    /// <returns>Distance</returns>
    public static double DistancePositionToPlane(this Position3D position, Position3D offset, Vector3D normal)
    {
        var nx = normal.X;
        var ny = normal.Y;
        var nz = normal.Z;
        var x = offset.X;
        var y = offset.Y;
        var z = offset.Z;
        var px = position.X;
        var py = position.Y;
        var pz = position.Z;
        var direction = (px - x) * nx + (py - y) * ny + (pz - z) * nz;
        var dist = Math.Abs(direction);
        return dist;
    }
}
