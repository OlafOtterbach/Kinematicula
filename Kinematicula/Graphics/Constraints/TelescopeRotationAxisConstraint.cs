﻿using Kinematicula.Graphics.Memento;

namespace Kinematicula.Graphics
{
    public class TelescopeRotationAxisConstraint : Constraint
    {
        public TelescopeRotationAxisConstraint(Anchor first, Anchor second)
            : this(first, second, 0.0, double.NegativeInfinity, double.PositiveInfinity)
        {
        }

        public TelescopeRotationAxisConstraint(Anchor first, Anchor second, double minimum)
            : this(first, second, minimum >= 0.0 ? 0.0 : minimum, minimum, double.PositiveInfinity)
        {
        }

        public TelescopeRotationAxisConstraint(Anchor first, Anchor second, double minimum, double maximum)
            : this(first, second, (minimum + maximum) / 2.0, minimum, maximum)
        {
        }

        public TelescopeRotationAxisConstraint(Anchor first, Anchor second, double angle, double minimum, double maximum)
            : base(first, second)
        {
            MinimumAngle = minimum;
            MaximumAngle = maximum;
            Angle = angle;
        }

        public double Angle
        {
            get
            {
                return _angle;
            }

            set
            {
                _angle = value;
                _angle = _angle < MinimumAngle ? MinimumAngle : _angle;
                _angle = _angle > MaximumAngle ? MaximumAngle : _angle;
            }
        }

        public double MinimumAngle { get; set; }

        public double MaximumAngle { get; set; }


        private double _angle;

        public override IMemento GetMemento()
        {
            return new TelescopeRotationAxisConstraintMemento(this);
        }
    }




    public class TelescopeRotationAxisConstraintMemento : IMemento
    {
        private readonly TelescopeRotationAxisConstraint _constraint;
        private readonly double _angle;
        private readonly double _minimumAngle;
        private readonly double _maximumAngle;

        public TelescopeRotationAxisConstraintMemento(TelescopeRotationAxisConstraint constraint)
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