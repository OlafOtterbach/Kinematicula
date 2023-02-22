namespace Kinematicula.Mathematics;

public static class ViewProjection
{
    public static double GetFrustumInRadiant(double nearPlane, double canvasWidth, double canvasHeight)
    {
        var planeHeight = canvasHeight < canvasWidth ? 1.0 : canvasWidth / canvasHeight;
        var frustum = 2.0 * Math.Atan(planeHeight / 1.0);
        return frustum;
    }

    public static Position3D ProjectCanvasToSceneSystem(double canvasX, double canvasY, double canvasWidth, double canvasHeight, double nearPlaneDist, Matrix44D cameraFrame)
    {
        var (x, y) = ViewProjection.ProjectCanvasToCameraPlane(canvasX, canvasY, canvasWidth, canvasHeight);
        var posCameraSystem = ViewProjection.ProjectCameraPlaneToCameraSystem(x, y, nearPlaneDist);
        var posSceneSystem = ViewProjection.ProjectCameraSystemToSceneSystem(posCameraSystem, cameraFrame);
        return posSceneSystem;
    }

    public static (double canvasX, double canvasY) ProjectSceneSystemToCanvas(Position3D position, double canvasWidth, double canvasHeight, double nearPlaneDist, Matrix44D cameraFrame)
    {
        var cameraSystemPos = ProjectSceneSystemToCameraSystem(position, cameraFrame);
        var (cameraPlaneX, cameraPlaneY) = ProjectCameraSystemToCameraPlane(cameraSystemPos, nearPlaneDist);
        var (canvasX, canvasY) = ProjectCameraPlaneToCanvas(cameraPlaneX, cameraPlaneY, canvasWidth, canvasHeight);
        return (canvasX, canvasY);
    }

    public static Position3D ProjectCameraSystemToSceneSystem(this Position3D position, Matrix44D cameraFrame)
    {
        var positionInScene = cameraFrame * position;
        return positionInScene;
    }

    public static Position3D ProjectSceneSystemToCameraSystem(this Position3D position, Matrix44D cameraFrame)
    {
        var cameraPosition = cameraFrame.Inverse() * position;
        return cameraPosition;
    }

    public static Position3D ProjectCameraPlaneToCameraSystem(double cameraPlaneX, double cameraPlaneY, double nearPlaneDist)
    {
        var positionInCameraCoordinates = new Position3D(nearPlaneDist, -cameraPlaneX, cameraPlaneY);
        return positionInCameraCoordinates;
    }

    public static (double, double) ProjectCameraSystemToCameraPlane(this Position3D position, double nearPlaneDist)
    {
        double cameraPlaneX = -(nearPlaneDist / position.X) * position.Y;
        double cameraPlaneY =  (nearPlaneDist / position.X) * position.Z;
        return (cameraPlaneX, cameraPlaneY);
    }

    public static (double, double) ProjectCanvasToCameraPlane(double canvasX, double canvasY, double canvasWidth, double canvasHeight)
    {
        var size = Math.Min(canvasWidth, canvasHeight);
        var x = (canvasX - canvasWidth / 2.0) / size;
        var y = (canvasHeight / 2.0 - canvasY) / size;
        return (x, y);
    }

    public static (int x, int y) ProjectCameraPlaneToCanvas(double cameraPlaneX, double cameraPlaneY, double canvasWidth, double canvasHeight)
    {
        var size = Math.Min(canvasWidth, canvasHeight);
        var canvasX = (int)(cameraPlaneX * size + canvasWidth / 2.0);
        var canvasY = (int)(canvasHeight - (cameraPlaneY * size + canvasHeight / 2.0));
        return (canvasX, canvasY);
    }
}
