namespace Kinematicula.LogicViewing.Extensions;

using Kinematicula.Scening;

public static class MoveEventExtensions
{
    public static MoveAction ToMoveAction(this MoveEvent moveEvent, Scene scene)
    {
        var moveAction = new MoveAction()
        {
            EventSource = moveEvent.EventSource,
            Body = scene.GetBody(moveEvent.SelectedBodyId),
            BodyTouchPosition = moveEvent.BodyTouchPosition,
            StartMoveX = moveEvent.StartMoveX,
            StartMoveY = moveEvent.StartMoveY,
            EndMoveX = moveEvent.EndMoveX,
            EndMoveY = moveEvent.EndMoveY,
            Camera = scene.GetCamera(moveEvent.CameraId),
            CanvasWidth = moveEvent.CanvasWidth,
            CanvasHeight = moveEvent.CanvasHeight
        };

        return moveAction;
    }
}
