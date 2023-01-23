namespace HiddenLineViewerApi.HiddenLine;

using Kinematicula.LogicViewing;
using Kinematicula.Scening;

public interface IHiddenLineService
{
    Dictionary<ushort, ushort[]> GetHiddenLineGraphics(Scene scene, CameraInfo camera, double canvasWidth, double canvasHeight);
}