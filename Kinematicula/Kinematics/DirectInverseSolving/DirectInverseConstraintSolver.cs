using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var startFrame = snapshot.GetFrameFor(startBody);
            var endFrame = startBody.Frame;
            var isValid = Solve(startBody, null, snapshot);
            if (!isValid)
            {
                snapshot.ResetScene();
                TransformMath.CalculateAxesAndRotation(startFrame, endFrame, out var axisAlpha, out var angleAlpha, out var axisBeta, out var angleBeta);

                var translation = endFrame.Offset - startFrame.Offset;

                var start = 0.0;
                var end = 1.0;
                isValid = false;
                var index = 0;
                while (!start.EqualsTo(end))
                {
                    snapshot.ResetScene();
                    var half = (start + end) / 2.0;
                    var alpha = half * angleAlpha;
                    var beta = half * angleBeta;
                    var alphaRotation = Matrix44D.CreateRotation(startFrame.Offset, axisAlpha, alpha);
                    var betaRotation = Matrix44D.CreateRotation(startFrame.Offset, axisBeta, beta);
                    var rotation = alphaRotation * betaRotation;

                    var translationStep = Matrix44D.CreateTranslation(half * translation);

                    startBody.Frame = translationStep * rotation * startFrame;
                    isValid = Solve(startBody, null, snapshot);
                    if (isValid)
                    {
                        start = half;
                    }
                    else
                    {
                        end = half;
                    }

                    index++;
                }

                if (!isValid)
                {
                    snapshot.ResetScene();
                    var half = start;
                    var alpha = half * angleAlpha;
                    var beta = half * angleBeta;
                    var alphaRotation = Matrix44D.CreateRotation(startFrame.Offset, axisAlpha, alpha);
                    var betaRotation = Matrix44D.CreateRotation(startFrame.Offset, axisBeta, beta);
                    var rotation = alphaRotation * betaRotation;
                    var translationStep = Matrix44D.CreateTranslation(half * translation);
                    startBody.Frame = translationStep * rotation * startFrame;
                    isValid = Solve(startBody, null, snapshot);
                    if(!isValid)
                    {
                        snapshot.ResetScene();
                    }
                }
            }

            return true;
        }

        private bool Solve(Body start, Constraint originConstraint, Snapshot snapshot)
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
                    return false;
                }

                known.UnionWith(constraints);
            }

            return true;
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