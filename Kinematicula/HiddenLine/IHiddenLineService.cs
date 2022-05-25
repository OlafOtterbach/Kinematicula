using Kinematicula.LogicViewing;
using Kinematicula.Scening;

namespace Kinematicula.HiddenLineGraphics
{
    public interface IHiddenLineService
    {
        Dictionary<ushort, ushort[]> GetHiddenLineGraphics(Scene scene, CameraInfo camera, double canvasWidth, double canvasHeight);
    }
}