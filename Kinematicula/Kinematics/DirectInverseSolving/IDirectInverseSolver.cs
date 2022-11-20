namespace Kinematicula.Kinematics.DirectInverseSolving;

using Kinematicula.Graphics;

public interface IDirectInverseSolver
{
    bool IsValid(Constraint constraint);

    bool Solve(Constraint constraint, Body startingBody);
}