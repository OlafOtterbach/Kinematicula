using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
    public class RotationAxisSolver : DirectInverseSolver<RotationAxisConstraint>
    {
        protected override bool SolveFirstToSecond(RotationAxisConstraint constraint, Snapshot snapShot)
        {
            var rail = constraint.First.Body;
            var wagon = constraint.Second.Body;
            var formerthreadFrame = snapShot.GetFrameFor(rail);
            var formerWagonFrame = snapShot.GetFrameFor(wagon);
            var realativeFrame = formerthreadFrame.Inverse() * formerWagonFrame;
            var newWagonFrame = rail.Frame * realativeFrame;
            wagon.Frame = newWagonFrame;
            return true;
        }

        protected override bool SolveSecondToFirst(RotationAxisConstraint constraint, Snapshot snapShot)
        {
            var thread = constraint.First.Body;
            var screw = constraint.Second.Body;
            var screwFrame = screw.Frame;
            var threadConnectionFrame = thread.Frame * constraint.First.ConnectionFrame;
            var screwConnectionFrame = screw.Frame * constraint.Second.ConnectionFrame;
            var formerThreadFrame = snapShot.GetFrameFor(thread);
            var formerScrewFrame = snapShot.GetFrameFor(screw);

            // IF position or orientation is not correct THEN
            if (!threadConnectionFrame.Offset.EqualsTo(screwConnectionFrame.Offset, 3) || !threadConnectionFrame.Ez.EqualsTo(screwConnectionFrame.Ez, 3))
            {
                // Setting thread to screw by former angle
                var relativeFrame = formerScrewFrame.Inverse() * formerThreadFrame;
                var newThreadFrame = screwFrame * relativeFrame;
                thread.Frame = newThreadFrame;
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
                }
                if (angle > constraint.MaximumAngle)
                {
                    angle = constraint.MaximumAngle;
                }

                var rotation = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), angle);
                screw.Frame = threadConnectionFrame * rotation * constraint.Second.ConnectionFrame.Inverse();
                constraint.Angle = angle;
            }

            return true;
        }
    }
}