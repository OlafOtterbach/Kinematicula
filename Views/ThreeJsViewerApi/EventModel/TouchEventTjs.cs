namespace ThreeJsViewerApi.EventModel;

using ThreeJsViewerApi.GraphicsModel;

public class TouchEventTjs
{
    public bool IsBodyTouched { get; set; }
    public Guid BodyId { get; set; }
    public PositionTjs TouchPosition { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
    public CameraTjs Camera { get; set; }
}
