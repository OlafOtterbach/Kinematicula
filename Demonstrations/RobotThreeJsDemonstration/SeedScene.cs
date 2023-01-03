namespace DoubleRobotDemonstration;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Graphics.Extensions;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Scening;
using RobotLib.Graphics;
using RobotLib.Kinematics;

public static class SeedScene
{
    public static Scene CreateAndPopulateScene()
    {
        var scene = new Scene(new DirectForwardConstraintSolver(), new DirectInverseConstraintSolver());
        scene.AddInverseSolver(new RobotInverseSolver(scene.ForwardSolver));
        scene.AddInverseSolver(new GripperToClampInverseSolver());
        scene.AddForwardSolver(new RobotForwardSolver());
        scene.AddForwardSolver(new GripperToClampForwardSolver());

        //var cube = Cube.Create(100);
        //scene.AddBody(cube);

        //var cylinder = Cylinder.Create(10, 50, 200);
        //scene.AddBody(cylinder);

        //var sphere = Sphere.Create(8, 100);
        //scene.AddBody(sphere);

        //var oval = Oval.Create(32, 1, 50, 50, false, true, false, true, 200, 50, Matrix44D.Identity);
        //scene.AddBody(oval);

        
        // floor
        var floor = new Body();// Floor.Create(10, 100);
        floor.Name = "floor";
        scene.AddBody(floor);
        var floorToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));

        var robot1 = RobotCreator.Create();
        robot1.Name = "robot 1";
        robot1.Frame = Matrix44D.CreateTranslation(new Vector3D(-400, 0, 0));
        robot1.Gripper.OpeningWidth = 50;
        var fixedRobotToFloorConstraint1 = new FixedConstraint(
            new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(-400, 0, 0))),
            new Anchor(robot1, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
        scene.AddBody(robot1);

        var robot2 = RobotCreator.Create();
        robot2.Name = "robot 2";
        robot2.Frame = Matrix44D.CreateCoordinateSystem(new Position3D(400, 0, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1));
        robot2.Gripper.OpeningWidth = 50;
        var fixedRobotToFloorConstraint2 = new FixedConstraint(
            new Anchor(floor, Matrix44D.CreateCoordinateSystem(new Position3D(400, 0, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1))),
            new Anchor(robot2, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
        scene.AddBody(robot2);

        var cubeBody = Cuboid.Create(150, 50, 200);
        cubeBody.Frame = Matrix44D.CreateTranslation(new Vector3D(0.0, 0, 485.705078125));
        cubeBody.Name = "cube body";
        cubeBody.AddSensor(new PlaneSensor("left mouse button", new Vector3D(0, 1, 0)));
        cubeBody.AddSensor(new SphereSensor("left mouse button and control key", new Position3D(0, 0, 25), 10.ToRadiant()));

        scene.AddBody(cubeBody);

        var fixedRobot1ToAntBody = new FixedConstraint(
            new Anchor(cubeBody, Matrix44D.CreateCoordinateSystem(new Position3D(-100, 0, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0))),
            new Anchor(robot1.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

        var fixedRobot2ToAntBody = new FixedConstraint(
            new Anchor(cubeBody, Matrix44D.CreateCoordinateSystem(new Position3D(100, 0, 0), new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0))),
            new Anchor(robot2.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

        // camera
        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        camera.SetCamera(30.0, 30.0, 1800.0);
        scene.AddBody(camera);

        //var result = scene.InverseSolver.TrySolve(cubeBody);

        scene.InitScene();
        return scene;
    }
}