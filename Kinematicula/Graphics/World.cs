namespace Kinematicula.Graphics;

using Kinematicula.Mathematics;

public class World : Body
{
    public override Matrix44D Frame
    {
        get
        {
            return Matrix44D.Identity;
        }
        set
        {

        }
    }
}