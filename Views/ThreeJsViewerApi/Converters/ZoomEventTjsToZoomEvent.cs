namespace ThreeJsViewerApi.Converters;

using Kinematicula.LogicViewing;
using ThreeJsViewerApi.EventModel;

public static class ZoomEventTjsToZoomEvent
{
    public static ZoomEvent ToZoomEvent(this ZoomEventTjs zoomEventTjs)
    {
        var zoomEvent = new ZoomEvent
        {
            Delta = zoomEventTjs.Delta,
            CameraId = zoomEventTjs.CameraId,
            CanvasWidth = zoomEventTjs.CanvasWidth,
            CanvasHeight = zoomEventTjs.CanvasHeight
        };

        return zoomEvent;
    }
}
