using Kinematicula.Graphics;

namespace Kinematicula.LogicViewing.Extensions
{
    internal static class CameraBodyExtensions
    {
        public static CameraInfo ToCamera(this Camera cameraBody)
        {
            return new CameraInfo()
            {
                Name = cameraBody.Name,
                NearPlane = cameraBody.NearPlane,
                Frame = cameraBody.Frame,
                Target  = cameraBody.Target,
            };
        }

        public static void Update(this Camera cameraBody, CameraInfo camera)
        {
            cameraBody.NearPlane = camera.NearPlane;
            cameraBody.Frame = camera.Frame;
            cameraBody.Target = camera.Target;
        }
    }
}
