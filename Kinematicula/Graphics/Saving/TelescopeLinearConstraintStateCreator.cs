namespace Kinematicula.Graphics.Saving
{
    public class TelescopeLinearConstraintStateCreator : StateCreator<TelescopeLinearAxisConstraint>
    {
        protected override IState Create(TelescopeLinearAxisConstraint telescopeAxisConstraint) => new TelescopeLinearConstraintState(telescopeAxisConstraint);
    }
}
