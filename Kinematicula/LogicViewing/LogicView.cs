namespace Kinematicula.LogicViewing;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.LogicViewing.Extensions;
using Kinematicula.LogicViewing.Mathmatics;
using Kinematicula.LogicViewing.Services;
using Kinematicula.LogicViewing.Services.Sensors;
using Kinematicula.Mathematics;
using Kinematicula.Scening;

public class LogicView : ILogicView
{
    private readonly IMoveSensorProcessor _moveSensorProcessors;

    public LogicView(Scene scene) : this(scene, new MoveSensorProcessor())
    { }

    public LogicView(Scene scene, IMoveSensorProcessor moveSensorProcessor)
    {
        Scene = scene;
        _moveSensorProcessors = moveSensorProcessor;
    }

    public Scene Scene { get; }


    public Camera GetCamera(string cameraName)
    {
        var cameras = Scene.Bodies.OfType<Camera>().ToList();
        var camera = cameras.FirstOrDefault(x => x.Name == cameraName) ?? cameras.First();
        return camera;
    }

    public SelectedBodyState SelectBody(SelectEvent selectEvent)
    {
        var camera = Scene.GetCamera(selectEvent.CameraId);
        var posScene = ViewProjection.ProjectCanvasToSceneSystem(selectEvent.selectPositionX, selectEvent.selectPositionY, selectEvent.CanvasWidth, selectEvent.CanvasHeight, camera.NearPlane,camera.Frame);
        var rayOffset = camera.Frame.Offset;
        var rayDirection = posScene - rayOffset;
        var (isIntersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);

        return new SelectedBodyState { SelectedBodyId = isIntersected ? body.Id : Guid.Empty, IsBodySelected = isIntersected, BodyIntersection = intersection };
    }

    public Camera Touch(TouchEvent touchEvent)
    {
        var camera = Scene.GetCamera(touchEvent.CameraId);

        if (touchEvent.IsBodyTouched)
        {
            var body = Scene.GetBody(touchEvent.BodyId);
            var absoluteTouchPosition = body.Frame * touchEvent.TouchPosition;
            camera.MoveTargetTo(absoluteTouchPosition);
            Scene.UpdateCamera(camera);
        }

        return camera;
    }

    public Camera Move(MoveEvent moveEvent)
    {
        var camera = Scene.GetCamera(moveEvent.CameraId);

        if (!_moveSensorProcessors.Process(moveEvent.ToMoveAction(Scene), Scene))
        {
            var deltaX = moveEvent.EndMoveX - moveEvent.StartMoveX;
            var deltaY = moveEvent.EndMoveY - moveEvent.StartMoveY;
            Orbit(camera, deltaX, deltaY, moveEvent.CanvasWidth, moveEvent.CanvasHeight);
            Scene.UpdateCamera(camera);
        }

        return camera;
    }

    public Camera Zoom(ZoomEvent zoomEvent)
    {
        var camera = Scene.GetCamera(zoomEvent.CameraId);

        var dy = zoomEvent.Delta * 2.0;

        camera.Zoom(dy);
        Scene.UpdateCamera(camera);

        return camera;
    }

    private void Orbit(Camera camera, double pixelDeltaX, double pixelDeltaY, int canvasWidth, int canvasHeight)
    {
        var horicontalPixelFor360Degree = canvasWidth;
        var verticalPixelFor360Degree = canvasHeight;
        var alpha = -(360.0 * pixelDeltaX / horicontalPixelFor360Degree).ToRadiant();
        var beta = -(360.0 * pixelDeltaY / verticalPixelFor360Degree).ToRadiant();
        camera.OrbitXY(alpha);
        camera.OrbitXZ(beta);
    }
}