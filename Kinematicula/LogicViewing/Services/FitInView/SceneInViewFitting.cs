namespace Kinematicula.LogicViewing.Services.FitInView;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.Scening;

public static class SceneInViewFitting
{
    public static void FitInView(this Scene scene, Camera camera)
    {
        var cameraSystem = camera.Frame.Inverse();
        var pointCloude = scene.Bodies.GetPointCloude(cameraSystem).ToList();



    }

}
