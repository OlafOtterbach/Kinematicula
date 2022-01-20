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
    }
}