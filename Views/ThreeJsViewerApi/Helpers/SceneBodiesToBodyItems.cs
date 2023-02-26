namespace ThreeJsViewerApi.Helpers;

using Kinematicula.Graphics;
using Kinematicula.Scening;
using ThreeJsViewerApi.Converters;
using ThreeJsViewerApi.EventModel;

public static class SceneBodiesToBodyItems
{
    public static BodyItemTjs[] GetBodyItems(this Scene scene)
    {
        var items = scene
                         .Bodies
                         .Where(body => !(body is Camera))
                         .Select(body => new BodyItemTjs(body.Id, body.Frame.ToFrameTjs()))
                         .ToArray();
        return items;
    }
}
