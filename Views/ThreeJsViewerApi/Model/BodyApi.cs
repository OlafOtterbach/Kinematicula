namespace ThreeJsViewerApi.Model
{
    public record BodyApi(
        Guid Id,
        string Name,
        EulerApi Frame,
        PositionApi[] Positions,
        NormalApi[] Normals,
        EdgeApi[] Edges);
}
