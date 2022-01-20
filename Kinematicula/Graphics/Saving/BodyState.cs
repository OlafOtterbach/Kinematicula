using Kinematicula.Graphics;
using Kinematicula.Mathematics;

namespace Kinematicula.Graphics.Saving
{
    public class BodyState : State<Body>
    {
        public BodyState(Body body) : base(body)
        {
            Frame = body.Frame;
        }

        public Matrix44D Frame { get; }

        public override void Restore()
        {
            Target.Frame = Frame;
        }
    }
}
