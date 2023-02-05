namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;

public static class ZoomEventDtoExtensions
{
    public static ZoomEvent ToZoomEvent(this ZoomEventDto zoomEventDto)
    {
        var zoomEvent = new ZoomEvent
        {
            Delta = zoomEventDto.delta,
            CameraId = zoomEventDto.camera.Id,
            CanvasWidth = zoomEventDto.canvasWidth,
            CanvasHeight = zoomEventDto.canvasHeight
        };

        return zoomEvent;
    }
}