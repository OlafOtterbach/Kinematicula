namespace TelescopeLinearTowerDemonstration;

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

        var cube1 = Cuboid.Create(500, 100, 100);
        cube1.Frame = Matrix44D.CreateTranslation(new Vector3D(0,0,50));
        cube1.Name = "cube 1";
        cube1.AddSensor(new LinearSensor(new Vector3D(1, 0, 0), cube1));
        scene.AddBody(cube1);

        var axis1 = new TelescopeLinearAxisConstraint(
            new Anchor(floor, Matrix44D.Identity),
            new Anchor(cube1, Matrix44D.CreateTranslation(new Vector3D(0,0,-50))),
            0.0,
            -100.0,
            +100.0);

        var cube2 = Cuboid.Create(500, 100, 100);
        cube2.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 150));
        cube2.Name = "cube 2";
        cube2.AddSensor(new LinearSensor(new Vector3D(1, 0, 0), cube2));
        scene.AddBody(cube2);

        var axis2 = new TelescopeLinearAxisConstraint(
            new Anchor(cube1, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
            new Anchor(cube2, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
            0.0,
            -100.0,
            +100.0);

        var cube3 = Cuboid.Create(500, 100, 100);
        cube3.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 250));
        cube3.Name = "cube 3";
        cube3.AddSensor(new LinearSensor(new Vector3D(1, 0, 0), cube3));
        scene.AddBody(cube3);

        var axis3 = new TelescopeLinearAxisConstraint(
            new Anchor(cube2, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
            new Anchor(cube3, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
            0.0,
            -100.0,
            +100.0);

        var cube4 = Cuboid.Create(500, 100, 100);
        cube4.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 250));
        cube4.Name = "cube 4";
        cube4.AddSensor(new LinearSensor(new Vector3D(1, 0, 0), cube3));
        scene.AddBody(cube4);

        var axis4 = new TelescopeLinearAxisConstraint(
            new Anchor(cube3, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
            new Anchor(cube4, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
            0.0,
            -100.0,
            +100.0);

        var cube5 = Cuboid.Create(500, 100, 100);
        cube5.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 250));
        cube5.Name = "cube 5";
        cube5.AddSensor(new LinearSensor(new Vector3D(1, 0, 0), cube5));
        scene.AddBody(cube5);

        var axis5 = new TelescopeLinearAxisConstraint(
            new Anchor(cube4, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
            new Anchor(cube5, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
            0.0,
            -100.0,
            +100.0);

        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        camera.SetCamera(30.0, 30.0, 1800.0);
        scene.AddBody(camera);

        scene.InitScene();
        return scene;
    }
}