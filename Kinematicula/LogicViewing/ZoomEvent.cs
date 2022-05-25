namespace Kinematicula.LogicViewing
{
    public class ZoomEvent
    {
        public double Delta { get; set; }
        public CameraInfo Camera { get; set; }
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
    }
}
