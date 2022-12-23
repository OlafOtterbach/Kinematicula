using ThreeJsViewerApi.Model;

namespace ThreeJsViewerApi;

public interface IThreeJsViewerLogic
{
    SceneTjs GetScene(string cameraName);
}
