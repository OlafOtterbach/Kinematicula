namespace RobotLib.Graphics;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Memento;

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

    public override IMemento GetMemento()
    {
        return new GripperToClampConstraintMemento(this);
    }
}



public class GripperToClampConstraintMemento : IMemento
{
    private readonly GripperToClampConstraint _constraint;
    private readonly double _linearPosition;

    public GripperToClampConstraintMemento(GripperToClampConstraint constraint)
    {
        _constraint = constraint;
        _linearPosition = constraint.LinearPosition;
    }

    public void Restore()
    {
        _constraint.LinearPosition = _linearPosition;
    }
}