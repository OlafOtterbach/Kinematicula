using Kinematicula.Graphics.Saving;
using Kinematicula.Kinematics.DirectInverseSolving;

namespace SpiritAutomataDemonstration.Constraints
{
    public class RotationToLinearInverseSolver : DirectInverseSolver<RotationToLinearConstraint>
    {
        protected override bool SolveFirstToSecond(RotationToLinearConstraint constraint, Snapshot snapShot)
        {
            throw new System.NotImplementedException();
        }

        protected override bool SolveSecondToFirst(RotationToLinearConstraint constraint, Snapshot snapShot)
        {
            throw new System.NotImplementedException();
        }
    }
}
