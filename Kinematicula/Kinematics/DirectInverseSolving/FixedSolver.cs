using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
    public class FixedSolver : DirectInverseSolver<FixedConstraint>
    {
        protected override bool SolveFirstToSecond(FixedConstraint fixedConstraint, Snapshot snapShot)
        {
            return Solve(fixedConstraint.First, fixedConstraint.Second);
        }

        protected override bool SolveSecondToFirst(FixedConstraint fixedConstraint, Snapshot snapShot)
        {
            var result = Solve(fixedConstraint.Second, fixedConstraint.First);
            return result;
        }

        private bool Solve(Anchor first, Anchor second)
        {
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
    }
}