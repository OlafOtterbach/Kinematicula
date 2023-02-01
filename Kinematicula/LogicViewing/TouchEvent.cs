namespace Kinematicula.LogicViewing;

using Kinematicula.Mathematics;

public class TouchEvent
{
    public bool IsBodyTouched { get; set; }

    public Guid BodyId { get; set; }

    public Position3D TouchPosition { get; set; }

    public Guid CameraId { get; set; }

    public int CanvasWidth { get; set; }

    public int CanvasHeight { get; set; }
}