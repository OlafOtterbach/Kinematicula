using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using ThreeJsViewerApi.Helpers;
using ThreeJsViewerApi.Model;

namespace ThreeJsViewerApi.Converters;

public static class ConverterBodyToBodyTjs
{
    public static BodyTjs ToBodyTjs(this Body body)
    {
        var triangles = body.Faces.SelectMany(face => face.Triangles).ToList();
        var result = ConvertTrianglesToTrianglesTjsAndVerticesTjs(triangles);
        var edgseTjs = body.Edges.ToEdgesTjs();

        BodyTjs bodyTjs = new BodyTjs(body.Id, body.Name, body.Frame.ToFrameTjs(), result.TrianglesTjs, result.VerticesTjs, edgseTjs);
        return bodyTjs;
    }

    private static (TriangleTjs[] TrianglesTjs, VertexTjs[] VerticesTjs) ConvertTrianglesToTrianglesTjsAndVerticesTjs(IEnumerable<Triangle> triangles)
    {
        var vertexComparer = new VertexComparer();
        var index = 0;
        var trianglesTjs = new List<TriangleTjs>();
        var vertexDict = new Dictionary<Vertex, (int Index, VertexTjs Vertex)>(vertexComparer);
        foreach (var triangle in triangles)
        {
            var vertex1 = -1;
            if (vertexDict.ContainsKey(triangle.P1))
            {
                vertex1 = vertexDict[triangle.P1].Index;
            }
            else
            {
                vertexDict[triangle.P1] = (index, triangle.P1.ToVertexTjs());
                vertex1 = index++;
            }

            var vertex2 = -1;
            if (vertexDict.ContainsKey(triangle.P2))
            {
                vertex2 = vertexDict[triangle.P2].Index;
            }
            else
            {
                vertexDict[triangle.P2] = (index, triangle.P2.ToVertexTjs());
                vertex2 = index++;
            }

            var vertex3 = -1;
            if (vertexDict.ContainsKey(triangle.P3))
            {
                vertex3 = vertexDict[triangle.P3].Index;
            }
            else
            {
                vertexDict[triangle.P3] = (index, triangle.P3.ToVertexTjs());
                vertex3 = index++;
            }

            var triangleTjs = new TriangleTjs(vertex1, vertex2, vertex3);
            trianglesTjs.Add(triangleTjs);
        }

        var verticesTjs = vertexDict.Values.Select(pair => pair.Vertex).ToArray();

        return (trianglesTjs.ToArray(), verticesTjs);
    }

    private static VertexTjs ToVertexTjs(this Vertex vertex)
    {
        var position = new PositionTjs(vertex.Point.Position.X, vertex.Point.Position.Y, vertex.Point.Position.Z);
        var normal = new NormalTjs(vertex.Normal.X, vertex.Normal.Y, vertex.Normal.Z);
        var color = new ColorTjs(1.0, 0.0, 0.0, 1.0);
        var vertexTjs = new VertexTjs(position, normal, color);

        return vertexTjs;
    }

    private static EdgeTjs[] ToEdgesTjs(this Edge[] edges)
    {
        var edgesTjs = edges.Select(edge => edge.ToEdgeTjs()).ToArray();
        return edgesTjs;
    }

    private static EdgeTjs ToEdgeTjs(this Edge edge)
    {
        var edgeTjs = new EdgeTjs(
            new PositionTjs(edge.Start.Position.X, edge.Start.Position.Y, edge.Start.Position.Z),
            new PositionTjs(edge.End.Position.X, edge.End.Position.Y, edge.End.Position.Z));

        return edgeTjs;
    }
}
