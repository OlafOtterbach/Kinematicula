namespace Kinematicula.Kinematics.DirectInverseSolving;

using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;

public class RotationAxisSolver : DirectInverseSolver<RotationAxisConstraint>
{
    protected override bool SolveFirstToSecond(RotationAxisConstraint constraint)
    {
        var thread = constraint.First.Body;
        var threadMat = thread.Frame;
        var threadAnchorMat = constraint.First.ConnectionFrame;

        var screw = constraint.Second.Body;
        var screwMat = screw.Frame;
        var screwAnchorMat = constraint.Second.ConnectionFrame;

        var rotMat = Matrix44D.CreateRotation(new Vector3D(0.0, 0.0, 1.0), constraint.Angle);
        screw.Frame = threadMat * threadAnchorMat * rotMat * (screwAnchorMat.Inverse());
        return true;
    }

    protected override bool SolveSecondToFirst(RotationAxisConstraint constraint)
    {
        var isValid = true;
        var thread = constraint.First.Body;
        var screw = constraint.Second.Body;
        var threadConnectionFrame = thread.Frame * constraint.First.ConnectionFrame;
        var screwConnectionFrame = screw.Frame * constraint.Second.ConnectionFrame;

        // IF position or orientation is not correct THEN
        if (!threadConnectionFrame.Offset.EqualsTo(screwConnectionFrame.Offset, 3) || !threadConnectionFrame.Ez.EqualsTo(screwConnectionFrame.Ez, 3))
        {
            // Setting thread to screw by former angle
            var screwMat = constraint.First.Body.Frame;
            var screwAnchorMat = constraint.First.ConnectionFrame;
            var threadAnchorMat = constraint.Second.ConnectionFrame;
            var rotMat = Matrix44D.CreateRotation(new Vector3D(0.0, 0.0, 1.0), constraint.Angle);
            thread.Frame = screwMat * screwAnchorMat * rotMat * (threadAnchorMat.Inverse());
            isValid = false;
        } // ELSE
        else
        {
            var toAngle = threadConnectionFrame.Ex.CounterClockwiseAngleWith(screwConnectionFrame.Ex, threadConnectionFrame.Ez);
            var fromAngle = constraint.Angle;
            var dist = toAngle.DistantAngleTo(fromAngle.Modulo2Pi());
            var alpha = fromAngle + dist;
            var beta = fromAngle - dist;
            var angle = (alpha.Modulo2Pi().EqualsTo(toAngle)) ? alpha : beta;
            if (angle < constraint.MinimumAngle)
            {
                angle = constraint.MinimumAngle;
                isValid = false;
            }
            if (angle > constraint.MaximumAngle)
            {
                angle = constraint.MaximumAngle;
                isValid = false;
            }

            var rotation = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), angle);
            screw.Frame = threadConnectionFrame * rotation * constraint.Second.ConnectionFrame.Inverse();
            constraint.Angle = angle;
        }

        return isValid;
    }
}