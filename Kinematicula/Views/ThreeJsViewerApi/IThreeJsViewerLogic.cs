namespace Kinematicula.Views.ThreeJsViewerApi;

using Kinematicula.Views.ThreeJsViewerApi.EventModel;
using Kinematicula.Views.ThreeJsViewerApi.GraphicsModel;

public interface IThreeJsViewerLogic
{
    SceneTjs GetScene(double canvasWidth, double canvasHeight);

    SelectedBodyStateTjs SelectBody(SelectEventTjs selectEventTjs);

    SceneStateTjs Touch(TouchEventTjs touchEventTjs);

    SceneStateTjs Move(MoveEventTjs moveEventTjs);

    SceneStateTjs Zoom(ZoomEventTjs zoomEventTjs);
}
