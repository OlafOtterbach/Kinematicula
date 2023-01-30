namespace Kinematicula.Graphics.Extensions;

using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;

public static class CameraExtensions
{
    public static void SetCameraToOrigin(this Camera camera, double alpha, double beta, double distance)
    {
        camera.SetCamera(new Position3D(), alpha, beta, distance);
    }

    public static void SetCameraToTarget(this Camera camera, double alpha, double beta, double distance)
    {
        camera.SetCamera(camera.Target, alpha, beta, distance);
    }

    public static void SetCamera(this Camera camera, Position3D targetPosition, double alpha, double beta, double distance)
    {
        var targetTranslation = Matrix44D.CreateTranslation(targetPosition.ToVector3D());

        var rotZ = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), alpha);
        var rotY = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), beta);
        var rotation = rotZ * rotY;

        var translation = Matrix44D.CreateTranslation(new Vector3D(-distance, 0.0, 0.0));
        var frame = targetTranslation * rotation * translation;

        camera.Frame = frame;
        camera.Target = new Position3D(0.0, 0.0, 0.0);
    }

    public static void OrbitXY(this Camera camera, double alpha)
    {
        var offset = camera.Frame.Offset;
        var ex = camera.Frame.Ex;
        var ey = camera.Frame.Ey;
        var ez = camera.Frame.Ez;
        var target = offset + ex * camera.Distance;
        var back = Matrix44D.CreateCoordinateSystem(target, ex, ey, ez);
        var trans = back.Inverse();
        ex = new Vector3D(1.0, 0.0, 0.0);
        ey = new Vector3D(0.0, 1.0, 0.0);
        offset = trans * offset;
        var rotateXY = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), alpha);
        ex = rotateXY * ex;
        ey = rotateXY * ey;
        offset = rotateXY * offset;
        offset = back * offset;
        ex = back * ex;
        ey = back * ey;
        ex.Normalize();
        ey.Normalize();
        var newFrame = Matrix44D.CreateCoordinateSystem(offset, ex, ez);
        camera.Frame = newFrame;
    }

    public static void OrbitXZ(this Camera camera, double alpha)
    {
        var offset = camera.Frame.Offset;
        var ex = camera.Frame.Ex;
        var ey = camera.Frame.Ey;
        var ez = camera.Frame.Ez;
        var target = offset + ex * camera.Distance;
        var back = Matrix44D.CreateCoordinateSystem(target, ex, ey, ez);
        var trans = back.Inverse();
        ex = new Vector3D(1.0, 0.0, 0.0);
        ez = new Vector3D(0.0, 0.0, 1.0);
        offset = trans * offset;
        var rotateXZ = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), -alpha);
        ex = rotateXZ * ex;
        ez = rotateXZ * ez;
        offset = rotateXZ * offset;
        offset = back * offset;
        ex = back * ex;
        ez = back * ez;
        ex.Normalize();
        ez.Normalize();
        var newFrame = Matrix44D.CreateCoordinateSystem(offset, ex, ez);
        camera.Frame = newFrame;
    }

    public static void Zoom(this Camera camera, double delta)
    {
        if (delta >= camera.Distance)
        {
            delta = camera.Distance;
        }
        var offset = camera.Frame.Offset;
        var ex = camera.Frame.Ex;
        var ey = camera.Frame.Ey;
        var ez = camera.Frame.Ez;
        offset = offset + ex * delta;
        camera.Frame = Matrix44D.CreateCoordinateSystem(offset, ex, ey, ez);
    }

    public static void MoveTargetTo(this Camera camera, Position3D target)
    {
        var offset = target - (camera.Target - camera.Frame.Offset);
        var ex = camera.Frame.Ex;
        var ey = camera.Frame.Ey;
        var ez = camera.Frame.Ez;

        camera.Target = target;
        camera.Frame = Matrix44D.CreateCoordinateSystem(offset, ex, ey, ez);
    }
}