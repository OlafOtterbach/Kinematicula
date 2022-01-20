using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;
using Kinematicula.Kinematics.DirectInverseSolving;
using System.Collections.Generic;

namespace Kinematicula.Kinematics
{
    public interface IDirectInverseConstraintSolver
    {
        void AddSolver(IDirectInverseSolver solver);

        void AddSolvers(IEnumerable<IDirectInverseSolver> solvers);

        bool Solve(Body start, Snapshot snapshot);
    }
}