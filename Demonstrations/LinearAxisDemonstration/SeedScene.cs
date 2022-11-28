namespace LinearAxisDemonstration;

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


        // first socket
        var firstSocket = Cube.Create(100);
        firstSocket.Name = "first socket";
        firstSocket.Frame = Matrix44D.CreateTranslation(new Vector3D(-250, 0, 50));
        scene.AddBody(firstSocket);

        var fixedFirstSocketToFloor = new FixedConstraint(new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(-250, 0,0))), new Anchor(firstSocket, Matrix44D.CreateTranslation(new Vector3D(0,0,-50))));

        // second socket
        var secondSocket = Cube.Create(100);
        secondSocket.Name = "second socket";
        secondSocket.Frame = Matrix44D.CreateTranslation(new Vector3D(250, 0, 50));
        scene.AddBody(secondSocket);

        var fixedSecondSocketToFloor = new FixedConstraint(new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(250, 0, 0))), new Anchor(secondSocket, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))));



        // first shifter
        var firstShifter = Cuboid.Create(500, 100, 100);
        firstShifter.Name = "first shifter";
        firstShifter.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
        scene.AddBody(firstShifter);

        new LinearAxisConstraint(
                        new Anchor(firstSocket, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
                        new Anchor(firstShifter, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
                        0,
                       -200,
                        200);

        // second shifter
        var secondShifter = Cuboid.Create(500, 100, 100);
        secondShifter.Name = "second shifter";
        secondShifter.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
        scene.AddBody(secondShifter);

        new LinearAxisConstraint(
                        new Anchor(secondSocket, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
                        new Anchor(secondShifter, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))),
                        0,
                       -100,
                        200);


        var firstAnchor = new Anchor(firstShifter, Matrix44D.CreateTranslation(new Vector3D(250, 0, 0)));
        var secondAnchor = new Anchor(secondShifter, Matrix44D.CreateTranslation(new Vector3D(-250, 0, 0)));
        var fixedFirstToSecondShifter = new FixedConstraint(firstAnchor, secondAnchor);

        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        camera.SetCamera(0.0, 45.0, 1500.0);
        scene.AddBody(camera);

        scene.InitScene();
        return scene;
    }
}