namespace HiddenLineViewerApi.HiddenLine;

using Kinematicula.Graphics;
using Kinematicula.Scening;

public interface IHiddenLineService
{
    Dictionary<ushort, ushort[]> GetHiddenLineGraphics(Scene scene, Camera camera, double canvasWidth, double canvasHeight);
}