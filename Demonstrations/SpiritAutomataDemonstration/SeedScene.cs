using Kinematicula.Scening;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Graphics.Saving;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;

namespace SpiritAutomataDemonstration
{
    public static class SeedScene
    {
        public static Scene CreateAndPopulateScene()
        {
            var scene = new Scene(new DirectForwardConstraintSolver(), new DirectInverseConstraintSolver(), new SnapshotFactory(new StateFactory()));

            // floor
            var floor = Floor.Create(12, 100);
            floor.Name = "Floor";
            scene.AddBody(floor);
            var fixedToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));

            // first pillar
            var firstPillar = Cuboid.Create(100, 500, 100);
            firstPillar.Name = "first pillar";
            scene.AddBody(firstPillar);
            var floorToFirstPillarFixConstraint
                = new FixedConstraint(new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(-500 - 50, 550, 0))),
                                      new Anchor(firstPillar, Matrix44D.CreateTranslation(new Vector3D(0, 0, -250))));

            // second pillar
            var secondPillar = Cuboid.Create(100, 500, 100);
            secondPillar.Name = "second pillar";
            scene.AddBody(secondPillar);
            var floorToSecondPillarFixConstraint
                = new FixedConstraint(new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(500 + 50, 550, 0))),
                                      new Anchor(secondPillar, Matrix44D.CreateTranslation(new Vector3D(0, 0, -250))));

            // third pillar
            var thirdPillar = Cuboid.Create(100, 500, 100);
            thirdPillar.Name = "third pillar";
            scene.AddBody(thirdPillar);
            var floorToThirdPillarFixConstraint
                = new FixedConstraint(new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(-500 - 50, -550, 0))),
                                      new Anchor(thirdPillar, Matrix44D.CreateTranslation(new Vector3D(0, 0, -250))));

            // fourth pillar
            var fourthPillar = Cuboid.Create(100, 500, 100);
            fourthPillar.Name = "fouth pillar";
            scene.AddBody(fourthPillar);
            var floorToFourthPillarFixConstraint
                = new FixedConstraint(new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(500 + 50, -550, 0))),
                                      new Anchor(fourthPillar, Matrix44D.CreateTranslation(new Vector3D(0, 0, -250))));



            // left rail
            var leftRail = Cuboid.Create(100, 100, 1200);
            leftRail.Name = "left rail";
            scene.AddBody(leftRail);
            var firstPillarToLeftRailFixConstraint
                = new FixedConstraint(new Anchor(firstPillar, Matrix44D.CreateTranslation(new Vector3D(0, 0, 250))),
                                      new Anchor(leftRail, Matrix44D.CreateTranslation(new Vector3D(0, 550, -50))));

            // right rail
            var rightRail = Cuboid.Create(100, 100, 1200);
            rightRail.Name = "right rail";
            scene.AddBody(rightRail);
            var secondPillarToRightRailFixConstraint
                = new FixedConstraint(new Anchor(secondPillar, Matrix44D.CreateTranslation(new Vector3D(0, 0, 250))),
                                      new Anchor(rightRail, Matrix44D.CreateTranslation(new Vector3D(0, 550, -50))));


            // waggon rail
            var waggonRail = Cuboid.Create(1200, 100, 100);
            waggonRail.Name = "waggon rail";
            waggonRail.AddSensor(new LinearSensor(new Vector3D(0, 1, 0)));
            scene.AddBody(waggonRail);

            var leftRailToWaggonRaillinearConstraint = new LinearAxisConstraint(
                new Anchor(leftRail, Matrix44D.CreateCoordinateSystem(new Position3D(0,550,50), new Vector3D(0,-1,0), new Vector3D(0,0,1))),
                new Anchor(waggonRail, Matrix44D.CreateCoordinateSystem(new Position3D(550, 0, -50), new Vector3D(0, 1, 0), new Vector3D(0, 0, 1))), 0,0,1000);

            // waggon
            var waggon = Cuboid.Create(100, 100, 100);
            waggon.Name = "rail";
            waggon.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
            scene.AddBody(waggon);
            var waggonRailToWaggonlinearConstraint = new LinearAxisConstraint(
                new Anchor(waggonRail, Matrix44D.CreateCoordinateSystem(new Position3D(0, 50, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1))),
                new Anchor(waggon, Matrix44D.CreateCoordinateSystem(new Position3D(0, 50, 0), new Vector3D(1, 0, 0), new Vector3D(0, 0, 1))), 100, -450, 450);

            // bottle



            //// Crank socket
            //var socket = Cylinder.Create(10, 50, 200);
            //scene.AddBody(socket);
            //var floorAnchor = new Anchor(floor, Matrix44D.Identity);
            //var socketToFloorAnchor = new Anchor(socket, Matrix44D.CreateTranslation(new Vector3D(0, 0, -100)));
            //var fixedToFloorConstraint = new FixedConstraint(floorAnchor, socketToFloorAnchor);

            //// Crank arm
            //var arm = Oval.Create(10, 10, 50, 50, false, false, false, false, 200, 50, Matrix44D.Identity);
            //scene.AddBody(arm);
            //var socketToArmAnchor = new Anchor(socket, Matrix44D.CreateTranslation(new Vector3D(0, 0, 100)));
            //var armToSocketAnchor = new Anchor(arm, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
            //var rotationAxisConstraint = new RotationAxisConstraint(socketToArmAnchor, armToSocketAnchor, 0.0.DegToRad(), -720.0.DegToRad(), +720.0.DegToRad());
            //arm.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));

            //// Crank grip
            //var grip = Cylinder.Create(10, 50, 50);
            //scene.AddBody(grip);
            //var armToGrip = new Anchor(arm, Matrix44D.CreateTranslation(new Vector3D(200, 0, +25)));
            //var gripToArm = new Anchor(grip, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
            //var fixedToArmConstraint = new FixedConstraint(armToGrip, gripToArm);
            //grip.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), arm));

            scene.InitScene();
            return scene;
        }
    }
}