﻿namespace HiddenLineViewerApi
{
    public class ZoomEventDto
    {
        public double delta { get; set; }
        public int canvasWidth { get; set; }
        public int canvasHeight { get; set; }
        public CameraDto camera { get; set; }
    }
}
