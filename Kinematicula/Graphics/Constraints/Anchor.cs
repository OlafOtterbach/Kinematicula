using Kinematicula.Mathematics;

namespace Kinematicula.Graphics
{
    public class Anchor
    {
        public Anchor(Body body, Matrix44D connectionFrame)
        {
            Body = body;
            ConnectionFrame = connectionFrame;
        }

        public Body Body { get; }
        public Matrix44D ConnectionFrame { get; }
    }
}