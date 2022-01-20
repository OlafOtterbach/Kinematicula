using Kinematicula.Mathematics;
using Kinematicula.HiddenLineGraphics.Model;

namespace Kinematicula.HiddenLineGraphics.Services
{
    public static class CameraPlaneToCanvasProjecting
    {
        public static IEnumerable<ushort> ToLineCoordinates(this LineHL line, double canvasWidth, double canvasHeight)
        {
            var (x1, y1) = ViewProjection.ProjectCameraPlaneToCanvas(line.Start.X, line.Start.Y, canvasWidth, canvasHeight);
            var (x2, y2) = ViewProjection.ProjectCameraPlaneToCanvas(line.End.X, line.End.Y, canvasWidth, canvasHeight);
            yield return (ushort)x1;
            yield return (ushort)y1;
            yield return (ushort)x2;
            yield return (ushort)y2;
        }
    }
}