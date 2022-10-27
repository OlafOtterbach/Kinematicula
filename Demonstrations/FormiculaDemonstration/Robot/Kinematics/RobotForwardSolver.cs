namespace FormiculaDemonstration.Robot.Kinematics;

using FormiculaDemonstration.Robot.Graphics;
using Kinematicula.Graphics;
using Kinematicula.Kinematics.DirectForwardSolving;

public class RobotForwardSolver : DirectForwardSolver<RobotConstraint>
{
    protected override void SolveFirstToSecond(RobotConstraint robotConstraint)
    {
        Solve(robotConstraint.First, robotConstraint.Second);
    }

    protected override void SolveSecondToFirst(RobotConstraint robotConstraint)
    {
        Solve(robotConstraint.Second, robotConstraint.First);
    }

    private void Solve(Anchor first, Anchor second)
    {
        var firstMat = first.Body.Frame;
        var firstAnchorMat = first.ConnectionFrame;
        var secondAnchorMat = second.ConnectionFrame;
        var targetBody = second.Body;

        targetBody.Frame = firstMat * firstAnchorMat * (secondAnchorMat.Inverse());
    }
}