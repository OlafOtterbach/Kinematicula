namespace Kinematicula.LogicViewing.Extensions;

using Kinematicula.Graphics;
using Kinematicula.Scening;

public static class SceneExtensions
{
    public static Camera GetCamera(this Scene scene, string name)
    {
        var camera = scene.GetCameraBody(name);
        return camera;
    }

    public static void UpdateCamera(this Scene scene, Camera camera)
    {
        if (camera != null)
        {
            scene.SetBodyFrame(camera, camera.Frame);
        }
    }

    private static Camera GetCameraBody(this Scene scene, string name)
    {
        var cameras = scene.Bodies.OfType<Camera>().ToList();
        var matchingCamera = cameras.Where(body => body.Name == name).Concat(cameras.Where(body => string.IsNullOrEmpty(body.Name))).First();

        return matchingCamera;
    }
}
