namespace Kinematicula.LogicViewing;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.LogicViewing.Extensions;
using Kinematicula.LogicViewing.Services;
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
        var posScene = ViewProjection.ProjectCanvasToSceneSystem(selectEvent.selectPositionX, selectEvent.selectPositionY, selectEvent.CanvasWidth, selectEvent.CanvasHeight, selectEvent.Camera.NearPlane, selectEvent.Camera.Frame);
        var rayOffset = selectEvent.Camera.Frame.Offset;
        var rayDirection = posScene - rayOffset;
        var (isIntersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);

        return new SelectedBodyState { SelectedBodyId = isIntersected ? body.Id : Guid.Empty, IsBodySelected = isIntersected, BodyIntersection = intersection };
    }

    public Camera Touch(TouchEvent touchEvent)
    {
        if (touchEvent.IsBodyTouched)
        {
            var body = Scene.GetBody(touchEvent.BodyId);
            var absoluteTouchPosition = body.Frame * touchEvent.TouchPosition;
            touchEvent.Camera.MoveTargetTo(absoluteTouchPosition);
            Scene.UpdateCamera(touchEvent.Camera);
        }

        return Scene.GetCamera(touchEvent.Camera.Name);
    }

    public Camera Select(SelectEvent selectEvent)
    {
        var posScene = ViewProjection.ProjectCanvasToSceneSystem(selectEvent.selectPositionX, selectEvent.selectPositionY, selectEvent.CanvasWidth, selectEvent.CanvasHeight, selectEvent.Camera.NearPlane, selectEvent.Camera.Frame);
        var rayOffset = selectEvent.Camera.Frame.Offset;
        var rayDirection = posScene - rayOffset;

        var (isintersected, intersection, body) = Scene.GetIntersectionOfRayAndScene(rayOffset, rayDirection);
        if (isintersected)
        {
            selectEvent.Camera.MoveTargetTo(intersection);
            Scene.UpdateCamera(selectEvent.Camera);
        }

        return Scene.GetCamera(selectEvent.Camera.Name);
    }

    public Camera Move(MoveEvent moveEvent)
    {
        if (!_moveSensorProcessors.Process(moveEvent.ToMoveAction(Scene), Scene))
        {
            var deltaX = moveEvent.EndMoveX - moveEvent.StartMoveX;
            var deltaY = moveEvent.EndMoveY - moveEvent.StartMoveY;
            moveEvent.Camera = Orbit(deltaX, deltaY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera);
            Scene.UpdateCamera(moveEvent.Camera);
        }

        return Scene.GetCamera(moveEvent.Camera.Name);
    }

    public Camera Zoom(ZoomEvent zoomEvent)
    {
        var dy = zoomEvent.Delta * 2.0;

        zoomEvent.Camera.Zoom(dy);
        Scene.UpdateCamera(zoomEvent.Camera);

        return Scene.GetCamera(zoomEvent.Camera.Name);
    }

    private Camera Orbit(double pixelDeltaX, double pixelDeltaY, int canvasWidth, int canvasHeight, Camera camera)
    {
        var horicontalPixelFor360Degree = canvasWidth;
        var verticalPixelFor360Degree = canvasHeight;
        var alpha = -(360.0 * pixelDeltaX / horicontalPixelFor360Degree).ToRadiant();
        var beta = -(360.0 * pixelDeltaY / verticalPixelFor360Degree).ToRadiant();
        camera.OrbitXY(alpha);
        camera.OrbitXZ(beta);
        return camera;
    }
}