namespace ThreeJsViewerApi;

using Kinematicula.Graphics;
using Kinematicula.LogicViewing;
using ThreeJsViewerApi.Converters;
using ThreeJsViewerApi.EventModel;
using ThreeJsViewerApi.GraphicsModel;

public class ThreeJsViewerLogic : IThreeJsViewerLogic
{
    private readonly ILogicView _view;

    public ThreeJsViewerLogic(ILogicView view)
    {
        _view = view;
    }

    public SceneTjs GetScene()
    {
        var bodiesTjs = _view.Scene.Bodies.Where(body => !(body is Camera)).Select(body => body.ToBodyTjs()).ToArray();
        var camerasTjs = _view.Scene.Bodies.OfType<Camera>().Select(camera => camera.ToCameraTjs()).ToArray();
        var sceneTjs = new SceneTjs(bodiesTjs, camerasTjs);

        return sceneTjs;
    }

    public SelectedBodyStateTjs SelectBody(SelectEventTjs selectEventTjs)
    {
        var selectEvent = selectEventTjs.ToSelectEvent();
        var selection = _view.SelectBody(selectEvent).ToBodySelectionTjs();
        return selection;
    }

    public SceneStateTjs Zoom(ZoomEventTjs zoomEventTjs)
    {
        var zoomEvent = zoomEventTjs.ToZoomEvent();
        var zoomedCamera = _view.Zoom(zoomEvent);
        var lines = _hiddenLineService.GetHiddenLineGraphics(_view.Scene, zoomedCamera, zoomEvent.CanvasWidth, zoomEvent.CanvasHeight).ToColoredLines();
        var sceneState = new SceneStateTjs()
        {
            Camera = zoomedCamera.ToCameraTjs(),
            ColoredDrawLines = lines
        };

        return sceneState;
    }
}