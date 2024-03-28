namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;

public static class FitInEventDtoExtensions
{
    public static FitInEvent ToFitInEvent(this FitInEventDto fitInEventDto)
    {
        var fitInEvent = new FitInEvent
        {
            CameraId = fitInEventDto.Camera.Id,
            CanvasWidth = fitInEventDto.CanvasWidth,
            CanvasHeight = fitInEventDto.CanvasHeight
        };

        return fitInEvent;
    }
}
