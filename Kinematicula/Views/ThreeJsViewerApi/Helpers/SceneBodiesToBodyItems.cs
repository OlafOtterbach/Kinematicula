namespace Kinematicula.Views.ThreeJsViewerApi.Helpers;

using Kinematicula.Graphics;
using Kinematicula.Scening;
using Kinematicula.Views.ThreeJsViewerApi.EventModel;
using Kinematicula.Views.ThreeJsViewerApi.Converters;

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
