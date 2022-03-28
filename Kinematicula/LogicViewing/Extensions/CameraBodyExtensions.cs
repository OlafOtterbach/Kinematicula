using Kinematicula.Graphics;

namespace Kinematicula.LogicViewing.Extensions
{
    internal static class CameraBodyExtensions
    {
        public static Camera ToCamera(this CameraBody cameraBody)
        {
            return new Camera()
            {
                Name = cameraBody.Name,
                NearPlane = cameraBody.NearPlane,
                Frame = cameraBody.Frame,
                Target  = cameraBody.Target,
            };
        }

        public static void Update(this CameraBody cameraBody, Camera camera)
        {
            cameraBody.NearPlane = camera.NearPlane;
            cameraBody.Frame = camera.Frame;
            cameraBody.Target = camera.Target;
        }
    }
}
