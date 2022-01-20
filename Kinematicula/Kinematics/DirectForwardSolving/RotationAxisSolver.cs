using Kinematicula.Graphics;
using Kinematicula.Mathematics;

namespace Kinematicula.Kinematics.DirectForwardSolving
{
    public class RotationAxisSolver : DirectForwardSolver<RotationAxisConstraint>
    {
        protected override void SolveFirstToSecond(RotationAxisConstraint constraint)
        {
            Solve(constraint.First, constraint.Second, constraint.Angle);
        }

        protected override void SolveSecondToFirst(RotationAxisConstraint constraint)
        {
            Solve(constraint.Second, constraint.First, -constraint.Angle);
        }
		
        private static void Solve(Anchor first, Anchor second, double angle)
        {
			var firstMat = first.Body.Frame;
			var firstAnchorMat = first.ConnectionFrame;
			var secondAnchorMat = second.ConnectionFrame;
			var rotMat = Matrix44D.CreateRotation(new Vector3D(0.0, 0.0, 1.0), angle);

			var secondBody = second.Body;

			secondBody.Frame = firstMat * firstAnchorMat * rotMat * (secondAnchorMat.Inverse());
        }
    }
}