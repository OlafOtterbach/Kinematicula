using Kinematicula.LogicViewing;
using ThreeJsViewerApi.Converters;
using ThreeJsViewerApi.Model;

namespace ThreeJsViewerApi;

public class ThreeJsViewerLogic : IThreeJsViewerLogic
{
    private readonly ILogicView _view;

    public ThreeJsViewerLogic(ILogicView view)
    {
        _view = view;
    }

    public SceneTjs GetScene(string cameraName)
    {
        var camera = _view.GetCamera(cameraName);
        var cameraTjs = new CameraTjs(camera.Name, camera.Frame.ToEulerFrameTjs());

        var bodiesTjs = _view.Scene.Bodies.Select(body => body.ToBodyTjs()).ToArray();

        var sceneTjs = new SceneTjs(cameraTjs, bodiesTjs);

        return sceneTjs;
    }
}