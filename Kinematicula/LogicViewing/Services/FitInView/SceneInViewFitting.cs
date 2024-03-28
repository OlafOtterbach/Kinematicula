namespace Kinematicula.LogicViewing.Services.FitInView;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.LogicViewing.Services.FitInView.Services;
using Kinematicula.Scening;

public static class SceneInViewFitting
{
    public static void FitInView(this Scene scene, Camera camera, double canvasWidth, double canvasHeight)
    {
        // getting point cloude in camera coordinate system
        //
        //                               .
        //                             .  .
        //                         .  .   .
        //     .  .  .              .   .
        //    .  .   .                .
        //      .  .                 |
        //    /           =>         _
        //  / /                     | |
        // /_/                      |_|
        //
        var cameraSystem = camera.Frame.Inverse();
        var pointCloudInCameraSystem = scene.Bodies.GetPointCloude(cameraSystem);

        var transformation = FitInViewService.FitInView(
            camera.Frame,
            camera.NearPlane,
            canvasWidth,
            canvasHeight,
            pointCloudInCameraSystem);

        camera.Frame = transformation;
    }
}
