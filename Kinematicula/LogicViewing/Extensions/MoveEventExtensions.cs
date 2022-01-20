using Kinematicula.Graphics.Extensions;
using Kinematicula.Scening;

namespace Kinematicula.LogicViewing.Extensions
{
    public static class MoveEventExtensions
    {
        public static MoveAction ToMoveAction(this MoveEvent moveEvent, Scene Scene)
        {
            var moveAction = new MoveAction()
            {
                EventSource = moveEvent.EventSource,
                Body = Scene.GetBody(moveEvent.SelectedBodyId),
                BodyTouchPosition = moveEvent.BodyTouchPosition,
                StartMoveX = moveEvent.StartMoveX,
                StartMoveY = moveEvent.StartMoveY,
                EndMoveX = moveEvent.EndMoveX,
                EndMoveY = moveEvent.EndMoveY,
                Camera = moveEvent.Camera,
                CanvasWidth = moveEvent.CanvasWidth,
                CanvasHeight = moveEvent.CanvasHeight
            };

            return moveAction;
        }
    }
}
