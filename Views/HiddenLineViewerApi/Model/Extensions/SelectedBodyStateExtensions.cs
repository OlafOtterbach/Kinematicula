namespace HiddenLineViewerApi;

using Kinematicula.LogicViewing;

public static class SelectedBodyStateExtensions
{
    public static SelectedBodyStateDto ToBodySelectionDto(this SelectedBodyState selectedBodyState)
    {
        return new SelectedBodyStateDto
        {
            BodyId = selectedBodyState.SelectedBodyId,
            IsBodyIntersected = selectedBodyState.IsBodySelected,
            BodyIntersection = selectedBodyState.BodyIntersection.ToPositionDto()
        };
    }
}
