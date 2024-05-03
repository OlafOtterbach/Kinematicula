namespace ThreeJsViewerApi.Converters;

using Kinematicula.LogicViewing;
using ThreeJsViewerApi.EventModel;

public static class FitInEventTjsToFitInEvent
{
    public static FitInEvent ToFitInEvent(this FitInEventTjs fitInEventTjs)
    {
        var fitInEvent = new FitInEvent
        {
            CameraId = fitInEventTjs.CameraId,
            CanvasWidth = fitInEventTjs.CanvasWidth,
            CanvasHeight = fitInEventTjs.CanvasHeight
        };

        return fitInEvent;
    }
}
