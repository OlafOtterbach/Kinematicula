using Kinematicula.Views.HiddenLineViewerApi.Model;

namespace Kinematicula.Views.HiddenLineViewerApi;

public interface IHiddenLineViewerLogic
{
    SceneStateDto GetScene(string cameraName, int canvasWidth, int canvasHeight);
    SelectedBodyStateDto SelectBody(SelectEventDto selectEventDto);
    SceneStateDto Touch(TouchEventDto touchEventDto);
    SceneStateDto Move(MoveEventDto moveEventDto);
    SceneStateDto Zoom(ZoomEventDto zoomEventDto);
}
