using Kinematicula.Graphics;
using Kinematicula.Scening;

namespace Kinematicula.LogicViewing.Extensions
{
    internal static class SceneExtensions
    {
        public static Camera GetCamera(this Scene scene, string name)
        {
            var cameraBody = scene.GetCameraBody(name);
            var camera = cameraBody.ToCamera();
            return camera;
        }

        public static void UpdateCamera(this Scene scene, Camera camera)
        {
            if (camera != null)
            {
                var cameraBody = scene.GetCameraBody(camera.Name);

                cameraBody.Update(camera);
                scene.SetBodyFrame(cameraBody, camera.Frame);
            }
        }

        private static CameraBody GetCameraBody(this Scene scene, string name)
        {
            var cameras = scene.Bodies.OfType<CameraBody>().ToList();
            var matchingCamera = cameras.Where(body => body.Name == name).Concat(cameras.Where(body => string.IsNullOrEmpty(body.Name))).First();

            return matchingCamera;
        }
    }
}
