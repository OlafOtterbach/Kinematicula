using Kinematicula.Graphics.Memento;

namespace Kinematicula.Graphics
{
    public class RotationAxisConstraint : Constraint
    {
        public RotationAxisConstraint(Anchor first, Anchor second)
            : this(first, second, 0.0, double.NegativeInfinity, double.PositiveInfinity)
        {
        }

        public RotationAxisConstraint(Anchor first, Anchor second, double minimum)
            : this(first, second, minimum >= 0.0 ? 0.0 : minimum, minimum, double.PositiveInfinity)
        {
        }

        public RotationAxisConstraint(Anchor first, Anchor second, double minimum, double maximum)
            : this(first, second, (minimum + maximum) / 2.0, minimum, maximum)
        {
        }

        public RotationAxisConstraint(Anchor first, Anchor second, double angle, double minimum, double maximum)
            : base(first, second)
        {
            MinimumAngle = minimum;
            MaximumAngle = maximum;
            Angle = angle;
        }

        public double Angle { get; set; }

        public double MinimumAngle { get; set; }

        public double MaximumAngle { get; set; }

        public override IMemento GetMemento()
        {
            return new RotationAxisConstraintMemento(this);
        }
    }




    public class RotationAxisConstraintMemento : IMemento
    {
        private readonly RotationAxisConstraint _constraint;
        private readonly double _angle;
        private readonly double _minimumAngle;
        private readonly double _maximumAngle;

        public RotationAxisConstraintMemento(RotationAxisConstraint constraint)
        {
            _constraint = constraint;
            _angle = constraint.Angle;
            _minimumAngle = constraint.MinimumAngle;
            _maximumAngle = constraint.MaximumAngle;
        }

        public void Restore()
        {
            _constraint.MinimumAngle = _minimumAngle;
            _constraint.MaximumAngle = _maximumAngle;
            _constraint.Angle = _angle;
        }
    }
}