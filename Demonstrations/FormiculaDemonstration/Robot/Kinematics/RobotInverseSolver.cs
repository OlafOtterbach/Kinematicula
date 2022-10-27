namespace FormiculaDemonstration.Robot.Kinematics;

using FormiculaDemonstration.Robot.Graphics;
using Kinematicula.Kinematics;
using Kinematicula.Kinematics.DirectInverseSolving;

public class RobotInverseSolver : DirectInverseSolver<RobotConstraint>
{
    private readonly IDirectForwardConstraintSolver _forwardSolver;

    public RobotInverseSolver(IDirectForwardConstraintSolver forwardSolver)
    {
        _forwardSolver = forwardSolver;
    }

    protected override bool SolveFirstToSecond(RobotConstraint robotConstraint)
    {
        var first = robotConstraint.First;
        var second = robotConstraint.Second;
        var shouldFrame = first.Body.Frame * first.ConnectionFrame * second.ConnectionFrame.Inverse();
        second.Body.Frame = shouldFrame;
        var result = second.Body.Frame == shouldFrame;

        if (!result)
        {
            shouldFrame = second.Body.Frame * second.ConnectionFrame * first.ConnectionFrame.Inverse();
            first.Body.Frame = shouldFrame;
        }

        return result;
    }

    protected override bool SolveSecondToFirst(RobotConstraint robotConstraint)
    {
        var first = robotConstraint.First;
        var second = robotConstraint.Second;
        if (second.Body.Parent is Robot robot)
        {
            var alpha1 = robot.GetAxisAngle(1);
            var alpha2 = robot.GetAxisAngle(2);
            var alpha3 = robot.GetAxisAngle(3);
            var alpha4 = robot.GetAxisAngle(4);
            var alpha5 = robot.GetAxisAngle(5);
            var alpha6 = robot.GetAxisAngle(6);
            var shouldFrame = second.Body.Frame * second.ConnectionFrame;
            var result = RobotService.GetAxesForTransformation(shouldFrame, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);
            if (result)
            {
                robot.SetAxisAngle(1, alpha1);
                robot.SetAxisAngle(2, alpha2);
                robot.SetAxisAngle(3, alpha3);
                robot.SetAxisAngle(4, alpha4);
                robot.SetAxisAngle(5, alpha5);
                robot.SetAxisAngle(6, alpha6);
                _forwardSolver.SolveLocal(robot);
                result = (second.Body.Frame * second.ConnectionFrame).Equals(shouldFrame, 0.0001);
            }

            return result;
        }
        else
        {
            return false;
        }
    }
}