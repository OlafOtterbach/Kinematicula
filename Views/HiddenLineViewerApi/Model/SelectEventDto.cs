﻿namespace HiddenLineViewerApi
{
    public class SelectEventDto
    {
        public double selectPositionX { get; set; }
        public double selectPositionY { get; set; }
        public int canvasWidth { get; set; }
        public int canvasHeight { get; set; }
        public CameraDto camera { get; set; }
    }
}
