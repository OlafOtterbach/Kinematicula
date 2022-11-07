﻿namespace FormiculaDemonstration;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Graphics.Extensions;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Scening;
using FormiculaDemonstration.Robot.Kinematics;
using FormiculaDemonstration.Robot.Graphics;

public static class SeedScene
{
    public static Scene CreateAndPopulateScene()
    {
        var scene = new Scene(new DirectForwardConstraintSolver(), new DirectInverseConstraintSolver());
        scene.AddInverseSolver(new RobotInverseSolver(scene.ForwardSolver));
        scene.AddInverseSolver(new GripperToClampInverseSolver());
        scene.AddForwardSolver(new RobotForwardSolver());
        scene.AddForwardSolver(new GripperToClampForwardSolver());

        // floor
        var floor = Floor.Create(10, 100);
        floor.Name = "floor";
        scene.AddBody(floor);
        var floorToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));

        var robot1 = RobotCreator.Create();
        robot1.Name = "robot 1";
        robot1.Frame = Matrix44D.CreateTranslation(new Vector3D(-400, 0, 0));
        var fixedRobotToFloorConstraint1 = new FixedConstraint(
            new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(-400, 0, 0))),
            new Anchor(robot1, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
        scene.AddBody(robot1);

        var robot2 = RobotCreator.Create();
        robot2.Name = "robot 2";
        robot2.Frame = Matrix44D.CreateCoordinateSystem(new Position3D(400, 0, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1));
        var fixedRobotToFloorConstraint2 = new FixedConstraint(
            new Anchor(floor, Matrix44D.CreateCoordinateSystem(new Position3D(400, 0, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1))),
            new Anchor(robot2, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
        scene.AddBody(robot2);

        //var robot3 = RobotCreator.Create();
        //var fixedRobotToFloorConstraint3 = new FixedConstraint(
        //    new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(-400, -200, 0))),
        //    new Anchor(robot3, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
        //scene.AddBody(robot3);

        //var robot4 = RobotCreator.Create();
        //var fixedRobotToFloorConstraint4 = new FixedConstraint(
        //    new Anchor(floor, Matrix44D.CreateCoordinateSystem(new Position3D(400,-200, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1))),
        //    new Anchor(robot4, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
        //scene.AddBody(robot4);

        var antBody = Cuboid.Create(200,50, 200);
        antBody.Frame = Matrix44D.CreateTranslation(new Vector3D(270.57366943359375 + 100 - 400.0, 0, 485.705078125));
        antBody.Name = "ant body";
        scene.AddBody(antBody);

        var fixedRobot1ToAntBody = new FixedConstraint(
            new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(-100, 0, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0))),
            new Anchor(robot1.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

        var fixedRobot2ToAntBody = new FixedConstraint(
            new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(100, 0, 0), new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0))),
            new Anchor(robot2.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

        //var fixedRobot3ToAntBody = new FixedConstraint(
        //    new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(-100, -100, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0))),
        //    new Anchor(robot3.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

        //var fixedRobot4ToAntBody = new FixedConstraint(
        //    new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(100, -100, 0), new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0))),
        //    new Anchor(robot4.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

        // camera
        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        camera.SetCamera(30.0, 30.0, 1800.0);
        scene.AddBody(camera);

        var result = scene.InverseSolver.TrySolve(antBody);

        scene.InitScene();
        return scene;
    }
}
