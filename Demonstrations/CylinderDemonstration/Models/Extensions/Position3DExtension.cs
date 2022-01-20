using Kinematicula.Mathematics;

namespace CylinderDemonstration.Models.Extensions
{
    public static class Position3DExtension
    {
        public static PositionDto ToPositionDto(this Position3D position)
        {
            return new PositionDto() { X = position.X, Y = position.Y, Z = position.Z };
        }
    }
}
