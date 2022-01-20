namespace Kinematicula.Graphics.Saving
{
    public class TelescopeRotationAxisConstraintState : State<TelescopeRotationAxisConstraint>
    {
        private double _angle;

        public TelescopeRotationAxisConstraintState(TelescopeRotationAxisConstraint telescopeAxisConstraint)
            : base(telescopeAxisConstraint)
        {
            _angle = telescopeAxisConstraint.Angle;
        }

        public override void Restore()
        {
            Target.Angle = _angle;
        }
    }
}