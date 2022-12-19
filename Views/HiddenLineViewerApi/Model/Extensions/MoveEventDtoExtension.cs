using Kinematicula.LogicViewing;
using Kinematicula.Mathematics;

namespace HiddenLineViewerApi
{
    public static class MoveEventDtoExtension
    {
        public static MoveEvent ToMoveEvent(this MoveEventDto moveEventDto)
        {
            var moveEvent = new MoveEvent()
            {
                EventSource = moveEventDto.EventSource,
                SelectedBodyId = moveEventDto.BodyId,
                BodyTouchPosition = new Position3D(moveEventDto.BodyIntersection.X, moveEventDto.BodyIntersection.Y, moveEventDto.BodyIntersection.Z),
                StartMoveX = moveEventDto.StartX,
                StartMoveY = moveEventDto.StartY,
                EndMoveX = moveEventDto.EndX,
                EndMoveY = moveEventDto.EndY,
                Camera = moveEventDto.Camera.ToCameraInfo(),
                CanvasWidth = moveEventDto.CanvasWidth,
                CanvasHeight = moveEventDto.CanvasHeight,
            };

            return moveEvent;
        }
    }
}
