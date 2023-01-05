using Kinematicula.Mathematics;
using ThreeJsViewerApi.Model;

namespace ThreeJsViewerApi.Converters;

public static class ConverterMatrix44DToFrameTjs
{
    public static FrameTjs ToFrameTjs(this Matrix44D matrix)
    {
        var frameTjs = new FrameTjs(
            matrix.A11, matrix.A12, matrix.A13, matrix.A14,
            matrix.A21, matrix.A22, matrix.A23, matrix.A24,
            matrix.A31, matrix.A32, matrix.A33, matrix.A34,
            matrix.A41, matrix.A42, matrix.A43, matrix.A44);
        return frameTjs;
    }
}
