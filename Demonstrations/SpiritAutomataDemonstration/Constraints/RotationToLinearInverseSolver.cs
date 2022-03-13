using System.Linq;
using System.Collections.Generic;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;

namespace SpiritAutomataDemonstration.Constraints
{
    public class RotationToLinearInverseSolver : DirectInverseSolver<RotationToLinearConstraint>
    {
        private RotationAxisSolver _rotationSolver;
        private TelescopeLinearAxisSolver _linearSolver;

        public RotationToLinearInverseSolver()
        {
            _rotationSolver = new RotationAxisSolver();
            _linearSolver = new TelescopeLinearAxisSolver();
        }

        protected override bool SolveFirstToSecond(RotationToLinearConstraint constraint, Snapshot snapShot)
        {
            var result = true;

            var first = constraint.First.Body;
            var second = constraint.Second.Body;

            var linearBody = first.Constraints.OfType<TelescopeLinearAxisConstraint>().Any() ? first : second;
            var linearConstraint = linearBody.Constraints.OfType<TelescopeLinearAxisConstraint>().First();

            var rotationBody = linearBody == first ? second : first;
            var rotationConstraint = rotationBody.Constraints.OfType<RotationAxisConstraint>().First();

            if(first == linearBody)
            {
                rotationConstraint.Angle = linearConstraint.LinearPosition.ToRadiant();
                result = _rotationSolver.Solve(rotationConstraint, rotationBody, new Snapshot(new Dictionary<object, IState>()));
            }
            else 
            {
                linearConstraint.LinearPosition = rotationConstraint.Angle.ToDegree();
                result = _linearSolver.Solve(linearConstraint, linearBody, new Snapshot(new Dictionary<object, IState>()));
            }

            return result;
        }

        protected override bool SolveSecondToFirst(RotationToLinearConstraint constraint, Snapshot snapShot)
        {
            var result = true;

            var first = constraint.First.Body;
            var second = constraint.Second.Body;

            var linearBody = first.Constraints.OfType<TelescopeLinearAxisConstraint>().Any() ? first : second;
            var linearConstraint = linearBody.Constraints.OfType<TelescopeLinearAxisConstraint>().First();

            var rotationBody = linearBody == first ? second : first;
            var rotationConstraint = rotationBody.Constraints.OfType<RotationAxisConstraint>().First();

            if (second == linearBody)
            {
                rotationConstraint.Angle = linearConstraint.LinearPosition.ToRadiant();
                result = _rotationSolver.Solve(rotationConstraint, rotationBody, new Snapshot(new Dictionary<object, IState>()));
            }
            else
            {
                linearConstraint.LinearPosition = rotationConstraint.Angle.ToDegree();
                result = _linearSolver.Solve(linearConstraint, linearBody, new Snapshot(new Dictionary<object, IState>()));
            }

            return result;
        }
    }
}
