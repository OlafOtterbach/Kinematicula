namespace ThreeJsViewerApi.Converters;

using Kinematicula.Graphics;
using ThreeJsViewerApi.Model;

public static class ConverterCameraToCameraTjs
{
    public static CameraTjs ToCameraTjs(this Camera camera)
    {
        var cameraTjs = new CameraTjs(
            camera.Name,
            camera.Frame.ToEulerFrameTjs(),
            camera.Frame.ToFrameTjs());

        return cameraTjs;
    }
}
