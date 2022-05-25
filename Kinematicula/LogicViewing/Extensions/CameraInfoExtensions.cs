using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;

namespace Kinematicula.LogicViewing.Extensions
{
    public static class CameraInfoExtensions
    {
        public static void OrbitXY(this CameraInfo camera, double alpha)
        {
            var offset = camera.Frame.Offset;
            var ex = camera.Frame.Ex;
            var ey = camera.Frame.Ey;
            var ez = camera.Frame.Ez;
            var target = offset + ey * camera.Distance;
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

        public static void OrbitYZ(this CameraInfo camera, double alpha)
        {
            var offset = camera.Frame.Offset;
            var ex = camera.Frame.Ex;
            var ey = camera.Frame.Ey;
            var ez = camera.Frame.Ez;
            var target = offset + ey * camera.Distance;
            var back = Matrix44D.CreateCoordinateSystem(target, ex, ey, ez);
            var trans = back.Inverse();
            ey = new Vector3D(0.0, 1.0, 0.0);
            ez = new Vector3D(0.0, 0.0, 1.0);
            offset = trans * offset;
            var rotateYZ = Matrix44D.CreateRotation(new Vector3D(1, 0, 0), alpha);
            ey = rotateYZ * ey;
            ez = rotateYZ * ez;
            offset = rotateYZ * offset;
            offset = back * offset;
            ey = back * ey;
            ez = back * ez;
            ey.Normalize();
            ez.Normalize();
            var newFrame = Matrix44D.CreateCoordinateSystem(offset, ex, ez);
            camera.Frame = newFrame;
        }

        public static void Zoom(this CameraInfo camera, double delta)
        {
            if (delta >= camera.Distance)
            {
                delta = camera.Distance;
            }
            var offset = camera.Frame.Offset;
            var ex = camera.Frame.Ex;
            var ey = camera.Frame.Ey;
            var ez = camera.Frame.Ez;
            offset = offset + ey * delta;
            camera.Frame = Matrix44D.CreateCoordinateSystem(offset, ex, ey, ez);
        }

        public static void MoveTargetTo(this CameraInfo camera, Position3D target)
        {
            var offset = target - (camera.Target - camera.Frame.Offset);
            var ex = camera.Frame.Ex;
            var ey = camera.Frame.Ey;
            var ez = camera.Frame.Ez;

            camera.Target = target;
            camera.Frame = Matrix44D.CreateCoordinateSystem(offset, ex, ey, ez);
        }
    }
}