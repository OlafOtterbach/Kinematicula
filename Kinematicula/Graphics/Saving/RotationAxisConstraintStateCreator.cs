using Kinematicula.Graphics;

namespace Kinematicula.Graphics.Saving
{
    public class RotationAxisConstraintStateCreator : StateCreator<RotationAxisConstraint>
    {
        protected override IState Create(RotationAxisConstraint axisConstraint) => new RotationAxisConstraintState(axisConstraint);
    }
}