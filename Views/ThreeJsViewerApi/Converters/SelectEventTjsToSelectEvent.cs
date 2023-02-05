namespace ThreeJsViewerApi.Converters;

using Kinematicula.LogicViewing;
using Kinematicula.Scening;
using ThreeJsViewerApi.EventModel;

public static class SelectEventTjsToSelectEvent
{
    public static SelectEvent ToSelectEvent(this SelectEventTjs selectEventTjs)
    {
        var selectEvent = new SelectEvent
        {
            selectPositionX = selectEventTjs.SelectPositionX,
            selectPositionY = selectEventTjs.SelectPositionY,
            CameraId = selectEventTjs.Camera.CameraId,
            CanvasWidth = selectEventTjs.CanvasWidth,
            CanvasHeight = selectEventTjs.CanvasHeight
        };

        return selectEvent;
    }
}
