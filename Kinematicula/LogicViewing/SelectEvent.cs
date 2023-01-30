﻿namespace Kinematicula.LogicViewing;

using Kinematicula.Graphics;

public class SelectEvent
{
    public double selectPositionX { get; set; }
    public double selectPositionY { get; set; }
    public Camera Camera { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
}
