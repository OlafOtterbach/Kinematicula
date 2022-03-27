using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.Kinematics;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;

namespace Kinematicula.Scening
{
    public class Scene
    {
        public Scene(
            IDirectForwardConstraintSolver forwardSolver,
            IDirectInverseConstraintSolver inverseSolver)
        {
            var world = new World();
            world.Name = "World";

            Bodies = new List<Body>() { world };
            ForwardSolver = forwardSolver;
            InverseSolver = inverseSolver;
        }

        public IDirectForwardConstraintSolver ForwardSolver { get; }

        public IDirectInverseConstraintSolver InverseSolver  { get; }

        public World World => Bodies.OfType<World>().First();

        public List<Body> Bodies { get; }

        public void AddBody(Body body)
        {
            var bodies = body.GetBodyAndDescendants();
            Bodies.AddRange(bodies);
        }

        public void AddForwardSolver(IDirectForwardSolver solver) => ForwardSolver.AddSolver(solver);

        public void AddInverseSolver(IDirectInverseSolver solver) => InverseSolver.AddSolver(solver);

        public void SetBodyFrame(Body body, Matrix44D bodyFrame)
        {
            if (body != null)
            {
                body.Frame = bodyFrame;
            }
            if(!InverseSolver.Solve(body))
            {

            }

            // Correction for precision of calculated solve result.
            ForwardSolver.Solve(this);
        }

        public void InitScene()
        {
            ForwardSolver.Solve(World);
        }
    }
}
