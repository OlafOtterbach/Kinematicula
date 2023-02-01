namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;
using Kinematicula.Scening;

public static class ZoomEventDtoExtensions
{
    public static ZoomEvent ToZoomEvent(this ZoomEventDto zoomEventDto, Scene scene)
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