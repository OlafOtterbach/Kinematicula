namespace Kinematicula.LogicViewing;

public class ZoomEvent
{
    public double Delta { get; set; }
    public Guid CameraId { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
}
