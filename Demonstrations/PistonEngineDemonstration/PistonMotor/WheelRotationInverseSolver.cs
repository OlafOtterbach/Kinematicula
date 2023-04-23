namespace PistonEngineDemonstration.PistonMotor;

using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Kinematics.DirectForwardSolving;

public class WheelRotationInverseSolver : DirectInverseSolver<WheelRotationConstraint>
{
    private Kinematicula.Kinematics.DirectInverseSolving.RotationAxisSolver _rotationSolver;
    private DirectForwardConstraintSolver _forwardSolver;

    public WheelRotationInverseSolver()
    {
        _rotationSolver = new Kinematicula.Kinematics.DirectInverseSolving.RotationAxisSolver();
        _forwardSolver = new DirectForwardConstraintSolver();
    }

    protected override bool SolveFirstToSecond(WheelRotationConstraint constraint)
        => _rotationSolver.Solve(constraint, constraint.First.Body);

    protected override bool SolveSecondToFirst(WheelRotationConstraint constraint)
    {
        if (_rotationSolver.Solve(constraint, constraint.Second.Body))
        {
            var motor = constraint.Second.Body.Parent as Motor;
            var (shaftAngle, pistonAlpha, pistonPosition)
                = MotorService.GetAxesForWheelAngle(constraint.Angle, 100, 300);
            motor.SetAxes(constraint.Angle, shaftAngle, pistonAlpha, pistonPosition);
            _forwardSolver.SolveLocal(motor);
        }

        return true;
    }
}