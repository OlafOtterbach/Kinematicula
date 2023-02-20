namespace ThreeJsViewerApi.GraphicsModel
{
    public record BodyTjs(
        Guid Id,
        string Name,
        FrameTjs Frame,
        VertexTjs[] Vertices,
        int[] Indices,
        PositionTjs[] EdgePoints,
        int[] EdgeIndices);
}
