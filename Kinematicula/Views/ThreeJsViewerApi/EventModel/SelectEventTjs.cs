namespace Kinematicula.Views.ThreeJsViewerApi.EventModel;

public class SelectEventTjs
{
    public double SelectPositionX { get; set; }
    public double SelectPositionY { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
    public Guid CameraId { get; set; }
}
