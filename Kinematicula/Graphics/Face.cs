namespace Kinematicula.Graphics;

public class Face
{
    public Triangle[] Triangles { get; set; }

    public bool HasBorder { get; set; }

    public bool HasFacets { get; set; }

#nullable enable
    public Color? Color { get; set; }
#nullable disable
}