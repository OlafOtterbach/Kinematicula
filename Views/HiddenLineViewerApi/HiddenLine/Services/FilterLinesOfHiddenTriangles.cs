namespace HiddenLineViewerApi.HiddenLine.Services;

using HiddenLineViewerApi.HiddenLine.Model;
using System.Collections.Generic;
using System.Linq;

public static class FilterLinesOfHiddenTriangles
{
    public static IEnumerable<LineHL> FilterLinesOfVisibleTriangles(this IEnumerable<LineHL> lines) => lines.Where(IsLineVisible);

    private static bool IsLineVisible(LineHL line) => line.Edge.First.Spin == TriangleSpin.counter_clockwise || line.Edge.Second.Spin == TriangleSpin.counter_clockwise;
}
