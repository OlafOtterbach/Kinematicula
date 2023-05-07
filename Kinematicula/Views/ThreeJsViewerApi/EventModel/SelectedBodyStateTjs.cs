namespace Kinematicula.Views.ThreeJsViewerApi.EventModel;

using Kinematicula.Views.ThreeJsViewerApi.GraphicsModel;

public class SelectedBodyStateTjs
{
    public bool IsBodyIntersected { get; set; }

    public Guid BodyId { get; set; }

    public PositionTjs BodyIntersection { get; set; }
}
