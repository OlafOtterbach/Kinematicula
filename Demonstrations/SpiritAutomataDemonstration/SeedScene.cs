using Kinematicula.Scening;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Graphics.Saving;
using Kinematicula.Kinematics.DirectForwardSolving;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using SpiritAutomataDemonstration.Constraints;

namespace SpiritAutomataDemonstration
{
    public static class SeedScene
    {
        public static Scene CreateAndPopulateScene()
        {
            var scene = new Scene(new DirectForwardConstraintSolver(), new DirectInverseConstraintSolver(), new SnapshotFactory(new StateFactory()));

            // floor
            var floor = Floor.Create(12, 100);
            floor.Name = "floor";
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

            var leftRailToWaggonRaillinearConstraint = new TelescopeLinearAxisConstraint(
                new Anchor(leftRail, Matrix44D.CreateCoordinateSystem(new Position3D(0, 550, 50), new Vector3D(0, -1, 0), new Vector3D(0, 0, 1))),
                new Anchor(waggonRail, Matrix44D.CreateCoordinateSystem(new Position3D(550, 0, -50), new Vector3D(0, 1, 0), new Vector3D(0, 0, 1))), 0, 0, 1000);

            // waggon
            var waggon = Cuboid.Create(100, 100, 100);
            waggon.Name = "rail";
            waggon.AddSensor(new PlaneSensor(new Vector3D(0, 0, 1)));
            scene.AddBody(waggon);
            var waggonRailToWaggonlinearConstraint = new TelescopeLinearAxisConstraint(
                new Anchor(waggonRail, Matrix44D.CreateCoordinateSystem(new Position3D(0, 50, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1))),
                new Anchor(waggon, Matrix44D.CreateCoordinateSystem(new Position3D(0, 50, 0), new Vector3D(1, 0, 0), new Vector3D(0, 0, 1))), 100, -450, 450);

            // bottle
            var borderFlags = new bool[] { true, true, true };
            var facetsFlags = new bool[] { false, false, true };
            var segment1 = new double[] { 50, 0, 52, 5, 50, 10, 50, 50, 55, 70, 65, 80, 80, 120, 100, 150 };
            var segment2 = new double[] { 100, 150, 100, 600 };
            var segment3 = new double[] { 100, 600, 0, 600 };
            var segments = new double[][] { segment1, segment2, segment3 };
            var bottle = RotationBody.Create(8, segments, borderFlags, facetsFlags);
            bottle.Name = "bottle";
            bottle.AddSensor(new PlaneSensor(new Vector3D(0, 0, 1)));
            scene.AddBody(bottle);

            var fixedConstraint = new FixedConstraint(
                new Anchor(waggon, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))),
                new Anchor(bottle, Matrix44D.Identity));

            //// crank socket y
            //var crankSocketY = Cylinder.Create(20, 200, 50);
            //crankSocketY.Name = "crankSocket y";
            //crankSocketY.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
            //var floorAnchorY = new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(-300, -1000, 0)));
            //var crankSocketYToFloorAnchor = new Anchor(crankSocketY, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0)));
            //var rotationAxisYConstraint = new RotationAxisConstraint(floorAnchorY, crankSocketYToFloorAnchor, 0.0.DegToRad(), 0.0.DegToRad(), 720.0.DegToRad());
            //scene.AddBody(crankSocketY);

            //// Crank grip y
            //var gripY = Cylinder.Create(10, 80, 100);
            //gripY.Name = "grip y";
            //scene.AddBody(gripY);
            //var socketToGripY = new Anchor(crankSocketY, Matrix44D.CreateTranslation(new Vector3D(120, 0, 25)));
            //var gripYToSocket = new Anchor(gripY, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50)));
            //var fixedToArmYConstraint = new FixedConstraint(socketToGripY, gripYToSocket);
            //gripY.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), crankSocketY));


            //// crank socket x
            //var crankSocketX = Cylinder.Create(20, 200, 50);
            //crankSocketX.Name = "crankSocket x";
            //crankSocketX.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
            //var floorAnchorX = new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(300, -1000, 0)));
            //var crankSocketXToFloorAnchor = new Anchor(crankSocketX, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0)));
            //var rotationAxisXConstraint = new RotationAxisConstraint(floorAnchorX, crankSocketXToFloorAnchor, 0.0.DegToRad(), 0.0.DegToRad(), 720.0.DegToRad());
            //scene.AddBody(crankSocketX);

            //// Crank grip x
            //var gripX = Cylinder.Create(10, 80, 100);
            //gripY.Name = "grip x";
            //scene.AddBody(gripX);
            //var socketToGripX = new Anchor(crankSocketX, Matrix44D.CreateTranslation(new Vector3D(120, 0, 25)));
            //var gripXToSocket = new Anchor(gripX, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50)));
            //var fixedToArmXConstraint = new FixedConstraint(socketToGripX, gripXToSocket);
            //gripX.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1), crankSocketX));

            //var controlYConstraint = new RotationToLinearConstraint(new Anchor(crankSocketY, Matrix44D.Identity), new Anchor(waggonRail, Matrix44D.Identity));

            //scene.AddForwardSolver(new RotationToLinearForwardSolver());
            //scene.AddInverseSolver(new RotationToLinearInverseSolver());
            scene.InitScene();
            return scene;
        }
    }
}