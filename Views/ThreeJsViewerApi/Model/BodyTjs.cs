namespace ThreeJsViewerApi.Model
{
    public record BodyTjs(
        Guid Id,
        string Name,
        FrameTjs Frame,
        TriangleTjs[] Triangles,
        VertexTjs[] Vertices,
        EdgeTjs[] Edges);
}
