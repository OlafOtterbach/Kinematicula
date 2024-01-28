namespace Kinematicula.LogicViewing.Services.FitInView.Services;

using Kinematicula.Mathematics;

public static class BoundedBoxCreator
{
    public static BoundedBox GetBoundedBox(this IEnumerable<Position3D> positions)
    {
        var first = CreateBaseBoundedBox();
        var boundedBox = positions.AsParallel().Aggregate(first, (acc,pos) => MergeToBoundedBox(acc, pos));
        return boundedBox;
    }

    private static BoundedBox CreateBaseBoundedBox()
    {
        var boundedBox = new BoundedBox()
        {
            MinX = double.MaxValue,
            MinY = double.MaxValue,
            MinZ = double.MaxValue,
            MaxX = double.MinValue,
            MaxY = double.MinValue,
            MaxZ = double.MinValue,
        };

        return boundedBox;
    }

    private static BoundedBox MergeToBoundedBox(BoundedBox first, Position3D pos)
    {
        var boundedBox = new BoundedBox()
        {
            MinX = Math.Min(first.MinX, pos.X),
            MinY = Math.Min(first.MinY, pos.Y),
            MinZ = Math.Min(first.MinZ, pos.Z),
            MaxX = Math.Max(first.MaxX, pos.X),
            MaxY = Math.Max(first.MaxY, pos.Y),
            MaxZ = Math.Max(first.MaxZ, pos.Z),
        };

        return boundedBox;
    }

}
