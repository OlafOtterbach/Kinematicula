using Kinematicula.Mathematics;

namespace Kinematicula.Graphics
{
    public class CameraBody : Body
    {
        public double Distance => (Target - Frame.Offset).Length;

        public double NearPlane { get; set; }

        public Position3D Target { get; set; }
    }
}
