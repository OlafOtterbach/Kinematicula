namespace Kinematicula.Scening;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Extensions;
using Kinematicula.Kinematics;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;

public class Scene
{
    public Scene(
        IDirectForwardConstraintSolver forwardSolver,
        IDirectInverseConstraintSolver inverseSolver)
    {
        var world = new World();
        world.Name = "World";

        var defaultCamera = new Camera()
        {
            Name = string.Empty,
            NearPlane = 1.0,
            Target = new Position3D(),
        };

        defaultCamera.SetCameraToOrigin(0.0.ToRadiant(), 45.0.ToRadiant(), 3000.0);

        Bodies = new List<Body>() { world, defaultCamera };
        ForwardSolver = forwardSolver;
        InverseSolver = inverseSolver;
    }

    public IDirectForwardConstraintSolver ForwardSolver { get; }

    public IDirectInverseConstraintSolver InverseSolver { get; }

    public Color Background { get; set; } = new Color(0, 0, 0);

    public World World => Bodies.OfType<World>().First();

    public List<Body> Bodies { get; }

    public Body GetBody(Guid id)
    {
        var body = Bodies.FirstOrDefault(b => b.Id == id);
        return body;
    }

    public void AddBody(Body body)
    {
        var bodies = body.GetBodyAndDescendants();
        Bodies.AddRange(bodies);
    }

    public void AddForwardSolver(IDirectForwardSolver solver) => ForwardSolver.AddSolver(solver);

    public void AddInverseSolver(IDirectInverseSolver solver) => InverseSolver.AddSolver(solver);

    public void SetBodyFrame(Body body, Matrix44D bodyFrame)
    {
        var freezedScene = new FreezeScene(this);

        if (body != null)
        {
            body.Frame = bodyFrame;

            if (!InverseSolver.TrySolve(body))
            {
                // reset scene and kinematics
                freezedScene.ResetScene();
                InverseSolver.TrySolve(body);
            }
        }
    }

    public void InitScene()
    {
        ForwardSolver.Solve(World);
    }
}
