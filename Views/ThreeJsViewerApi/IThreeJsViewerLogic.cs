namespace ThreeJsViewerApi;

using ThreeJsViewerApi.EventModel;
using ThreeJsViewerApi.GraphicsModel;

public interface IThreeJsViewerLogic
{
    SceneTjs GetScene();

    SelectedBodyStateTjs SelectBody(SelectEventTjs selectEventTjs);

    SceneStateTjs Zoom(ZoomEventTjs zoomEventTjs);
}
