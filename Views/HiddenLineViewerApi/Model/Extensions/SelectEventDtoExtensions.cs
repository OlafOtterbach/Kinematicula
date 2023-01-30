using Kinematicula.LogicViewing;
using Kinematicula.LogicViewing.Extensions;
using Kinematicula.Scening;

namespace HiddenLineViewerApi
{
    public static class SelectEventDtoExtensions
    {
        public static SelectEvent ToSelectEvent(this SelectEventDto selectEventDto, Scene scene)
        {
            var selectEvent = new SelectEvent
            {
                selectPositionX = selectEventDto.selectPositionX,
                selectPositionY = selectEventDto.selectPositionY,
                Camera = scene.GetCamera(selectEventDto.camera.Name),
                CanvasWidth = selectEventDto.canvasWidth,
                CanvasHeight = selectEventDto.canvasHeight
            };

            return selectEvent;
        }
    }
}
