using Kinematicula.Graphics;
using Kinematicula.Kinematics.DirectInverseSolving;

namespace Kinematicula.Kinematics
{
    public interface IDirectInverseConstraintSolver
    {
        void AddSolver(IDirectInverseSolver solver);

        void AddSolvers(IEnumerable<IDirectInverseSolver> solvers);

        bool Solve(Body start);
    }
}