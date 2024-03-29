﻿namespace Kinematicula.Graphics;

public class Face
{
    public Triangle[] Triangles { get; set; }

    public bool HasBorder { get; set; }

    public bool HasFacets { get; set; }

    public Color Color { get; set; } = new Color(1.0, 1.0, 1.0);
}