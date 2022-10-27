namespace RobotDemonstration.Robot.Graphics;

using Kinematicula.Graphics;

public class GripperToClampConstraint : Constraint
{
    public GripperToClampConstraint(Anchor first, Anchor second)
        : this(first, second, (first.Body as Gripper)?.OpeningWidth / 2.0 ?? 0.0)
    { }

    public GripperToClampConstraint(Anchor first, Anchor second, double position)
        : base(first, second)
    {
        LinearPosition = position;
    }

    public double LinearPosition
    {
        get
        {
            return Gripper?.OpeningWidth / 2.0 ?? 0.0;
        }

        set
        {
            if (Gripper != null) Gripper.OpeningWidth = value * 2.0;
        }
    }

    public double Minimum => Gripper?.Minimum / 2.0 ?? 0.0;

    public double Maximum => Gripper?.Maximum / 2.0 ?? 0.0;

    private Gripper Gripper => First.Body as Gripper;
}