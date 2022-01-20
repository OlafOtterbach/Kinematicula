namespace Kinematicula.Graphics.Saving
{
    public class FixedConstraintStateCreator : StateCreator<FixedConstraint>
    {
        protected override IState Create(FixedConstraint fixedConstraint) => new FixedConstraintState(fixedConstraint);
    }
}
