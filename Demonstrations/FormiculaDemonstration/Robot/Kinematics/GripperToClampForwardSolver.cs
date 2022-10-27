namespace FormiculaDemonstration.Robot.Kinematics;

using Graphics;
using Kinematicula.Graphics;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Mathematics;

public class GripperToClampForwardSolver : DirectForwardSolver<GripperToClampConstraint>
{
    protected override void SolveFirstToSecond(GripperToClampConstraint constraint)
    {
        Solve(constraint.First, constraint.Second, constraint.LinearPosition);
    }

    protected override void SolveSecondToFirst(GripperToClampConstraint constraint)
    {
        Solve(constraint.Second, constraint.First, -constraint.LinearPosition);
    }

    private static void Solve(Anchor first, Anchor second, double translation)
    {
        var firstMat = first.Body.Frame;
        var firstAnchorMat = first.ConnectionFrame;
        var secondAnchorMat = second.ConnectionFrame;

        var linMat = Matrix44D.CreateTranslation(new Vector3D(translation, 0.0, 0.0));

        var secondBody = second.Body;

        secondBody.Frame = firstMat * firstAnchorMat * linMat * secondAnchorMat.Inverse();
    }
}