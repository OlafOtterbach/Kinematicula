using Kinematicula.LogicViewing;

namespace CylinderDemonstration.Models.Extensions
{
    public static class TouchEventDtoExtension
    {
        public static TouchEvent ToTouchEvent(this TouchEventDto touchEventDto)
        {
            var touchEvent = new TouchEvent
            {
                IsBodyTouched = touchEventDto.IsBodyTouched,
                BodyId = touchEventDto.BodyId,
                TouchPosition = touchEventDto.TouchPosition.ToPosition3D(),
                Camera = touchEventDto.Camera.ToCamera(),
                CanvasWidth = touchEventDto.CanvasWidth,
                CanvasHeight = touchEventDto.CanvasHeight
            };
            return touchEvent;
        }
    }
}
