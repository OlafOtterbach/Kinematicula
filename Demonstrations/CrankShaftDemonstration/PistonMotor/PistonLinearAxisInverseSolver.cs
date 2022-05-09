using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;

namespace CrankShaftDemonstration.PistonMotor
{
    public class PistonLinearAxisInverseSolver : DirectInverseSolver<PistonLinearAxisConstraint>
{
        private Kinematicula.Kinematics.DirectInverseSolving.LinearAxisSolver _linearSolver;
        private DirectForwardConstraintSolver _forwardSolver;

        public PistonLinearAxisInverseSolver()
        {
            _linearSolver = new Kinematicula.Kinematics.DirectInverseSolving.LinearAxisSolver();
            _forwardSolver = new DirectForwardConstraintSolver();
        }

        protected override bool SolveFirstToSecond(PistonLinearAxisConstraint constraint)
            => _linearSolver.Solve(constraint, constraint.First.Body);

        protected override bool SolveSecondToFirst(PistonLinearAxisConstraint constraint)
        {
            if (_linearSolver.Solve(constraint, constraint.Second.Body))
            {
                var position = constraint.LinearPosition;
                var motor = constraint.Second.Body.Parent as Motor;
                //var (shaftAlpha, pistonAlpha, pistonPosition)
                //    = MotorService.GetAxesForWheelAngle(constraint.Angle, 100, 300);
                //motor.SetAxes(angle, shaftAlpha, pistonAlpha, pistonPosition);
                _forwardSolver.SolveLocal(motor);
            }

            return true;
        }
    }
}
