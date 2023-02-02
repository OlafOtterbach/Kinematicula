namespace Kinematicula.LogicViewing.Extensions;

using Kinematicula.Graphics;
using Kinematicula.Scening;

public static class SceneExtensions
{
    public static Camera GetCamera(this Scene scene, Guid cameraId)
    {
        var camera = scene.GetBody(cameraId) as Camera;
        return camera;
    }

    public static void UpdateCamera(this Scene scene, Camera camera)
    {
        if (camera != null)
        {
            scene.SetBodyFrame(camera, camera.Frame);
        }
    }
}
