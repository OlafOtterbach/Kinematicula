namespace Kinematicula.Graphics.Saving
{
    public class BodyStateCreator : StateCreator<Body>
    {
        protected override IState Create(Body body) => new BodyState(body);
    }
}