namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;
using Kinematicula.LogicViewing.Extensions;
using Kinematicula.Scening;

public static class ZoomEventDtoExtensions
{
    public static ZoomEvent ToZoomEvent(this ZoomEventDto zoomEventDto, Scene scene)
    {
        var zoomEvent = new ZoomEvent
        {
            Delta = zoomEventDto.delta,
            Camera = scene.GetCamera(zoomEventDto.camera.Id),
            CanvasWidth = zoomEventDto.canvasWidth,
            CanvasHeight = zoomEventDto.canvasHeight
        };

        return zoomEvent;
    }
}