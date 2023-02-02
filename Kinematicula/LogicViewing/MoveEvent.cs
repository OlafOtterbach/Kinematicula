namespace Kinematicula.LogicViewing;

using Kinematicula.Mathematics;

public class MoveEvent
{
    public string EventSource { get; set; }
    public Guid SelectedBodyId { get; set; }
    public Position3D BodyTouchPosition { get; set; }

    public double StartMoveX { get; set; }
    public double StartMoveY { get; set; }

    public double EndMoveX { get; set; }
    public double EndMoveY { get; set; }

    public Guid CameraId { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
}
