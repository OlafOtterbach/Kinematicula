namespace Kinematicula.Graphics.Saving
{
    public class LinearConstraintState : State<LinearAxisConstraint>
    {
        private double _linearPosition;

        public LinearConstraintState(LinearAxisConstraint linearConstraint)
            : base(linearConstraint)
        {
            _linearPosition = linearConstraint.LinearPosition;
        }

        public override void Restore()
        {
            Target.LinearPosition = _linearPosition;
        }
    }
}