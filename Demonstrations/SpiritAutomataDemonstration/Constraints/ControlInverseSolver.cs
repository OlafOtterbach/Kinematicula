using System.Linq;
using Kinematicula.Graphics;
using Kinematicula.Kinematics.DirectInverseSolving;

namespace SpiritAutomataDemonstration.Constraints
{
    public class ControlInverseSolver : DirectInverseSolver<ControlConstraint>
    {
        protected override bool SolveFirstToSecond(ControlConstraint constraint)
        {
            var shifter = constraint.First.Body;
            var linearConstraint = shifter.Constraints.OfType<LinearAxisConstraint>().Where(c => c.Second.Body == shifter).First();
            var pos = (linearConstraint.First.Body.Frame * linearConstraint.First.ConnectionFrame).Inverse() * (shifter.Frame )

            return false;
        }

        protected override bool SolveSecondToFirst(ControlConstraint constraint)
        {
            return false;
        }

    }
}
