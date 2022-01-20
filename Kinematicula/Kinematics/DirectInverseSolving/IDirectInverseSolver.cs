using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
    public interface IDirectInverseSolver
    {
        bool Solve(Constraint constraint, Body startingBody, Snapshot snapShot);
    }
}