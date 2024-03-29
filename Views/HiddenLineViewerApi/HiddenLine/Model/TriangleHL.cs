﻿namespace HiddenLineViewerApi.HiddenLine.Model;

using Kinematicula.Graphics;
using Kinematicula.Mathematics;

public class TriangleHL
{
    public Vector3D Normal { get; set; }
    public Position3D P1 { get; set; }
    public Position3D P2 { get; set; }
    public Position3D P3 { get; set; }
    public TriangleSpin Spin { get; set; }

    public FaceHL Face { get; set; }

    public Triangle Triangle { get; set; }
}