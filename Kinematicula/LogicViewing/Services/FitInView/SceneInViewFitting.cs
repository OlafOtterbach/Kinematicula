//
//   Point Cloud:
//   
//                               dw
//               |----------------------------------------|
//       
//                                         w = w2            
//                               |------------------------O P2    --
//                               |                                 |
//                               |                                 |
//                               |                                 | dh
//                               |                                 |
//                     w1        |                                 |
//            P1 O---------------|                                --
//   
//   
//   Eye-Sight-Zone: Put the Point cloud inside:
//   
//                            +x
//                          /|\
//                           |
//                           W
//     |---------------------------------------------|
//                            |
//                            |  dw
//            |--------------------------------------|
//   
//   \                                                 /
//    .  dl                   |         w = w2        .
//     \------|               |----------------------/    --       --
//      \     |               |                     /      |        |
//       \    |dh             |                    /       |        |
//        \   |               |                   /        |        | dh
//         \  |               |                  /         |        |
//          \ |     w1        |                 /          |        |
//           \----------------|                /           |       --
//            .               .               .            |
//             \              |              /             |
//              \             |             /              |
//               \            |            /               |
//                \           |           /                | h
//                 \          |          /                 |
//                  \         |         /                  |
//                   \        |        /                   |
//                    \   ----|       /                    |
//                     \ / @/ |      /                     |
//                      \  /2 |     /                      |
//                       \    |    /                       |
//                        \  ---  /                        |
//                         \/ @ \/                         |
//                          \   /                          |
//                           \|/                          --
//                  +y  <-----O
//       
//   
//   
//   dw = abs( y(P1) - y(P2) )
//   dh = abs( x(P1) - x(P2) )
//   dl = dh * tan(@/2)
//   sign = ( x(P1) > x(P2) ? +1 : -1 )
//   w = sign (dw + dl) / 2   
//   h = w / tan(@/2)
//   P = ( x(P1) > x(P2) ? P1 : P2 )
//   ty = w - y(P)
//   ----------------------
//   
//   Same calculation for vertical sight view with h' w'
//   
//          h'
//       |-----|
//     
//       +z   /                      ---
//       |\  /                        |
//       |  /                         |
//       | /                          |
//       |/                          ---
//       O--------------> +x
//        \
//         \
//          \
//           \
//       
//   tz = w' - z(P')   
//   ----------------------
//   
//   ty = w - y(P)
//   tz = w' - z(P')
//   tx = (h > h') ? h - x(P) : h' - x(P')
//   
//   => T = (Tx Tz Ty TmoveCloudeInSight TcloudInCameraSystem) ^ -1
//
namespace Kinematicula.LogicViewing.Services.FitInView;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.LogicViewing.Mathmatics;
using Kinematicula.LogicViewing.Services.FitInView.Services;
using Kinematicula.Mathematics;
using Kinematicula.Scening;
using static Math;

public static class SceneInViewFitting
{
    public static void FitInView(this Scene scene, Camera camera, double canvasWidth, double canvasHeight)
    {
        // getting point cloude in camera coordinate system
        var M_CameraSystem = camera.Frame.Inverse();
        var pointCloudInCameraSystem = scene.Bodies.GetPointCloude(M_CameraSystem);

        // getting angles between frustum virew planes
        var angleBetweenLeftAndRightPlane = ViewProjection.GetHorizontalAngle(camera.NearPlane, canvasWidth, canvasHeight);
        var angleBetweenTopAndBottomPlane = ViewProjection.GetVerticalAngle(camera.NearPlane, canvasWidth, canvasHeight);

        // getting point cloud translated to position in the view frustum without intersecting it.
        var (pointCloudInViewFrustum, translationInViewFrustum) = TranslatePointCloudeInViewFrustum(
                                                                    pointCloudInCameraSystem,
                                                                    angleBetweenLeftAndRightPlane,
                                                                    angleBetweenTopAndBottomPlane);

        // getting minimal distant points of the point cloud to frustum view planes.
        var (left, right, top, bottom) = GetExtremasOfPointCloud(
            pointCloudInViewFrustum,
            angleBetweenLeftAndRightPlane,
            angleBetweenTopAndBottomPlane);

        // get horizontal move to fit in frustum view
        var dw_h = Abs(left.Y - right.Y);
        var dh_h = Abs(left.X - right.X);
        var dl_h = dh_h * Tan(angleBetweenLeftAndRightPlane / 2.0);
        var sign_h = left.X > right.X ? +1 : -1;
        var w_h = sign_h * (dw_h + dl_h) / 2.0;
        var h_h = w_h / Tan(angleBetweenLeftAndRightPlane / 2.0);
        var p_h = left.X > right.X ? left : right;
        var ty_h = w_h - p_h.Y;
        var MY = Matrix44D.CreateTranslation(new Vector3D(0.0, ty_h, 0.0));

        // get vertical move to fit in frustum view
        var dw_v = Abs(top.Y - bottom.Y);
        var dh_v = Abs(top.X - bottom.X);
        var dl_v = dh_v * Tan(angleBetweenTopAndBottomPlane / 2.0);
        var sign_v = top.X > bottom.X ? +1 : -1;
        var w_v = sign_v * (dw_v + dl_v) / 2.0;
        var h_v = w_v / Tan(angleBetweenTopAndBottomPlane / 2.0);
        var p_v = left.X > right.X ? left : right;
        var tz_v = w_v - p_v.Y;
        var MZ = Matrix44D.CreateTranslation(new Vector3D(0.0, 0.0, tz_v));

        //  get move in camera direction to fit in frustum view 
        var tx = (h_h > h_v) ? h_h - p_h.X : h_v - p_v.X;
        var MX = Matrix44D.CreateTranslation(new Vector3D(tx, 0.0, 0.0));
    }

