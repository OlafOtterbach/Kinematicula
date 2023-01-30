﻿namespace Kinematicula.LogicViewing;

using Kinematicula.Graphics;

public class ZoomEvent
{
    public double Delta { get; set; }
    public Camera Camera { get; set; }
    public int CanvasWidth { get; set; }
    public int CanvasHeight { get; set; }
}
