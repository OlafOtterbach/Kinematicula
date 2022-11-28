namespace Kinematicula.Kinematics.DirectInverseSolving;

using Kinematicula.Graphics;
using Kinematicula.Mathematics.Extensions;
using Kinematicula.Mathematics;

public class LinearAxisSolver : DirectInverseSolver<LinearAxisConstraint>
{
    protected override bool SolveFirstToSecond(LinearAxisConstraint constraint)
    {
        // move second relative to linearposition to first body
        var railAnchor = constraint.First;
        var wagonAnchor = constraint.Second;
        var railMat = railAnchor.Body.Frame;
        var railAnchorMat = railAnchor.ConnectionFrame;
        var waggonAnchorMat = wagonAnchor.ConnectionFrame;
        var linMat = Matrix44D.CreateTranslation(new Vector3D(constraint.LinearPosition, 0.0, 0.0));
        var wagonFrame = railMat * railAnchorMat * linMat * (waggonAnchorMat.Inverse());
        wagonAnchor.Body.Frame = wagonFrame;

        return true;
    }

    protected override bool SolveSecondToFirst(LinearAxisConstraint constraint)
    {
        var rail = constraint.First.Body;
        var wagon = constraint.Second.Body;
        var railFrame = rail.Frame * constraint.First.ConnectionFrame;
        var wagonFrame = wagon.Frame * constraint.Second.ConnectionFrame;

        var wagonOnRailFrame = railFrame.Inverse() * wagonFrame;
        var wagonPosition = wagonOnRailFrame.Offset.X;
        var isValid =    wagonPosition >= constraint.Minimum
                      && wagonPosition <= constraint.Maximum
                      && wagonOnRailFrame.Offset.Y.EqualsTo(0.0, 0.001)
                      && wagonOnRailFrame.Offset.Z.EqualsTo(0.0, 0.001)
                      && wagonFrame.Ex.EqualsTo(railFrame.Ex, 3)
                      && wagonFrame.Ey.EqualsTo(railFrame.Ey, 3);

        if (!isValid)
        {
            var ex = railFrame.Ex;
            var ey = railFrame.Ey;
            var ez = railFrame.Ez;
            if (wagonPosition < constraint.Minimum) wagonPosition = constraint.Minimum;
            if (wagonPosition > constraint.Maximum) wagonPosition = constraint.Maximum;
            var offset = railFrame.Offset + wagonPosition * railFrame.Ex;
            var correctedWagonFrame = Matrix44D.CreateCoordinateSystem(offset, ex, ey, ez);
            var newWagonFrame = correctedWagonFrame * constraint.Second.ConnectionFrame.Inverse();

            wagon.Frame = newWagonFrame;
        }

        constraint.LinearPosition = wagonPosition;

        return isValid;
    }
}