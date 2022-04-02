using Kinematicula.Mathematics;

namespace Kinematicula.LogicViewing
{
    public class CameraInfo
    {
        public string Name { get; set; }

        public double Distance => (Target - Frame.Offset).Length;

        public double NearPlane { get; set; }

        public Position3D Target { get; set; }

        public Matrix44D Frame { get; set; }
    }
}