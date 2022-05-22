using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;

namespace PistonEngineDemonstration.PistonMotor
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
                var (wheelAngle, shaftAngle, pistonAngle, pistonPosition) = MotorService.GetAxesForPistonPosition(position, 100, 300);

                var counterWheelAngle = (-wheelAngle).Modulo2Pi();
                var currentWheelAngle = motor.GetWheelAxisValue();
                var distance = currentWheelAngle.DistantAngleTo(wheelAngle);
                var counterDistance = currentWheelAngle.DistantAngleTo(counterWheelAngle);
                if(counterDistance < distance)
                {
                    wheelAngle = counterWheelAngle;
                }

                motor.SetAxes(wheelAngle, shaftAngle, pistonAngle, pistonPosition);

                _forwardSolver.SolveLocal(motor);
            }

            return true;
        }
    }
}
