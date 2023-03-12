namespace ThreeJsViewerApi.Converters;

using Kinematicula.LogicViewing;
using ThreeJsViewerApi.EventModel;

public static class TouchEventTjsToTouchEvent
{
    public static TouchEvent ToTouchEvent(this TouchEventTjs touchEventTjs)
    {
        var touchEvent = new TouchEvent
        {
            IsBodyTouched = touchEventTjs.IsBodyTouched,
            BodyId = touchEventTjs.BodyId,
            TouchPosition = touchEventTjs.TouchPosition.ToPosition3D(),
            CameraId = touchEventTjs.CameraId,
            CanvasWidth = touchEventTjs.CanvasWidth,
            CanvasHeight = touchEventTjs.CanvasHeight
        };
        return touchEvent;
    }
}
