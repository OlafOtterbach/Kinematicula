using Kinematicula.Graphics;

namespace Kinematicula.Kinematics.DirectForwardSolving
{
    public class FixedSolver : DirectForwardSolver<FixedConstraint>
    {
        protected override void SolveFirstToSecond(FixedConstraint fixedConstraint)
        {
            Solve(fixedConstraint.First, fixedConstraint.Second);
        }

        protected override void SolveSecondToFirst(FixedConstraint fixedConstraint)
        {
            Solve(fixedConstraint.Second, fixedConstraint.First);
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
}