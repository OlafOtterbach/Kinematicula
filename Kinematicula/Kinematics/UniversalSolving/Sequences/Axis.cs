namespace Kinematicula.Kinematics.UniversalSolving.Sequences
{
    public struct Axis
    {
        public Axis(AxisType axisType, double value)
        {
            AxisType = axisType;
            Value = value;
        }

        public AxisType AxisType { get; }
        public double Value { get; }
    }
}
