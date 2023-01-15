namespace ThreeJsViewerApi.Converters;

using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using ThreeJsViewerApi.Model;

public static class ConverterCameraToCameraTjs
{
    public static CameraTjs ToCameraTjs(this Camera camera)
    {
        var frame = camera.Frame;

        var back = Matrix44D.CreateTranslation(-1.0 * frame.Offset.ToVector3D());
        var trans = Matrix44D.CreateTranslation(frame.Offset.ToVector3D());
        var rotY = Matrix44D.CreateRotation(frame.Ey, -ConstantsMath.HalfPi);
        var rotZ = Matrix44D.CreateRotation(frame.Ez, -ConstantsMath.HalfPi);
        var frameTjs = trans * rotY * rotZ * back * frame;


        var cameraTjs = new CameraTjs(
            camera.Name,
            frameTjs.ToEulerFrameTjs(),
            frameTjs.ToFrameTjs());

        return cameraTjs;
    }
}
