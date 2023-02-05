namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;

public static class TouchEventDtoExtensions
{
    public static TouchEvent ToTouchEvent(this TouchEventDto touchEventDto)
    {
        var touchEvent = new TouchEvent
        {
            IsBodyTouched = touchEventDto.IsBodyTouched,
            BodyId = touchEventDto.BodyId,
            TouchPosition = touchEventDto.TouchPosition.ToPosition3D(),
            CameraId = touchEventDto.Camera.Id,
            CanvasWidth = touchEventDto.CanvasWidth,
            CanvasHeight = touchEventDto.CanvasHeight
        };
        return touchEvent;
    }
}
