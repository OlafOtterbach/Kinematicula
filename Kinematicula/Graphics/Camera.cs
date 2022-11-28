namespace Kinematicula.Graphics;

using Kinematicula.Mathematics;

public class Camera : Body
{
    public double Distance => (Target - Frame.Offset).Length;

    public double NearPlane { get; set; }

    public Position3D Target { get; set; }
}
