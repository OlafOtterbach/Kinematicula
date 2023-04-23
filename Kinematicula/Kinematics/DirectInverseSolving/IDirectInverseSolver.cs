namespace Kinematicula.Kinematics.DirectInverseSolving;

using Kinematicula.Graphics;

public interface IDirectInverseSolver
{
    bool Solve(Constraint constraint, Body startingBody);
}