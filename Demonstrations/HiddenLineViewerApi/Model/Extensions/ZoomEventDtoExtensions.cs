using Kinematicula.LogicViewing;

namespace HiddenLineViewerApi
{
    public static class ZoomEventDtoExtensions
    {
        public static ZoomEvent ToZoomEvent(this ZoomEventDto zoomEventDto)
        {
            var zoomEvent = new ZoomEvent
            {
                Delta = zoomEventDto.delta,
                Camera = zoomEventDto.camera.ToCameraInfo(),
                CanvasWidth = zoomEventDto.canvasWidth,
                CanvasHeight = zoomEventDto.canvasHeight
            };

            return zoomEvent;
        }
    }
}