using Kinematicula.Mathematics;

namespace Kinematicula.Graphics
{
    public class World : Body
    {
        protected override Matrix44D OnFrameChange(Matrix44D currentFrame, Matrix44D newFrame)
        {
            return currentFrame;
        }
    }
}