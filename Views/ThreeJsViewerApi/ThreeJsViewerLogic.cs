﻿namespace ThreeJsViewerApi;

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

    public SceneStateTjs Touch(TouchEventTjs touchEventTjs)
    {
        var touchEvent = touchEventTjs.ToTouchEvent();
        var touchCamera = _view.Touch(touchEvent);

        var items = _view.Scene
                         .Bodies
                         .Where(body => !(body is Camera))
                         .Select(body => new BodyItemTjs(body.Id, body.Frame.ToFrameTjs()))
                         .ToArray();

        var sceneState = new SceneStateTjs(touchCamera.ToCameraTjs(), items);

        return sceneState;
    }

    public SceneStateTjs Move(MoveEventTjs moveEventTjs)
    {
        var moveEvent = moveEventTjs.ToMoveEvent();
        var rotatedCamera = _view.Move(moveEvent);

        var items = _view.Scene
                         .Bodies
                         .Where(body => !(body is Camera))
                         .Select(body => new BodyItemTjs(body.Id, body.Frame.ToFrameTjs()))
                         .ToArray();

        var sceneState = new SceneStateTjs(rotatedCamera.ToCameraTjs(), items);

        return sceneState;
    }


    public SceneStateTjs Zoom(ZoomEventTjs zoomEventTjs)
    {
        var zoomEvent = zoomEventTjs.ToZoomEvent();
        var zoomedCamera = _view.Zoom(zoomEvent);

        var items = _view.Scene
                         .Bodies
                         .Where(body => !(body is Camera))
                         .Select(body => new BodyItemTjs(body.Id, body.Frame.ToFrameTjs()))
                         .ToArray();

        var sceneState = new SceneStateTjs(zoomedCamera.ToCameraTjs(), items);

        return sceneState;
    }
}