using Kinematicula.Graphics;

namespace Kinematicula.Kinematics.DirectForwardSolving
{
    public interface IDirectForwardSolver
    {
        void Solve(Constraint constraint, Body startEntity);
    }
}
