namespace Kinematicula.Graphics.Creators.Creator;

using Kinematicula.Mathematics;

public struct CreatorTriangle
{
    public CreatorFace Parent { get; set; }
    public Vector3D Normal { get; set; }
    public CreatorVertex P1 { get; set; }
    public CreatorVertex P2 { get; set; }
    public CreatorVertex P3 { get; set; }
}