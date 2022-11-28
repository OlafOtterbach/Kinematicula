namespace Kinematicula.Kinematics.DirectInverseSolving;

using Kinematicula.Graphics;
using Kinematicula.Mathematics.Extensions;
using Kinematicula.Mathematics;

public class TelescopeLinearAxisSolver : DirectInverseSolver<TelescopeLinearAxisConstraint>
{
    protected override bool SolveFirstToSecond(TelescopeLinearAxisConstraint constraint)
    {
        var rail = constraint.First.Body;
        var wagon = constraint.Second.Body;
        var railFrame = rail.Frame * constraint.First.ConnectionFrame;
        var wagonFrame = wagon.Frame * constraint.Second.ConnectionFrame;
        var wagonOnRailFrame = railFrame.Inverse() * wagonFrame;
        var wagonPosition = wagonOnRailFrame.Offset.X;

        var isValid =    wagonPosition >= constraint.Minimum
                      && wagonPosition <= constraint.Maximum
                      && wagonOnRailFrame.Offset.Y.EqualsTo(0.0)
                      && wagonOnRailFrame.Offset.Z.EqualsTo(0.0)
                      && wagonFrame.Ex == railFrame.Ex
                      && wagonFrame.Ey == railFrame.Ey;

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

    protected override bool SolveSecondToFirst(TelescopeLinearAxisConstraint constraint)
    {
        var rail = constraint.First.Body;
        var wagon = constraint.Second.Body;
        var railFrame = rail.Frame * constraint.First.ConnectionFrame;
        var wagonFrame = wagon.Frame * constraint.Second.ConnectionFrame;
        var wagonOnRailFrame = railFrame.Inverse() * wagonFrame;
        var wagonPosition = wagonOnRailFrame.Offset.X;

        var isValid = wagonOnRailFrame.Offset.X >= constraint.Minimum
                   && wagonOnRailFrame.Offset.X <= constraint.Maximum
                   && wagonOnRailFrame.Offset.Y.EqualsTo(0.0)
                   && wagonOnRailFrame.Offset.Z.EqualsTo(0.0)
                   && wagonFrame.Ex == railFrame.Ex
                   && wagonFrame.Ey == railFrame.Ey;

        if (!isValid)
        {
            var ex = wagonFrame.Ex;
            var ey = wagonFrame.Ey;
            var ez = wagonFrame.Ez;
            if (wagonPosition < constraint.Minimum) wagonPosition = constraint.Minimum;
            if (wagonPosition > constraint.Maximum) wagonPosition = constraint.Maximum;
            var railOffset = wagonFrame.Offset - wagonPosition * railFrame.Ex;
            var correctedRailFrame = Matrix44D.CreateCoordinateSystem(railOffset, ex, ey, ez);
            var newRailFrame = correctedRailFrame * constraint.First.ConnectionFrame.Inverse();

            rail.Frame = newRailFrame;
        }

        constraint.LinearPosition = wagonPosition;

        return isValid;
    }
}
