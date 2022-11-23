namespace Kinematicula.Kinematics.DirectInverseSolving;

using Kinematicula.Graphics;

public class DirectInverseConstraintSolver : IDirectInverseConstraintSolver
{
    private Dictionary<Type, IDirectInverseSolver> _solvers;

    public DirectInverseConstraintSolver()
    {
        var solvers = new List<IDirectInverseSolver>
        {
            new FixedSolver(),
            new LinearAxisSolver(),
            new RotationAxisSolver(),
            new TelescopeLinearAxisSolver(),
            new TelescopeRotationAxisSolver(),
        };

        _solvers = solvers.ToDictionary(solver => solver.GetType());
    }

    public void AddSolver(IDirectInverseSolver solver)
    {
        if (solver != null && !_solvers.ContainsKey(solver.GetType()))
        {
            _solvers[solver.GetType()] = solver;
        }
    }

    public void AddSolvers(IEnumerable<IDirectInverseSolver> solvers)
    {
        foreach (var solver in solvers)
        {
            AddSolver(solver);
        }
    }

    public bool TrySolve(Body startBody)
    {
        Console.WriteLine("Start");
        var result = TrySolve(startBody, null, 1);

        return result;
    }

    private bool TrySolve(Body startBody, Constraint startConstraint, int count)
    {
        var result = Solve(startBody, null);
        if (!result.IsSolved)
        {
            if (count > 100)
                return false;

            if (!TrySolve(result.BreakingBody, result.BreakingConstraint, count + 1))
            {
                return false;
            }
        }

        return true;
    }

    private (bool IsSolved, Body BreakingBody, Constraint BreakingConstraint) Solve(Body start , Constraint startConstraint)
    {
        var known = new HashSet<Constraint>();
        var stack = new Stack<Body>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var currentBody = stack.Pop();
            var constraints = startConstraint == null ? currentBody.Constraints : new List<Constraint>();
            startConstraint = null;

            var solveResult = constraints
                              .Where(constraint => !known.Contains(constraint))
                              .Where(constraint => !IsValid(constraint))
                              .Select(constraint => Execute(constraint, currentBody))
                              .ToList();
            var unsolved = solveResult.Where(x => !x.isSolved).ToList();
            if (!unsolved.Any())
            {
                solveResult.ForEach(x => stack.Push(x.BreakingBody));
            }
            else
            {
                var breakingReason = solveResult.First(x => !x.isSolved);
                return breakingReason;
            }

            known.UnionWith(constraints);
        }

        return (true, null, null);
    }

    private bool IsValid(Constraint constraint)
    {
        var isValid = _solvers.Values.Select(x => x.IsValid(constraint)).Any(x => x);
        return isValid;
    }

    private (bool isSolved, Body BreakingBody, Constraint BreakinConstraint)
    Execute(Constraint constraint, Body currentBody)
    {
        var isSolved = _solvers.Values.Select(x => x.Solve(constraint, currentBody)).All(x => x);

        if (isSolved)
        {
            return (isSolved, constraint.First.Body == currentBody ? constraint.Second.Body : constraint.First.Body, constraint);
        }
        else
        {
            return (isSolved, constraint.First.Body == currentBody ? constraint.First.Body : constraint.Second.Body, constraint);
        }
    }
}