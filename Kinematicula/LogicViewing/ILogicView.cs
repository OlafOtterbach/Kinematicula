using Kinematicula.Scening;

namespace Kinematicula.LogicViewing
{
    public interface ILogicView
    {
        Scene Scene { get; }

        CameraInfo GetCamera(string cameraName);

        SelectedBodyState SelectBody(SelectEvent selectEvent);

        CameraInfo Touch(TouchEvent touchEvent);

        CameraInfo Move(MoveEvent moveEvent);

        CameraInfo Zoom(ZoomEvent zoomEvent);
    }
}