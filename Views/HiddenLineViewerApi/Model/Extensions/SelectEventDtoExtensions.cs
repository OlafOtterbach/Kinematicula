namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;
using Kinematicula.Scening;

public static class SelectEventDtoExtensions
{
    public static SelectEvent ToSelectEvent(this SelectEventDto selectEventDto, Scene scene)
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
