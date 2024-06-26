﻿namespace RobotThreeJsDemonstration;

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
        scene.Background = new Color(0.8, 0.8, 0.8);
        scene.AddInverseSolver(new RobotInverseSolver(scene.ForwardSolver));
        scene.AddInverseSolver(new GripperToClampInverseSolver());
        scene.AddForwardSolver(new RobotForwardSolver());
        scene.AddForwardSolver(new GripperToClampForwardSolver());

        // floor
        var floor = Floor.Create(10, 100, new Color(1, 1, 1), new Color(0, 0, 0.4), new Color(0.2, 0.2, 0.2));
        floor.Name = "Floor";
        scene.AddBody(floor);
        var floorToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));

        var robot = RobotCreator.Create(
            12,
            12,
            8,
            false,
            new Color(1.0, 1.0, 1.0),
            new Color(1.0, 1.0, 1.0),
            new Color(0.0, 0.0, 1.0),
            new Color(0.8, 0.8, 0.8),
            new Color(0.0, 0.0, 1.0),
            new Color(0.8, 0.8, 0.8),
            new Color(0.0, 0.0, 1.0),
            new Color(0.0, 0.0, 1.0),
            new Color(0.8, 0.8, 0.8),
            new Color(0.8, 0.8, 0.8),
            new Color(0.0, 0.0, 0.0),
            new Color(0.4, 0.4, 0.4),
            new Color(0.2, 0.2, 0.2));

        robot.SetAxisAngle(1,  00.ToRadiant());
        robot.SetAxisAngle(2,  45.ToRadiant());
        robot.SetAxisAngle(3,  90.ToRadiant());
        robot.SetAxisAngle(4,  00.ToRadiant());
        robot.SetAxisAngle(5, -45.ToRadiant());
        robot.SetAxisAngle(6,  00.ToRadiant());

        var fixedRobotToFloorConstraint = new FixedConstraint(
            new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))),
            new Anchor(robot, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
        scene.AddBody(robot);

        // camera
        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        //camera.SetCameraToOrigin(0.0.ToRadiant(), 10.0.ToRadiant(), 1800.0);
        camera.SetCameraToOrigin(-230.0.ToRadiant(), 30.0.ToRadiant(), 1800.0);

        scene.AddBody(camera);

        scene.InitScene();
        return scene;
    }
}