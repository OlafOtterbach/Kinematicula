﻿namespace HiddenLineViewerApi;

using HiddenLineViewerApi.HiddenLine;
using Kinematicula.LogicViewing;

public class HiddenLineViewerLogic : IHiddenLineViewerLogic
{
    private IHiddenLineService _hiddenLineService;
    private ILogicView _view;

    public HiddenLineViewerLogic(IHiddenLineService hiddenLineService, ILogicView view)
    {
        _hiddenLineService = hiddenLineService;
        _view = view;
    }

    public SceneStateDto GetScene(string cameraName, int canvasWidth, int canvasHeight)
    {
        var camera = _view.GetCamera(cameraName);
        var sceneState = new SceneStateDto()
        {
            Camera = camera.ToCameraDto(),
            ColoredDrawLines = _hiddenLineService.GetHiddenLineGraphics(_view.Scene, camera, canvasWidth, canvasHeight).ToColoredLines()
        };

        return sceneState;
    }

    public SelectedBodyStateDto SelectBody(SelectEventDto selectEventDto)
    {
        var selectEvent = selectEventDto.ToSelectEvent(_view.Scene);
        var selection = _view.SelectBody(selectEvent).ToBodySelectionDto();
        return selection;
    }

    public SceneStateDto Touch(TouchEventDto touchEventDto)
    {
        var touchEvent = touchEventDto.ToTouchEvent(_view.Scene);
        var touchCamera = _view.Touch(touchEvent);
        var lines = _hiddenLineService.GetHiddenLineGraphics(_view.Scene, touchCamera, touchEvent.CanvasWidth, touchEvent.CanvasHeight).ToColoredLines();
        var sceneState = new SceneStateDto()
        {
            Camera = touchCamera.ToCameraDto(),
            ColoredDrawLines = lines
        };

        return sceneState;
    }

    public SceneStateDto Move(MoveEventDto moveEventDto)
    {
        var moveEvent = moveEventDto.ToMoveEvent(_view.Scene);
        var rotatedCamera = _view.Move(moveEvent);
        var lines = _hiddenLineService.GetHiddenLineGraphics(_view.Scene, rotatedCamera, moveEvent.CanvasWidth, moveEvent.CanvasHeight).ToColoredLines();
        var sceneState = new SceneStateDto()
        {
            Camera = rotatedCamera.ToCameraDto(),
            ColoredDrawLines = lines
        };

        return sceneState;
    }

    public SceneStateDto Zoom(ZoomEventDto zoomEventDto)
    {
        var zoomEvent = zoomEventDto.ToZoomEvent(_view.Scene);
        var zoomedCamera = _view.Zoom(zoomEvent);
        var lines = _hiddenLineService.GetHiddenLineGraphics(_view.Scene, zoomedCamera, zoomEvent.CanvasWidth, zoomEvent.CanvasHeight).ToColoredLines();
        var sceneState = new SceneStateDto()
        {
            Camera = zoomedCamera.ToCameraDto(),
            ColoredDrawLines = lines
        };

        return sceneState;
    }
}