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
        //        
        //      widthBetweenWestAndEast
        //   |-------------------------|
        //   
        //    \---|       |-----------/
        //     \  |       |          /
        //      \ |       |         /
        //       \--------|        /
        //        \       |       /
        //         \      |      /
        //          \     |     /
        //           \    |    /
        //            \   |x  /
        //             \  |  /
        //              \ | /
        //               \|/
        //      y <-------O
        //
        var distanceWestToEastinHorizontalDirection = Math.Abs(minPositionEast.Y - minPositionWest.Y);
        var distanceWestToEastInCameraDirection = Math.Abs(minPositionEast.X - minPositionWest.X);
        var distancePlaneToMinPositionInHorizontalDirection = distanceWestToEastInCameraDirection * Math.Tan(angleBetweenWestAndEastPlane);

        var widthBetweenWestAndEast = distancePlaneToMinPositionInHorizontalDirection + distanceWestToEastinHorizontalDirection;
        var heightForWestEastPlanes = (widthBetweenWestAndEast / 2.0) / (Math.Tan(angleBetweenWestAndEastPlane));

        Vector3D horizontalTranslation;
        if(minPositionWest.X < minPositionEast.X)
        {
            var newPositionEast = new Position3D(heightForWestEastPlanes, -widthBetweenWestAndEast, minPositionEast.Z);
            horizontalTranslation = newPositionEast - minPositionEast;
        }
        else
        {
            var newPositionWest = new Position3D(heightForWestEastPlanes, widthBetweenWestAndEast, minPositionWest.Z);
            horizontalTranslation = newPositionWest - minPositionWest;
        }


        // get vertical position of point cloud
        var distanceNorthToSouthinHorizontalDirection = Math.Abs(minPositionNorth.Z - minPositionSouth.Z);

        var distanceNorthToSouthInCameraDirection = Math.Abs(minPositionEast.X - minPositionWest.X);
        var distancePlaneToMinPositionInVerticalDirection = distanceNorthToSouthInCameraDirection * Math.Tan(angleBetweenNorthAndSouthPlane);

        var widthBetweenNorthAndSouth = distancePlaneToMinPositionInVerticalDirection + distanceNorthToSouthinHorizontalDirection;
        var heightForNorthSouthPlanes = (widthBetweenNorthAndSouth / 2.0) / (Math.Tan(angleBetweenNorthAndSouthPlane));

        Vector3D verticalTranslation;
        if (minPositionNorth.X < minPositionSouth.X)
        {
            var newPositionSouth = new Position3D(heightForNorthSouthPlanes, minPositionSouth.Y , -widthBetweenNorthAndSouth);
            verticalTranslation = newPositionSouth - minPositionSouth;
        }
        else
        {
            var newPositionWest = new Position3D(heightForNorthSouthPlanes, minPositionNorth.Y, widthBetweenNorthAndSouth);
            horizontalTranslation = newPositionWest - minPositionWest;
        }


    }

}
