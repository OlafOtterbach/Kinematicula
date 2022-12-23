namespace ThreeJsViewerApi.Model
{
    public record BodyTjs(
        Guid Id,
        string Name,
        EulerFrameTjs Frame,
        TriangleTjs[] Triangles,
        VertexTjs[] Vertices,
        EdgeTjs[] Edges);
}
