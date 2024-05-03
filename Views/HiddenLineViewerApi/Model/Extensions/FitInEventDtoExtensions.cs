namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;

public static class FitInEventDtoExtensions
{
    public static FitInEvent ToFitInEvent(this FitInEventDto fitInEventDto)
    {
        var fitInEvent = new FitInEvent
        {
            CameraId = fitInEventDto.camera.Id,
            CanvasWidth = fitInEventDto.canvasWidth,
            CanvasHeight = fitInEventDto.canvasHeight
        };

        return fitInEvent;
    }
}
