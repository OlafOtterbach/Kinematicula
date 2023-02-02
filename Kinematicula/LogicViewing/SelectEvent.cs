﻿namespace Kinematicula.LogicViewing;

public class SelectEvent
{
    public double selectPositionX { get; set; }
    public double selectPositionY { get; set; }
    public Guid CameraId { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
}
