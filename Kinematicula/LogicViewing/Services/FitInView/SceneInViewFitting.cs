namespace Kinematicula.LogicViewing.Services.FitInView;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.LogicViewing.Mathmatics;
using Kinematicula.LogicViewing.Services.FitInView.Services;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using Kinematicula.Scening;
using static Math;

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
        var M_CameraSystem = camera.Frame.Inverse();
        var pointCloudInCameraSystem = scene.Bodies.GetPointCloude(M_CameraSystem);

        // getting angles between frustum virew planes
        var angleBetweenLeftAndRightPlane = ViewProjection.GetHorizontalAngle(camera.NearPlane, canvasWidth, canvasHeight);
        var angleBetweenTopAndBottomPlane = ViewProjection.GetVerticalAngle(camera.NearPlane, canvasWidth, canvasHeight);

        // getting point cloud translated to position in the view frustum without intersecting it.
        //
        //                                  .       
        //   \             /         \       .    ./
        //    \           /           \ .   .   . /                     
        //     \         /             \         /             
        //      \  .    /      =>       \       /              
        //       \   . /  .              \     /                
        //      . \ . / .                 \   /                 
        //         \ /                     \ /                
        //          O                       O
        //

        var (pointCloudInViewFrustum, translationInViewFrustum) = TranslatePointCloudeInViewFrustum(
                                                                    pointCloudInCameraSystem,
                                                                    angleBetweenLeftAndRightPlane,
                                                                    angleBetweenTopAndBottomPlane);

        // getting minimal distant points of the point cloud to frustum view planes.
        //   
        //                               dw
        //               |----------------------------------------|
        //       
        //                        .                w = w2      .     
        //                  .            |------------------------O P2 right  --
        //                      .    .   |     .              .                |
        //                     .         |   .          .                      |
        //                               |                                     | dh
        //                 -             |    .          -                     |
        //                     w1        |                                     |
        //       left P1 O---------------|                                    --
        //
        var (left, right, top, bottom) = GetExtremasOfPointCloud(
            pointCloudInViewFrustum,
            angleBetweenLeftAndRightPlane,
            angleBetweenTopAndBottomPlane);

        // get horizontal move to fit in frustum view
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
        //   frustum-viev-Zone: Put the Point P1/P2 to inside:
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
        //   ty = w - y(P)        var dw_h = Abs(left.Y - right.Y);
        //
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
        //
        //
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
        //
        //   tx = (h > h') ? h - x(P) : h' - x(P')
        //
        var tx = (h_h > h_v) ? h_h - p_h.X : h_v - p_v.X;
        var MX = Matrix44D.CreateTranslation(new Vector3D(tx, 0.0, 0.0));

        // get the camera translation.
        //
        // T = (Tx Tz Ty TmoveCloudeInSight TcloudInCameraSystem) ^-1
        //
        var T = (MX * MZ * MY * translationInViewFrustum * M_CameraSystem).Inverse();

        camera.Frame = T;
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
        var planeLeft = FrustumViewPlanes.CreateWest(angleBetweenLeftAndRightPlane);
        var planeRight = FrustumViewPlanes.CreateWest(angleBetweenLeftAndRightPlane);
        var planeTop = FrustumViewPlanes.CreateWest(angleBetweenTopAndBottomPlane);
        var planeBottom = FrustumViewPlanes.CreateWest(angleBetweenTopAndBottomPlane);

        // get minimal distant position to every cardinal plane
        var minPositionLeft = pointCloud.MinDistanceToPlane(planeLeft);
        var minPositionRight = pointCloud.MinDistanceToPlane(planeRight);
        var minPositionTop = pointCloud.MinDistanceToPlane(planeTop);
        var minPositionBottom = pointCloud.MinDistanceToPlane(planeBottom);

        return (minPositionLeft, minPositionRight,minPositionTop, minPositionBottom);
    }
}
