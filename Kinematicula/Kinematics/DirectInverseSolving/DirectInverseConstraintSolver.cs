using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
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

        public bool Solve(Body startBody, Snapshot snapshot)
        {
            var result = Solve(startBody, null, snapshot);
            if (!result.IsSolved)
            {
                Solve(result.BreakingBody, snapshot);
            }

            return true;
        }

        private (bool IsSolved, Body BreakingBody) Solve(Body start, Constraint originConstraint, Snapshot snapshot)
        {
            var known = new HashSet<Constraint>();
            if (originConstraint != null) known.Add(originConstraint);
            var stack = new Stack<Body>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var currentBody = stack.Pop();
                var constraints = currentBody.Constraints;

                var solveResult = constraints.Where(constraint => !known.Contains(constraint))
                                  .Select(constraint => Execute(constraint, currentBody, snapshot))
                                  .ToList();
                var unsolved = solveResult.Where(x => !x.IsValid).ToList();
                if (!unsolved.Any())
                {
                    solveResult.ForEach(x => stack.Push(x.Body));
                }
                else
                {
                    var breakingBody = solveResult.First(x => !x.IsValid).Body;
                    return (false, breakingBody);
                }

                known.UnionWith(constraints);
            }

            return (true, null);
        }

        private (bool IsValid, Body Body, Constraint Constraint)
        Execute(Constraint constraint, Body currentBody, Snapshot snapshot)
        {
            var isValid = _solvers.Values.Select(x => x.Solve(constraint, currentBody, snapshot)).All(x => x);

            if (isValid)
            {
                return (isValid, constraint.First.Body == currentBody ? constraint.Second.Body : constraint.First.Body, constraint);
            }
            else
            {
                return (isValid, constraint.First.Body == currentBody ? constraint.First.Body : constraint.Second.Body, constraint);
            }
        }
    }
}