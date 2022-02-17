using Kinematicula.Kinematics.DirectForwardSolving;

namespace SpiritAutomataDemonstration.Constraints
{
    public class RotationToLinearForwardSolver : DirectForwardSolver<RotationToLinearConstraint>
    {
        protected override void SolveFirstToSecond(RotationToLinearConstraint constraint)
        {
        }

        protected override void SolveSecondToFirst(RotationToLinearConstraint constraint)
        {
        }
    }
}
