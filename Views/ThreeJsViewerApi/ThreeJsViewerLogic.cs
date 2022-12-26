using Kinematicula.Graphics;
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

    public SceneTjs GetScene()
    {
        var bodiesTjs = _view.Scene.Bodies.Where(body => !(body is Camera)).Select(body => body.ToBodyTjs()).ToArray();
        var camerasTjs = _view.Scene.Bodies.OfType<Camera>().Select(camera => camera.ToCameraTjs()).ToArray();
        var sceneTjs = new SceneTjs(bodiesTjs, camerasTjs);

        return sceneTjs;
    }
}