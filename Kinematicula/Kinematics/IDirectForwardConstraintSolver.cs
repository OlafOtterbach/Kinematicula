using System.Collections.Generic;
using Kinematicula.Graphics;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Scening;

namespace Kinematicula.Kinematics
{
    public interface IDirectForwardConstraintSolver
    {
        void AddSolver(IDirectForwardSolver solver);

        void AddSolvers(IEnumerable<IDirectForwardSolver> solvers);


        void Solve(Scene Scene);

        // Solve all constraints starting at start body.
        void Solve(Body start);

        // Solve constraints only inside start body.
        void SolveLocal(Body start);
    }
}