    public static IEnumerable<Position3D> GetPointCloudeInCameraSystem(Scene scene, Camera camera)
    {
        var cameraSystem = camera.Frame.Inverse();
        var pointCloud = scene.Bodies.GetPointCloude(cameraSystem);
        return pointCloud;
    }

    public static (IEnumerable<Position3D> PointCloud, Matrix44D Translation) TranslatePointCloudeInViewFrustum(
        IEnumerable<Position3D> pointCloud,
        double angleBetweenLeftAndRightPlane,
        double angleBetweenTopAndBottomPlane)
    {
        var boundedBox = pointCloud.GetBoundedBox();
        var translation = boundedBox.GetNonIntersectionDistanceOfBoundedBox(angleBetweenLeftAndRightPlane, angleBetweenTopAndBottomPlane);
        var translatedPointCloud = pointCloud.AsParallel().Select(p => translation * p).ToList();

        return (translatedPointCloud, translation);
    }

    public static (Position3D Left, Position3D Right, Position3D Top, Position3D Bottom)
    GetExtremasOfPointCloud(
        IEnumerable<Position3D> pointCloud,
        double angleBetweenLeftAndRightPlane,
        double angleBetweenTopAndBottomPlane)
    {
        // getting frustum planes
        var planeLeft = CardinalDirectionsCreator.CreateWest(angleBetweenLeftAndRightPlane);
        var planeRight = CardinalDirectionsCreator.CreateWest(angleBetweenLeftAndRightPlane);
        var planeTop = CardinalDirectionsCreator.CreateWest(angleBetweenTopAndBottomPlane);
        var planeBottom = CardinalDirectionsCreator.CreateWest(angleBetweenTopAndBottomPlane);

        // get minimal distant position to every cardinal plane
        var minPositionLeft = pointCloud.MinDistanceToPlane(planeLeft);
        var minPositionRight = pointCloud.MinDistanceToPlane(planeRight);
        var minPositionTop = pointCloud.MinDistanceToPlane(planeTop);
        var minPositionBottom = pointCloud.MinDistanceToPlane(planeBottom);

        return (minPositionLeft, minPositionRight,minPositionTop, minPositionBottom);
    }






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
        //    \---|       |-----------/   ---
        //     \  |       |          /     |
        //      \@|       |         /      |
        //       \--------|        /       |
        //        \       |       /        |
        //         \      |      /         |
        //          \    /|\    /          |
        //           \    |    /           |
        //            \   |x  /            |
        //             \--|  /             |
        //              \@| /              |
        //               \|/               |
        //     +y <-------O               ---
        //              camera
        //
        // @ = angleBetweenWestAndEastPlane / 2.0
        //
        var distanceWestToEastinHorizontalDirection = Math.Abs(minPositionEast.Y - minPositionWest.Y);
        var distanceWestToEastInCameraDirection = Math.Abs(minPositionEast.X - minPositionWest.X);
        var distancePlaneToMinPositionInHorizontalDirection = distanceWestToEastInCameraDirection * Math.Tan(angleBetweenWestAndEastPlane / 2.0);

        var widthBetweenWestAndEast = distancePlaneToMinPositionInHorizontalDirection + distanceWestToEastinHorizontalDirection;
        var heightForWestEastPlanes = (widthBetweenWestAndEast / 2.0) / (Math.Tan(angleBetweenWestAndEastPlane / 2.0));

        double yTranslationWestEast;
        if (minPositionWest.X < minPositionEast.X)
        {
            // west is nearer to camera
            var newPositionEast = -widthBetweenWestAndEast / 2.0;
            yTranslationWestEast = newPositionEast - minPositionEast.Y;
        }
        else
        {
            // east is nearer to camera
            var newPositionWest = widthBetweenWestAndEast / 2.0;
            yTranslationWestEast = newPositionWest - minPositionWest.Y;
        }


        // get vertical north-south position of point cloud analog west and east plane.
        var distanceNorthToSouthinHorizontalDirection = Math.Abs(minPositionNorth.Z - minPositionSouth.Z);

        var distanceNorthToSouthInCameraDirection = Math.Abs(minPositionEast.X - minPositionWest.X);
        var distancePlaneToMinPositionInVerticalDirection = distanceNorthToSouthInCameraDirection * Math.Tan(angleBetweenNorthAndSouthPlane / 2.0);

        var widthBetweenNorthAndSouth = distancePlaneToMinPositionInVerticalDirection + distanceNorthToSouthinHorizontalDirection;
        var heightForNorthSouthPlanes = (widthBetweenNorthAndSouth / 2.0) / (Math.Tan(angleBetweenNorthAndSouthPlane / 2.0));

        double zTranslationNorthSouth;
        if (minPositionNorth.X < minPositionSouth.X)
        {
            var newPositionSouth = -widthBetweenNorthAndSouth / 2.0;
            zTranslationNorthSouth = newPositionSouth - minPositionSouth.Z;
        }
        else
        {
            var newPositionNorth = widthBetweenNorthAndSouth / 2.0;
            zTranslationNorthSouth = newPositionNorth - minPositionNorth.Z;
        }

        var height = heightForWestEastPlanes > heightForNorthSouthPlanes ? heightForWestEastPlanes : heightForNorthSouthPlanes;
        var cameraDirectionTranslation = Matrix44D.CreateTranslation(new Vector3D(height, 0.0, 00.0));

        var horizontalTranlation = Matrix44D.CreateTranslation(new Vector3D(0.0, yTranslationWestEast, 0.0));
        var verticalTranslation = Matrix44D.CreateTranslation(new Vector3D(0.0, 0.0, zTranslationNorthSouth));

    }


}
