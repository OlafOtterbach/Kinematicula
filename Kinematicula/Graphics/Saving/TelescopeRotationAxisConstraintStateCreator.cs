
    namespace Kinematicula.Graphics.Saving
{
    public class TelescopeRotationAxisConstraintStateCreator : StateCreator<TelescopeRotationAxisConstraint>
    {
        protected override IState Create(TelescopeRotationAxisConstraint telescopeAxisConstraint) => new TelescopeRotationAxisConstraintState(telescopeAxisConstraint);
    }
}