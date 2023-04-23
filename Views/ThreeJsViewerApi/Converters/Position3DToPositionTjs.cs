namespace ThreeJsViewerApi.Converters;

using Kinematicula.Mathematics;
using ThreeJsViewerApi.GraphicsModel;

public static class Position3DToPositionTjs
{
    public static PositionTjs ToPositionTjs(this Position3D position)
    {
        return new PositionTjs() { X = position.X, Y = position.Y, Z = position.Z };
    }

    public static Position3D ToPosition3D(this PositionTjs positionDto)
    {
        return new Position3D(positionDto.X, positionDto.Y, positionDto.Z);
    }
}
