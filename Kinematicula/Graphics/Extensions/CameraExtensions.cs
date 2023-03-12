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
        camera.Target = targetPosition;
    }

    public static void OrbitXY(this Camera camera, double alpha)
    {
        var rotationZ = Matrix44D.CreateRotation(camera.Target, camera.Frame.Ez, alpha);
        var rotatedFrame = rotationZ * camera.Frame;
        camera.Frame = rotatedFrame;
    }

    public static void OrbitXZ(this Camera camera, double alpha)
    {
        var rotationY = Matrix44D.CreateRotation(camera.Target, camera.Frame.Ey, -alpha);
        var rotatedFrame = rotationY * camera.Frame;
        camera.Frame = rotatedFrame;
    }

    public static void Zoom(this Camera camera, double delta)
    {
        if (delta >= camera.Distance)
        {
            delta = camera.Distance;
        }

        var translationX = Matrix44D.CreateTranslation(camera.Frame.Ex * delta);
        var translatedFrame = translationX * camera.Frame;
        camera.Frame = translatedFrame;
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