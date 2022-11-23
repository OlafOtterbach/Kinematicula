using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.Scening;

namespace Kinematicula.Kinematics.DirectForwardSolving
{
    public class DirectForwardConstraintSolver : IDirectForwardConstraintSolver
    {
        private Dictionary<Type, IDirectForwardSolver> _solvers;

        public DirectForwardConstraintSolver()
        {
            var solvers = new List<IDirectForwardSolver>
            {
                new FixedSolver(),
                new LinearAxisSolver(),
                new RotationAxisSolver(),
                new TelescopeLinearAxisSolver(),
                new TelescopeRotationAxisSolver(),
            };

            _solvers = solvers.ToDictionary(solver => solver.GetType());
        }

        public void AddSolver(IDirectForwardSolver solver)
        {
            if (solver != null && !_solvers.ContainsKey(solver.GetType()))
            {
                _solvers[solver.GetType()] = solver;
            }
        }

        public void AddSolvers(IEnumerable<IDirectForwardSolver> solvers)
        {
            foreach (var solver in solvers)
            {
                AddSolver(solver);
            }
        }

        public void Solve(Scene Scene)
        {
            Solve(Scene.World);
        }

        public void Solve(Body start)
        {
            var known = new HashSet<Constraint>();
            var stack = new Stack<Body>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var currentBody = stack.Pop();
                var constraints = currentBody.Constraints;
                constraints.Where(constraint => !known.Contains(constraint))
                      .Select(constraint => Execute(constraint, currentBody))
                      .ToList()
                      .ForEach(body => stack.Push(body));
                known.UnionWith(constraints);
            }
        }

        public void SolveLocal(Body start)
        {
            var bodies = start.GetBodyAndDescendants().ToList();
            Dictionary<Body, Constraint[]> constraintsOfBody = bodies.ToDictionary(body => body, body => body.Constraints.Where(c => bodies.Contains(c.First.Body) && bodies.Contains(c.Second.Body)).ToArray());
            Solve(start, constraintsOfBody);
        }

        private void Solve(Body start, Dictionary<Body, Constraint[]> constraintsOfBody)
        {
            var known = new HashSet<Constraint>();
            var stack = new Stack<Body>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var currentBody = stack.Pop();
                var constraints = constraintsOfBody[currentBody];
                constraints.Where(constraint => !known.Contains(constraint))
                      .Select(constraint => Execute(constraint, currentBody))
                      .ToList()
                      .ForEach(body => stack.Push(body));
                known.UnionWith(constraints);
            }
        }

        private Body Execute(Constraint constraint, Body currentBody)
        {
            foreach (var solver in _solvers.Values)
            {
                solver.Solve(constraint, currentBody);
            }

            return constraint.First.Body == currentBody ? constraint.Second.Body : constraint.First.Body;
        }
    }
}