namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;

public static class SelectEventDtoExtensions
{
    public static SelectEvent ToSelectEvent(this SelectEventDto selectEventDto)
    {
        var selectEvent = new SelectEvent
        {
            selectPositionX = selectEventDto.selectPositionX,
            selectPositionY = selectEventDto.selectPositionY,
            CameraId = selectEventDto.camera.Id,
            CanvasWidth = selectEventDto.canvasWidth,
            CanvasHeight = selectEventDto.canvasHeight
        };

        return selectEvent;
    }
}
