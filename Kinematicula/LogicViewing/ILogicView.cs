using Kinematicula.Graphics;
using Kinematicula.Scening;

namespace Kinematicula.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        Camera GetCamera(string cameraName);

        SelectedBodyState SelectBody(SelectEvent selectEvent);

        Camera Touch(TouchEvent touchEvent);

        Camera Move(MoveEvent moveEvent);

        Camera Zoom(ZoomEvent zoomEvent);
    }
}