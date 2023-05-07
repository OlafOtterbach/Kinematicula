namespace Kinematicula.Views.ThreeJsViewerApi.Converters;

using Kinematicula.LogicViewing;
using Kinematicula.Views.ThreeJsViewerApi.EventModel;

public static class SelectedBodyStateToSelectedBodyStateTjs
{
    public static SelectedBodyStateTjs ToBodySelectionTjs(this SelectedBodyState selectedBodyState)
    {
        return new SelectedBodyStateTjs
        {
            BodyId = selectedBodyState.SelectedBodyId,
            IsBodyIntersected = selectedBodyState.IsBodySelected,
            BodyIntersection = selectedBodyState.BodyIntersection.ToPositionTjs()
        };
    }
}
