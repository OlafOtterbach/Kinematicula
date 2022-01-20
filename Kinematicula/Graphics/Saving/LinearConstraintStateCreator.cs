namespace Kinematicula.Graphics.Saving
{
    public class LinearConstraintStateCreator : StateCreator<LinearAxisConstraint>
    {
        protected override IState Create(LinearAxisConstraint axisConstraint) => new LinearConstraintState(axisConstraint);
    }
}