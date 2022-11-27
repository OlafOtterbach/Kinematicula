namespace CylinderDemonstration;

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
        floor.Name = "Floor";
        scene.AddBody(floor);
        var fixedToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));

        // Crank socket
        var socket = Cylinder.Create(10, 50, 200);
        scene.AddBody(socket);
        var floorAnchor = new Anchor(floor, Matrix44D.Identity);
        var socketToFloorAnchor = new Anchor(socket, Matrix44D.CreateTranslation(new Vector3D(0, 0, -100)));
        var fixedToFloorConstraint = new FixedConstraint(floorAnchor, socketToFloorAnchor);

        // Crank arm
        var arm = Oval.Create(10, 10, 50, 50, false, false, false, false, 200, 50, Matrix44D.Identity);
        scene.AddBody(arm);
        var socketToArmAnchor = new Anchor(socket, Matrix44D.CreateTranslation(new Vector3D(0, 0, 100)));
        var armToSocketAnchor = new Anchor(arm, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
        var rotationAxisConstraint = new RotationAxisConstraint(socketToArmAnchor, armToSocketAnchor, 45.0.ToRadiant(), -720.0.ToRadiant(), +720.0.ToRadiant());
        arm.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));


        // Crank grip
        var grip = Cylinder.Create(10, 50, 50);
        scene.AddBody(grip);
        var armToGrip = new Anchor(arm, Matrix44D.CreateTranslation(new Vector3D(200, 0, +25)));
        var gripToArm = new Anchor(grip, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
        var fixedToArmConstraint = new FixedConstraint(armToGrip, gripToArm);
        grip.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), arm));

        var camera = new Camera()
        {
            Name = "CameraOne",
            NearPlane = 1.0,
            Target = new Position3D(),
        };
        camera.SetCamera(0.0, 90.0, 1500.0);
        scene.AddBody(camera);

        scene.InitScene();
        return scene;
    }
}