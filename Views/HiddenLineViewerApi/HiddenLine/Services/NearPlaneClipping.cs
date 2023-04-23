namespace HiddenLineViewerApi.HiddenLine.Services;

using HiddenLineViewerApi.HiddenLine.Model;

public static class NearPlaneClipping
{
    public static IEnumerable<EdgeHL> ClippAtNearPlane(this IEnumerable<EdgeHL> lines, double nearPlaneDist)
    {
        foreach (var line in lines)
        {
            (bool success, EdgeHL clippedEdge) = ClippEdgeAtNearPlane(line, nearPlaneDist);
            if (success)
            {
                yield return clippedEdge;
            }
        }
    }

    private static (bool, EdgeHL) ClippEdgeAtNearPlane(EdgeHL edge, double nearPlaneDist)
    {
        var start = edge.Start;
        var end = edge.End;

        if ((start.X < nearPlaneDist) || (end.X < nearPlaneDist))
        {
            if ((start.X < nearPlaneDist) && (end.X < nearPlaneDist))
            {
                return (false, null);
            }
            var direction = end - start;
            var intersection = start;
            if (direction.Y != 0.0)
            {
                double lamda = Math.Abs((start.X - nearPlaneDist) / direction.X);
                intersection += direction * lamda;
            }
            if (start.X < nearPlaneDist)
            {
                start = intersection;
            }
            else
            {
                end = intersection;
            }
        }

        return (true, new EdgeHL { Start = start, End = end, First = edge.First, Second = edge.Second, Color = edge.Color, Edge = edge.Edge });
    }
}