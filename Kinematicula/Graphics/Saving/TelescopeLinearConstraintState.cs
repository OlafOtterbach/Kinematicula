namespace Kinematicula.Graphics.Saving
{
    public class TelescopeLinearConstraintState : State<TelescopeLinearAxisConstraint>
    {
        private double _telescopeLinearPosition;

        public TelescopeLinearConstraintState(TelescopeLinearAxisConstraint telescopeLinearConstraint)
            : base(telescopeLinearConstraint)
        {
            _telescopeLinearPosition = telescopeLinearConstraint.LinearPosition;
        }

        public override void Restore()
        {
            Target.LinearPosition = _telescopeLinearPosition;
        }
    }
}