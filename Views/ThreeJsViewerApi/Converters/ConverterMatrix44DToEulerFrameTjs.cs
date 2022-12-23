using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using ThreeJsViewerApi.Model;

namespace ThreeJsViewerApi.Converters;

public static class ConverterMatrix44DToEulerFrameTjs
{
    public static EulerFrameTjs ToEulerFrameTjs(this Matrix44D matrix)
    {
        var cardanFrame = matrix.ToCardanFrame();
        var eulerTjs = new EulerFrameTjs(cardanFrame.Offset.X, cardanFrame.Offset.Y, cardanFrame.Offset.Z, cardanFrame.AlphaAngleAxisX, cardanFrame.BetaAngleAxisY, cardanFrame.GammaAngleAxisZ);
        return eulerTjs;
    }
}
