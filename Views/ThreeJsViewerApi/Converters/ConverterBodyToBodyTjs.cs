﻿namespace ThreeJsViewerApi.Converters;

using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using ThreeJsViewerApi.GraphicsModel;
using ThreeJsViewerApi.Helpers;

public static class ConverterBodyToBodyTjs
{
    public static BodyTjs ToBodyTjs(this Body body)
    {
        var result = ConvertFacesToTrianglesTjsAndVerticesTjs(body.Faces);
        var (edgePoints, edgeIndices) = body.Edges.ToEdgesTjs();

        BodyTjs bodyTjs = new BodyTjs(body.Id, body.Name, body.Frame.ToFrameTjs(), result.VerticesTjs, result.IndicesTjs, edgePoints, edgeIndices, body.EdgeColor.ToColorTjs());
        return bodyTjs;
    }

    private static (int[] IndicesTjs, VertexTjs[] VerticesTjs) ConvertFacesToTrianglesTjsAndVerticesTjs(IEnumerable<Face> faces)
    {
        var vertexComparer = new VertexComparer();
        var index = 0;
        var trianglesTjs = new List<TriangleTjs>();
        var indices = new List<int>();
        var vertexDict = new Dictionary<VertexTjs, int>(vertexComparer);
        foreach (var face in faces)
        {
            var triangles = face.Triangles;
            foreach (var triangle in triangles)
            {
                var vertex1 = -1;
                var vertexTjs1 = triangle.P1.ToVertexTjs(face.Color);
                if (vertexDict.ContainsKey(vertexTjs1))
                {
                    vertex1 = vertexDict[vertexTjs1];
                }
                else
                {
                    vertexDict[vertexTjs1] = index;
                    vertex1 = index++;
                }

                var vertex2 = -1;
                var vertexTjs2 = triangle.P2.ToVertexTjs(face.Color);
                if (vertexDict.ContainsKey(vertexTjs2))
                {
                    vertex2 = vertexDict[vertexTjs2];
                }
                else
                {
                    vertexDict[vertexTjs2] = index;
                    vertex2 = index++;
                }

                var vertex3 = -1;
                var vertexTjs3 = triangle.P3.ToVertexTjs(face.Color);
                if (vertexDict.ContainsKey(vertexTjs3))
                {
                    vertex3 = vertexDict[vertexTjs3];
                }
                else
                {
                    vertexDict[vertexTjs3] = index;
                    vertex3 = index++;
                }

                indices.Add(vertex1);
                indices.Add(vertex2);
                indices.Add(vertex3);
            }
        }

        return (indices.ToArray(), vertexDict.Keys.ToArray());
    }

    private static VertexTjs ToVertexTjs(this Vertex vertex, Color color)
    {
        var positionTjs = new PositionTjs(vertex.Point.Position.X, vertex.Point.Position.Y, vertex.Point.Position.Z);
        var normalTjs = new NormalTjs(vertex.Normal.X, vertex.Normal.Y, vertex.Normal.Z);
        var colorTjs = new ColorTjs(color.Alpha, color.Red, color.Green, color.Blue);
        var vertexTjs = new VertexTjs(positionTjs, normalTjs, colorTjs);

        return vertexTjs;
    }

    private static (PositionTjs[] EdgePoints, int[] EdgeIndices) ToEdgesTjs(this Edge[] edges)
    {
        var startEndFromEdges = edges.Where(IsEdgeVisible).GetPositionsFromEdges().ToList();
        var positions = startEndFromEdges.Distinct().ToList();
        var positionIndexDict = positions.ToDictionary(p => p, p => positions.IndexOf(p));

        var edgeIndices = startEndFromEdges.Select(p => positionIndexDict[p]).ToArray();
        var edgePoints = positions.Select(p => p.ToPositionTjs()).ToArray();

        return (edgePoints, edgeIndices);
    }

    private static bool IsEdgeVisible(Edge edge)
    {
        var firstFace = edge.First.ParentTriangle.ParentFace;
        var secondFace = edge.Second.ParentTriangle.ParentFace;

        if(firstFace.Color.Green == 1.0)
        {
            var a = 1;
        }

        if(firstFace == secondFace)
        {
            var face = firstFace;
            if(!face.HasFacets && edge.First != edge.Second)
            {
                // Is inner edge of face and face not facetted
                return false;
            }
        }
        else
        {
            if (!firstFace.HasBorder && !secondFace.HasBorder)
            {
                // Is inner edge of face and face not facetted
                return false;
            }
        }

        return true;
    }

    private static IEnumerable<Position3D> GetPositionsFromEdges(this IEnumerable<Edge> edges)
    {
        foreach (var edge in edges)
        {
            yield return edge.Start.Position;
            yield return edge.End.Position;
        }
    }
}
