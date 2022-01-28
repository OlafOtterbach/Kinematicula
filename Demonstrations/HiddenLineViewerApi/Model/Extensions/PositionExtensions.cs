using Kinematicula.Mathematics;

namespace HiddenLineViewerApi
{
    public static class PositionExtensions
    {
        public static PositionDto ToPositionDto(this Position3D position)
        {
            return new PositionDto() { X = position.X, Y = position.Y, Z = position.Z };
        }

        public static Position3D ToPosition3D(this PositionDto positionDto)
        {
            return new Position3D(positionDto.X, positionDto.Y, positionDto.Z);
        }
    }
}
