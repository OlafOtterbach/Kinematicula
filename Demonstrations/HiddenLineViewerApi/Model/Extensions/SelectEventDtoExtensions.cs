using Kinematicula.LogicViewing;

namespace HiddenLineViewerApi
{
    public static class SelectEventDtoExtensions
    {
        public static SelectEvent ToSelectEvent(this SelectEventDto selectEventDto)
        {
            var selectEvent = new SelectEvent
            {
                selectPositionX = selectEventDto.selectPositionX,
                selectPositionY = selectEventDto.selectPositionY,
                Camera = selectEventDto.camera.ToCameraInfo(),
                CanvasWidth = selectEventDto.canvasWidth,
                CanvasHeight = selectEventDto.canvasHeight
            };

            return selectEvent;
        }
    }
}
