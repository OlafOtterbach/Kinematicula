namespace CrankShaftDemonstration.PistonMotor;

using Kinematicula.Kinematics.DirectInverseSolving;

public class WheelRotationInverseSolver : DirectInverseSolver<WheelRotationConstraint>
{
    RotationAxisSolver _rotationSolver;

    public WheelRotationInverseSolver()
    {
        _rotationSolver = new RotationAxisSolver();
    }

    protected override bool SolveFirstToSecond(WheelRotationConstraint constraint)
        => _rotationSolver.Solve(constraint, constraint.First.Body);

    protected override bool SolveSecondToFirst(WheelRotationConstraint constraint)
    {
        if (_rotationSolver.Solve(constraint, constraint.Second.Body))
        {
            var motor = constraint.Second.Body.Parent as Motor;
            var (shaftAlpha, pistonAlpha) = MotorService.GetAxesForWheelAngle(constraint.Angle, 100, 300);
            motor.SetAxes(constraint.Angle, shaftAlpha, pistonAlpha);
        }

        return true;
    }
}

