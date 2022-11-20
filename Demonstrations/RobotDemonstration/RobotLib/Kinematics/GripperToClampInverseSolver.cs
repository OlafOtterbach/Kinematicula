namespace RobotLib.Kinematics;

using Graphics;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;

public class GripperToClampInverseSolver : DirectInverseSolver<GripperToClampConstraint>
{
    private readonly LinearAxisSolver _linearSolver;

    public GripperToClampInverseSolver()
    {
        _linearSolver = new LinearAxisSolver();
    }

    protected override bool IsConstraintValid(GripperToClampConstraint constraint) => false;

    protected override bool SolveFirstToSecond(GripperToClampConstraint constraint)
    {
        var gripper = constraint.First.Body;

        var result = _linearSolver.Solve(constraint, gripper);

        return result;
    }

    protected override bool SolveSecondToFirst(GripperToClampConstraint constraint)
    {
        var gripper = constraint.First.Body;
        var clamp = constraint.Second.Body;
        var gripperFrame = gripper.Frame * constraint.First.ConnectionFrame;
        var clampFrame = clamp.Frame * constraint.Second.ConnectionFrame;

        var clampOnGripperFrame = gripperFrame.Inverse() * clampFrame;
        var clampPosition = clampOnGripperFrame.Offset.X;
        var isValid = clampPosition >= constraint.Minimum
                      && clampPosition <= constraint.Maximum
                      && clampOnGripperFrame.Offset.Y.EqualsTo(0.0, 0.001)
                      && clampOnGripperFrame.Offset.Z.EqualsTo(0.0, 0.001)
                      && clampFrame.Ex.EqualsTo(gripperFrame.Ex, 3)
                      && clampFrame.Ey.EqualsTo(gripperFrame.Ey, 3);

        if (!isValid)
        {
            var ex = gripperFrame.Ex;
            var ey = gripperFrame.Ey;
            var ez = gripperFrame.Ez;
            if (clampPosition < constraint.Minimum) clampPosition = constraint.Minimum;
            if (clampPosition > constraint.Maximum) clampPosition = constraint.Maximum;
            var offset = gripperFrame.Offset + clampPosition * gripperFrame.Ex;
            var correctedWagonFrame = Matrix44D.CreateCoordinateSystem(offset, ex, ey, ez);
            var newWagonFrame = correctedWagonFrame * constraint.Second.ConnectionFrame.Inverse();

            clamp.Frame = newWagonFrame;
        }

        constraint.LinearPosition = clampPosition;

        return isValid;
    }
}