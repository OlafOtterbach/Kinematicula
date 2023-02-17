namespace ThreeJsViewerApi.EventModel;

using ThreeJsViewerApi.GraphicsModel;

public class MoveEventTjs
{
    public string EventSource { get; set; }
    public Guid BodyId { get; set; }
    public PositionTjs BodyIntersection { get; set; }
    public double StartX { get; set; }
    public double StartY { get; set; }
    public double EndX { get; set; }
    public double EndY { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
    public Guid CameraId { get; set; }
}
