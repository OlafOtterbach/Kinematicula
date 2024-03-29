﻿namespace TelescopeRotationTowerDemonstration;

using Kinematicula.Scening;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Graphics.Extensions;

public static class SeedScene
{
    public static Scene CreateAndPopulateScene()
    {
        var scene = new Scene(new DirectForwardConstraintSolver(), new DirectInverseConstraintSolver());

        // floor
        var floor = Floor.Create(10, 100);
        floor.Name = "floor";
        scene.AddBody(floor);
        var fixedToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));

        var cube1 = Cube.Create(100);
        cube1.Frame = Matrix44D.CreateTranslation(new Vector3D(0,0,50));
        cube1.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), cube1));
        scene.AddBody(cube1);

        var axis1 = new TelescopeRotationAxisConstraint(
            new Anchor(floor, Matrix44D.Identity),
            new Anchor(cube1, Matrix44D.CreateTranslation(new Vector3D(0,0,-50))),
            0.0.ToRadiant(),
            -90.0.ToRadiant(),
            +90.0.ToRadiant());

        var cube2 = Cube.Create(100);
        cube2.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 150));
        cube2.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), cube2));
        scene.AddBody(cube2);

        var axis2 = new TelescopeRotationAxisConstraint(
            new Anchor(cube1, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
            new Anchor(cube2, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
            0.0.ToRadiant(),
            -90.0.ToRadiant(),
            +90.0.ToRadiant());

        var cube3 = Cube.Create(100);
        cube3.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 250));
        cube3.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), cube3));
        scene.AddBody(cube3);

        var axis3 = new TelescopeRotationAxisConstraint(
            new Anchor(cube2, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
            new Anchor(cube3, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
            0.0.ToRadiant(),
            -90.0.ToRadiant(),
            +90.0.ToRadiant());

        var cube4 = Cube.Create(100);
        cube4.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 250));
        cube4.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), cube4));
        scene.AddBody(cube4);

        var axis4 = new TelescopeRotationAxisConstraint(
            new Anchor(cube3, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
            new Anchor(cube4, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
            0.0.ToRadiant(),
            -90.0.ToRadiant(),
            +90.0.ToRadiant());

        var cube5 = Cube.Create(100);
        cube5.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 250));
        cube5.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), cube5));
        scene.AddBody(cube5);

        var axis5 = new TelescopeRotationAxisConstraint(
            new Anchor(cube4, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
            new Anchor(cube5, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
            0.0.ToRadiant(),
            -90.0.ToRadiant(),
            +90.0.ToRadiant());

        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        camera.SetCameraToOrigin(30.0.ToRadiant(), 30.0.ToRadiant(), 1800.0);
        scene.AddBody(camera);

        scene.InitScene();
        return scene;
    }
}