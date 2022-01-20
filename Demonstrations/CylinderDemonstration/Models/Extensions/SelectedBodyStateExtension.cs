using Kinematicula.LogicViewing;

namespace CylinderDemonstration.Models.Extensions
{
    public static class SelectedBodyStateExtension
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
}
