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
        var result = TrySolve(startBody, 1);

        return result;
    }

    private bool TrySolve(Body startBody, int count)
    {
        var result = Solve(startBody, null);
        if (!result.IsSolved)
        {
            if (count > 100)
                return false;

            if (!TrySolve(result.BreakingBody, count + 1))
            {
                return false;
            }
        }

        return true;
    }

    private (bool IsSolved, Body BreakingBody) Solve(Body start, Constraint originConstraint)
    {
        var known = new HashSet<Constraint>();
        if (originConstraint != null) known.Add(originConstraint);
        var stack = new Stack<Body>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var currentBody = stack.Pop();
            var constraints = currentBody.Constraints;

            var solveResult = constraints
                              .Where(constraint => !known.Contains(constraint))
                              .Where(constraint => !IsValid(constraint))
                              .Select(constraint => Execute(constraint, currentBody))
                              .ToList();
            var unsolved = solveResult.Where(x => !x.isSolved).ToList();
            if (!unsolved.Any())
            {
                solveResult.ForEach(x => stack.Push(x.Body));
            }
            else
            {
                var breakingBody = solveResult.First(x => !x.isSolved).Body;
                return (false, breakingBody);
            }

            known.UnionWith(constraints);
        }

        return (true, null);
    }

    private bool IsValid(Constraint constraint)
    {
        var isValid = _solvers.Values.Select(x => x.IsValid(constraint)).Any(x => x);
        return isValid;
    }

    private (bool isSolved, Body Body) Execute(Constraint constraint, Body currentBody)
    {
        var isSolved = _solvers.Values.Select(x => x.Solve(constraint, currentBody)).All(x => x);

        if (isSolved)
        {
            return (isSolved, constraint.First.Body == currentBody ? constraint.Second.Body : constraint.First.Body);
        }
        else
        {
            return (isSolved, constraint.First.Body == currentBody ? constraint.First.Body : constraint.Second.Body);
        }
    }
}