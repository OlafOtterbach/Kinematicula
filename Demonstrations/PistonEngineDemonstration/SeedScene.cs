﻿namespace PistonEngineDemonstration;

using Kinematicula.Scening;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Graphics.Extensions;
using PistonEngineDemonstration.PistonMotor;

public static class SeedScene
{
    public static Scene CreateAndPopulateScene()
    {
        var scene = new Scene(new DirectForwardConstraintSolver(), new DirectInverseConstraintSolver());
        scene.AddInverseSolver(new WheelRotationInverseSolver());
        scene.AddInverseSolver(new PistonLinearAxisInverseSolver());

        // floor
        var floor = Floor.Create(10, 100, new Color(0, 1, 0));
        floor.Name = "Floor";
        scene.AddBody(floor);
        var fixedToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));

        // motor
        var motor = MotorCreator.Create(scene, new Color(0, 1, 0));
        scene.AddBody(motor);
        var fixedToFloorConstraint = new FixedConstraint(new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(0, 0, 200))), new Anchor(motor, Matrix44D.Identity));

        // camera
        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        camera.SetCameraToOrigin(30.0.ToRadiant(), 30.0.ToRadiant(), 1800.0);
        camera.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 200)) * camera.Frame;
        scene.AddBody(camera);

        scene.InitScene();
        return scene;
    }
}