﻿namespace Kinematicula.Kinematics.DirectInverseSolving;

using Kinematicula.Graphics;

public class FixedSolver : DirectInverseSolver<FixedConstraint>
{
    public bool IsValid(FixedConstraint constraint)
    {
        var shouldFrame = constraint.First.Body.Frame * constraint.First.ConnectionFrame * constraint.Second.ConnectionFrame.Inverse();
        var result = constraint.Second.Body.Frame == shouldFrame;

        return result;
    }

    protected override bool SolveFirstToSecond(FixedConstraint fixedConstraint)
    {
        if (IsValid(fixedConstraint))
            return true;

        var result = Solve(fixedConstraint.First, fixedConstraint.Second);
        return result;
    }

    protected override bool SolveSecondToFirst(FixedConstraint fixedConstraint)
    {
        if (IsValid(fixedConstraint))
            return true;

        var result = Solve(fixedConstraint.Second, fixedConstraint.First);
        return result;
    }

    private bool Solve(Anchor first, Anchor second)
    {
        var shouldFrame = first.Body.Frame * first.ConnectionFrame * second.ConnectionFrame.Inverse();
        second.Body.Frame = shouldFrame;
        var result = second.Body.Frame == shouldFrame;

        if (!result)
        {
            shouldFrame = second.Body.Frame * second.ConnectionFrame * first.ConnectionFrame.Inverse();
            first.Body.Frame = shouldFrame;
        }

        return result;
    }
}
