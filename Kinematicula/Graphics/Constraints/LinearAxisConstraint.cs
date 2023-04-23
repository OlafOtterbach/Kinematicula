using Kinematicula.Graphics.Memento;

namespace Kinematicula.Graphics
{
    public class LinearAxisConstraint : Constraint
    {
        private double _position;

        public LinearAxisConstraint(Anchor first, Anchor second)
            : this(first, second, 0.0, double.NegativeInfinity, double.PositiveInfinity)
        {
        }

        public LinearAxisConstraint(Anchor first, Anchor second, double minimum)
            : this(first, second, minimum >= 0.0 ? 0.0 : minimum, minimum, double.PositiveInfinity)
        {
        }

        public LinearAxisConstraint(Anchor first, Anchor second, double minimum, double maximum)
            : this(first, second, minimum >= 0.0 ? 0.0 : minimum, minimum, maximum)
        {
        }

        public LinearAxisConstraint(Anchor first, Anchor second, double position, double minimum, double maximum)
            : base(first, second)
        {
            Minimum = minimum;
            Maximum = maximum;
            LinearPosition = position;
        }

        public double LinearPosition
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
                _position = _position < Minimum ? Minimum : _position;
                _position = _position > Maximum ? Maximum : _position;
            }
        }

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public override IMemento GetMemento()
        {
            return new LinearAxisConstraintMemento(this);
        }
    }



    public class LinearAxisConstraintMemento : IMemento
    {
        private readonly LinearAxisConstraint _constraint;
        private readonly double _linearPosition;
        private readonly double _minimum;
        private readonly double _maximum;

        public LinearAxisConstraintMemento(LinearAxisConstraint constraint)
        {
            _constraint = constraint;
            _linearPosition = constraint.LinearPosition;
            _minimum = constraint.Minimum;
            _maximum = constraint.Maximum;
        }

        public void Restore()
        {
            _constraint.Minimum = _minimum;
            _constraint.Maximum = _maximum;
            _constraint.LinearPosition = _linearPosition;
        }
    }
}