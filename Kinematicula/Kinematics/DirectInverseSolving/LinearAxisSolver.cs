using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;
using Kinematicula.Mathematics.Extensions;
using Kinematicula.Mathematics;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
    public class LinearAxisSolver : DirectInverseSolver<LinearAxisConstraint>
    {
        protected override bool SolveFirstToSecond(LinearAxisConstraint constraint, Snapshot snapShot)
        {
            var rail = constraint.First.Body;
            var wagon = constraint.Second.Body;

            var formerRailFrame = snapShot.GetFrameFor(rail);
            var formerWagonFrame = snapShot.GetFrameFor(wagon);

            var relativeFrame = formerRailFrame.Inverse() * formerWagonFrame;

            var newWagonFrame = rail.Frame * relativeFrame;

            wagon.Frame = newWagonFrame;
            return true;
        }

        protected override bool SolveSecondToFirst(LinearAxisConstraint constraint, Snapshot snapShot)
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
}