namespace ThreeJsViewerApi.EventModel;

using ThreeJsViewerApi.GraphicsModel;

public class ZoomEventTjs
{
    public double Delta { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
    public CameraTjs Camera { get; set; }
}
