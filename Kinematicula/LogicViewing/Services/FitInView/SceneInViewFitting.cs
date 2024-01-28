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

        // getting point cloud translated to position in the view frustum without intersecting it.
        var boundedBox = pointCloude.GetBoundedBox();
        var angleBetweenWestAndEastPlane = ViewProjection.GetHorizontalAngle(nearPlane, canvasWidth, canvasHeight);
        var angleBetweenNorthAndSouthPlane = ViewProjection.GetVerticalAngle(nearPlane, canvasWidth, canvasHeight);
        var translation = boundedBox.GetNonIntersectionDistanceOfBoundedBox(angleBetweenWestAndEastPlane, angleBetweenNorthAndSouthPlane);
        pointCloude = pointCloude.AsParallel().Select(p => translation * p).ToList();

       // getting frustum planes
        var planeWest = CardinalDirectionsCreator.CreateWest(angleBetweenWestAndEastPlane);
        var planeEast = CardinalDirectionsCreator.CreateWest(angleBetweenWestAndEastPlane);
        var planeNorth = CardinalDirectionsCreator.CreateWest(angleBetweenNorthAndSouthPlane);
        var planeSouth = CardinalDirectionsCreator.CreateWest(angleBetweenNorthAndSouthPlane);

        // get minimal distant position to every cardinal plane
        var minPositionWest = pointCloude.MinDistanceToPlane(planeWest);
        var minPositionEast = pointCloude.MinDistanceToPlane(planeEast);
        var minPositionNorth = pointCloude.MinDistanceToPlane(planeNorth);
        var minPositionSouth = pointCloude.MinDistanceToPlane(planeSouth);

        // get horizontal position of point cloud
        var distanceWestToEastinHorizontalDirection = Math.Abs(minPositionEast.Y - minPositionWest.Y);
        var distanceWestToEastInCameraDirection = Math.Abs(minPositionEast.X - minPositionWest.X);
        var distancePlaneToMinPositionInHorizontalDirection = distanceWestToEastInCameraDirection * Math.Tan(angleBetweenWestAndEastPlane);

        var widthBetweenWestAndEast = distancePlaneToMinPositionInHorizontalDirection + distanceWestToEastinHorizontalDirection;
        var heightForWestEastPlane = (widthBetweenWestAndEast / 2.0) / (Math.Tan(angleBetweenWestAndEastPlane));

        //var horizontalTranslation
        //    = minPositionEast.X < minPositionWest
        //    ? 

    }

}
