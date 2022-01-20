using Kinematicula.Scening;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Graphics.Saving;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;

namespace CylinderDemonstration
{
    public static class SeedScene
    {
        public static Scene CreateAndPopulateScene()
        {
            var scene = new Scene(new DirectForwardConstraintSolver(), new DirectInverseConstraintSolver(), new SnapshotFactory(new StateFactory()));

            var minimum = -90.0.DegToRad();
            var maximum = 90.0.DegToRad();

            var floor = Floor.Create(10, 100);
//            floor.Color = new Color(0, 1, 0);
            floor.Name = "Floor";
            scene.AddBody(floor);
            var fixedToWorldConstraint = new FixedConstraint(
                new Anchor(scene.World, Matrix44D.Identity),
                new Anchor(floor, Matrix44D.Identity));



            var socket = Cylinder.Create(10, 50, 200);
//            socket.Color = new Color(0, 1, 0);
            scene.AddBody(socket);
            var floorAnchor = new Anchor(floor, Matrix44D.Identity);
            var socketToFloorAnchor = new Anchor(socket, Matrix44D.CreateTranslation(new Vector3D(0, 0, -100)));
            var fixedToFloorConstraint = new FixedConstraint(floorAnchor, socketToFloorAnchor);
            
            var arm = Oval.Create(10, 10, 50, 50, false, false, false, false, 200, 50, Matrix44D.Identity);
//            arm.Color = new Color(0, 1, 0);
            scene.AddBody(arm);
            var socketToArmAnchor = new Anchor(socket, Matrix44D.CreateTranslation(new Vector3D(0, 0, 100)));
            var armToSocketAnchor = new Anchor(arm, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
            var rotationAxisConstraint = new RotationAxisConstraint(socketToArmAnchor, armToSocketAnchor, 0.0.DegToRad(), -720.0.DegToRad(), +720.0.DegToRad());

            arm.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));


            var grip = Cylinder.Create(10, 50, 50);
//            grip.Color = new Color(1, 0, 0);
            scene.AddBody(grip);
            var armToGrip = new Anchor(arm, Matrix44D.CreateTranslation(new Vector3D(200, 0, +25)));
            var gripToArm = new Anchor(grip, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
            var fixedToArmConstraint = new FixedConstraint(armToGrip, gripToArm);

            grip.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), arm));

            scene.InitScene();

            return scene;
        }
    }
}