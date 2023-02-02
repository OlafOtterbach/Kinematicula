namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;
using Kinematicula.Scening;

public static class TouchEventDtoExtensions
{
    public static TouchEvent ToTouchEvent(this TouchEventDto touchEventDto, Scene scene)
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
