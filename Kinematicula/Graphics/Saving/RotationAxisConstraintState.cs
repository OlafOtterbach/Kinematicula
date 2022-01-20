namespace Kinematicula.Graphics.Saving
{
    public class RotationAxisConstraintState : State<RotationAxisConstraint>
    {
        private double _angle;

        public RotationAxisConstraintState(RotationAxisConstraint axisConstraint) : base(axisConstraint)
        {
            _angle = axisConstraint.Angle;
        }

        public override void Restore()
        {
            Target.Angle = _angle;
        }
    }
}
