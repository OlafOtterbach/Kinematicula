using Kinematicula.Graphics;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
    public interface IDirectInverseSolver
    {
        bool Solve(Constraint constraint, Body startingBody);
    }
}