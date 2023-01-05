using Kinematicula.Graphics;
using ThreeJsViewerApi.Model;

namespace ThreeJsViewerApi.Converters;

public static class ConverterCameraToCameraTjs
{
    public static CameraTjs ToCameraTjs(this Camera camera)
    {
        var cameraTjs = new CameraTjs(
            camera.Name,
            camera.Frame.ToFrameTjs());

        return cameraTjs;
    }
}
