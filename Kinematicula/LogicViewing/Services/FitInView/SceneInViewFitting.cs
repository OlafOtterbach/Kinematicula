namespace Kinematicula.LogicViewing.Services.FitInView;

using Kinematicula.Graphics.Extensions;
using Kinematicula.LogicViewing.Mathmatics;
using Kinematicula.LogicViewing.Services.FitInView.Services;
using Kinematicula.Mathematics;
using Kinematicula.Scening;

public static class SceneInViewFitting
{
    public static void FitInView(this Scene scene, Matrix44D cameraFrame, double nearPlane, double canvasWidth, double canvasHeight)
    {
        // getting point cloude in camera coordinate system
        var cameraSystem = cameraFrame.Inverse();
        var pointCloude = scene.Bodies.GetPointCloude(cameraSystem).ToList();

        // gettin point cloud translated before the vie frustum without intersecting it.
        var boundedBox = pointCloude.GetBoundedBox();
        var horicontalAngle = ViewProjection.GetHorizontalAngle(nearPlane, canvasWidth, canvasHeight);
        var verticalAngle = ViewProjection.GetVerticalAngle(nearPlane, canvasWidth, canvasHeight);
        var translation = boundedBox.GetNonIntersectionDistanceOfBoundedBox(horicontalAngle, verticalAngle);
        pointCloude = pointCloude.AsParallel().Select(p => translation * p).ToList();

        // getting frustum planes


    }

}
