using Kinematicula.Scening;

namespace Kinematicula.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        SelectedBodyState SelectBody(SelectEvent selectEvent);

        Camera Touch(TouchEvent touchEvent);

        Camera Move(MoveEvent moveEvent);

        Camera Zoom(ZoomEvent zoomEvent);
    }
}