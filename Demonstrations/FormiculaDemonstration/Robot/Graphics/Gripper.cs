namespace FormiculaDemonstration.Robot.Graphics;

using Kinematicula.Graphics;

public class Gripper : Body
{
    private double _openingWidth;

    public Gripper(double maximum) : this(0.0, maximum)
    {
    }

    public Gripper(double openingWidth, double maximum)
    {
        Minimum = 0.0;
        Maximum = maximum;
        OpeningWidth = openingWidth;
    }

    public double OpeningWidth
    {
        get
        {
            return _openingWidth;
        }

        set
        {
            _openingWidth = value;
            _openingWidth = _openingWidth < Minimum ? Minimum : _openingWidth;
            _openingWidth = _openingWidth > Maximum ? Maximum : _openingWidth;
        }
    }

    public double Minimum { get; }

    public double Maximum { get; }
}
