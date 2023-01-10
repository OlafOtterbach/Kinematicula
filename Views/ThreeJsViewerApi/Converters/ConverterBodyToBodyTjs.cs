﻿using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using ThreeJsViewerApi.Helpers;
using ThreeJsViewerApi.Model;

namespace ThreeJsViewerApi.Converters;

public static class ConverterBodyToBodyTjs
{
    public static BodyTjs ToBodyTjs(this Body body)
    {
        var result = ConvertFacesToTrianglesTjsAndVerticesTjs(body.Faces);
        var edgseTjs = body.Edges.ToEdgesTjs();

        BodyTjs bodyTjs = new BodyTjs(body.Id, body.Name, body.Frame.ToFrameTjs(), result.VerticesTjs, result.IndicesTjs, edgseTjs);
        return bodyTjs;
    }

    private static (int[] IndicesTjs, VertexTjs[] VerticesTjs) ConvertFacesToTrianglesTjsAndVerticesTjs(IEnumerable<Face> faces)
    {
        var vertexComparer = new VertexComparer();
        var index = 0;
        var trianglesTjs = new List<TriangleTjs>();
        var indices = new List<int>();
        var vertexDict = new Dictionary<VertexTjs, (int Index, VertexTjs Vertex)>(vertexComparer);
        foreach (var face in faces)
        {
            var triangles = face.Triangles;
            foreach (var triangle in triangles)
            {
                var vertex1 = -1;
                var vertexTjs1 = triangle.P1.ToVertexTjs(face.Color);
                if (vertexDict.ContainsKey(vertexTjs1))
                {
                    vertex1 = vertexDict[vertexTjs1].Index;
                }
                else
                {
                    vertexDict[vertexTjs1] = (index, vertexTjs1);
                    vertex1 = index++;
                }

                var vertex2 = -1;
                var vertexTjs2 = triangle.P2.ToVertexTjs(face.Color);
                if (vertexDict.ContainsKey(vertexTjs2))
                {
                    vertex2 = vertexDict[vertexTjs2].Index;
                }
                else
                {
                    vertexDict[vertexTjs2] = (index, vertexTjs2);
                    vertex2 = index++;
                }

                var vertex3 = -1;
                var vertexTjs3 = triangle.P3.ToVertexTjs(face.Color);
                if (vertexDict.ContainsKey(vertexTjs3))
                {
                    vertex3 = vertexDict[vertexTjs3].Index;
                }
                else
                {
                    vertexDict[vertexTjs3] = (index, vertexTjs3);
                    vertex3 = index++;
                }

                indices.Add(vertex1);
                indices.Add(vertex2);
                indices.Add(vertex3);
            }
        }

        var verticesTjs = vertexDict.Values.Select(pair => pair.Vertex).ToArray();

        return (indices.ToArray(), verticesTjs);
    }

    private static VertexTjs ToVertexTjs(this Vertex vertex, Color color)
    {
        var positionTjs = new PositionTjs(vertex.Point.Position.X, vertex.Point.Position.Y, vertex.Point.Position.Z);
        var normalTjs = new NormalTjs(vertex.Normal.X, vertex.Normal.Y, vertex.Normal.Z);
        var colorTjs = new ColorTjs(color.Alpha, color.Red, color.Green, color.Blue);
        var vertexTjs = new VertexTjs(positionTjs, normalTjs, colorTjs);

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
