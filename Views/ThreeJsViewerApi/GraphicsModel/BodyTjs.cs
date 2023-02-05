namespace ThreeJsViewerApi.GraphicsModel
{
    public record BodyTjs(
        Guid Id,
        string Name,
        FrameTjs Frame,
        VertexTjs[] Vertices,
        int[] Indices,
        EdgeTjs[] Edges);
}
