using Kinematicula.Scening;
using Kinematicula.LogicViewing;
using Kinematicula.Mathematics;
using Kinematicula.HiddenLineGraphics.Services;
using Kinematicula.HiddenLineGraphics.Model;

namespace Kinematicula.HiddenLineGraphics
{
    public class HiddenLineService : IHiddenLineService
    {
        public Dictionary<ushort, ushort[]> GetHiddenLineGraphics(Scene scene, Camera camera, double canvasWidth, double canvasHeight)
        {
            // Convert to hidden line graphics scene
            var sceneHL = scene.ToHiddenLineScene(camera);

            // clipp 3D lines at near planle
            var clippedEdges = sceneHL.Edges.ClippAtNearPlane(sceneHL.NearPlaneDistance);

            // Project 3D lines on Nearplane to 2D lines
            var planeLines = clippedEdges.ProjectToCameraPlane(sceneHL.NearPlaneDistance);

            // Filter lines with at least one visible triangle
            var visiblePlaneLines = planeLines.FilterLinesOfVisibleTriangles();

            // Clipp lines at the projected screen limits
            var (left, bottom) = ViewProjection.ProjectCanvasToCameraPlane(0, canvasHeight, canvasWidth, canvasHeight);
            var (right, top) = ViewProjection.ProjectCanvasToCameraPlane(canvasWidth, 0, canvasWidth, canvasHeight);
            var clippedLines = visiblePlaneLines.Clipp(left, right, bottom, top);

            // Cut 2D lines in uncutted 2D lines
            var cuttedLines = clippedLines.CutLines();

            var visibleLines = cuttedLines.FilterOutHiddenLines(sceneHL);

            var colorDictionary = GetColorDictionary(visibleLines);

            var coloredLinesDict = new Dictionary<ushort, ushort[]>();
            foreach(var color in colorDictionary.Keys)
            {
                coloredLinesDict[color] = colorDictionary[color].SelectMany(line => line.ToLineCoordinates(canvasWidth, canvasHeight)).ToArray();
            }

            return coloredLinesDict;
        }

        private Dictionary<ushort, List<LineHL>> GetColorDictionary(IEnumerable<LineHL> lines)
        {
            var dict = new Dictionary<ushort, List<LineHL>>();

            foreach(var line in lines)
            {
                var color = line.Color;
                if(!dict.ContainsKey(color))
                {
                    dict[color] = new List<LineHL>();
                }

                dict[color].Add(line);
            }

            return dict;
        }

    }
}