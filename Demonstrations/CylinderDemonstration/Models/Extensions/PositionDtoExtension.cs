using Kinematicula.Mathematics;

namespace CylinderDemonstration.Models.Extensions
{
    public static class PositionDtoExtension
    {
        public static Position3D ToPosition3D(this PositionDto positionDto)
        {
            return new Position3D(positionDto.X, positionDto.Y, positionDto.Z);
        }
    }
}
