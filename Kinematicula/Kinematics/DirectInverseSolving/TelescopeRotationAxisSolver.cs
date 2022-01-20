using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;
using Kinematicula.Mathematics.Extensions;
using Kinematicula.Mathematics;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
    public class TelescopeRotationAxisSolver : DirectInverseSolver<TelescopeRotationAxisConstraint>
    {
        protected override bool SolveFirstToSecond(TelescopeRotationAxisConstraint constraint, Snapshot snapShot)
        {
            var thread = constraint.First.Body;
            var screw = constraint.Second.Body;
            var screwFrame = screw.Frame;
            var threadConnectionFrame = thread.Frame * constraint.First.ConnectionFrame;
            var screwConnectionFrame = screw.Frame * constraint.Second.ConnectionFrame;
            var formerThreadFrame = snapShot.GetFrameFor(thread);
            var formerScrewFrame = snapShot.GetFrameFor(screw);

            // IF position or orientation is not correct THEN
            if (threadConnectionFrame.Offset != screwConnectionFrame.Offset || threadConnectionFrame.Ez != screwConnectionFrame.Ez)
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

        protected override bool SolveSecondToFirst(TelescopeRotationAxisConstraint constraint, Snapshot snapShot)
        {
            var thread = constraint.First.Body;
            var screw = constraint.Second.Body;
            var screwFrame = screw.Frame;
            var threadConnectionFrame = thread.Frame * constraint.First.ConnectionFrame;
            var screwConnectionFrame = screw.Frame * constraint.Second.ConnectionFrame;
            var formerThreadFrame = snapShot.GetFrameFor(thread);
            var formerScrewFrame = snapShot.GetFrameFor(screw);

            // IF position or orientation is not correct THEN
            if (threadConnectionFrame.Offset != screwConnectionFrame.Offset || threadConnectionFrame.Ez != screwConnectionFrame.Ez)
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

                var rotation = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), -angle);
                thread.Frame = screwConnectionFrame * rotation * constraint.First.ConnectionFrame.Inverse();
                constraint.Angle = angle;
            }

            return true;
        }
    }
}