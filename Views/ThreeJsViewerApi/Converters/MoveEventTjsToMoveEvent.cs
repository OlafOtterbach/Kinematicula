namespace ThreeJsViewerApi.Converters;

using Kinematicula.LogicViewing;
using Kinematicula.Mathematics;
using ThreeJsViewerApi.EventModel;

public static class MoveEventTjsToMoveEvent
{
    public static MoveEvent ToMoveEvent(this MoveEventTjs moveEventTjs)
    {
        var moveEvent = new MoveEvent()
        {
            EventSource = moveEventTjs.EventSource,
            SelectedBodyId = moveEventTjs.BodyId,
            BodyTouchPosition = new Position3D(moveEventTjs.BodyIntersection.X, moveEventTjs.BodyIntersection.Y, moveEventTjs.BodyIntersection.Z),
            StartMoveX = moveEventTjs.StartX,
            StartMoveY = moveEventTjs.StartY,
            EndMoveX = moveEventTjs.EndX,
            EndMoveY = moveEventTjs.EndY,
            CameraId = moveEventTjs.CameraId,
            CanvasWidth = moveEventTjs.CanvasWidth,
            CanvasHeight = moveEventTjs.CanvasHeight,
        };

        return moveEvent;
    }
}
