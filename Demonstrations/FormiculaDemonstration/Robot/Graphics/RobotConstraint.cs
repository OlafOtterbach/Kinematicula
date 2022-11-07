namespace FormiculaDemonstration.Robot.Graphics;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Memento;

public class RobotConstraint : Constraint
{
    public RobotConstraint(Anchor first, Anchor second) : base(first, second)
    {
    }
    public override IMemento GetMemento()
    {
        return new RobotConstraintMemento();
    }
}



public class RobotConstraintMemento : IMemento
{
    public void Restore() { }
}

