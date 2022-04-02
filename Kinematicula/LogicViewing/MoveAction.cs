﻿using Kinematicula.Graphics;
using Kinematicula.Mathematics;

namespace Kinematicula.LogicViewing
{
    public class MoveAction
    {
        public string EventSource { get; set; }
        public Body Body { get; set; }
        public Position3D BodyTouchPosition { get; set; }
        public double StartMoveX { get; set; }
        public double StartMoveY { get; set; }
        public double EndMoveX { get; set; }
        public double EndMoveY { get; set; }
        public CameraInfo Camera { get; set; }
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
    }
}
