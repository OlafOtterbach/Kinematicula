﻿namespace FormiculaDemonstration;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Graphics.Extensions;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Scening;
using RobotLib.Kinematics;
using FormiculaDemonstration.Ant;
using System.Linq;

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
        var floor = Floor.Create(10, 100, new Color(0, 1, 0));
        floor.Name = "floor";
        scene.AddBody(floor);
        var floorToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));

        var ant = AntCreator.Create(new Color(0,1,0));
        var fixedAntToFloorConstraint1 = new FixedConstraint(
            new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))),
            new Anchor(ant, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
        scene.AddBody(ant);

        // camera
        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        camera.SetCameraToOrigin(30.0.ToRadiant(), 30.0.ToRadiant(), 1800.0);
        scene.AddBody(camera);

        var result = scene.InverseSolver.TrySolve(ant.Children.First(x => x.Name == "ant body"));

        scene.InitScene();
        return scene;
    }
}