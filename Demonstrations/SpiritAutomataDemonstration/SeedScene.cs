﻿using Kinematicula.Scening;
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
            waggon.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
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

            // crank socket y
            var crankSocketY = Cylinder.Create(10, 200, 100);
            crankSocketY.Name = "crankSockety";
            crankSocketY.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
            var floorAnchor = new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(0, -1000, 0)));
            var crankSocketYToFloorAnchor = new Anchor(crankSocketY, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0)));
            var rotationAxisYConstraint = new RotationAxisConstraint(floorAnchor, crankSocketYToFloorAnchor, 180.0.DegToRad(), 0.0.DegToRad(), 720.0.DegToRad());
            scene.AddBody(crankSocketY);

            scene.InitScene();
            return scene;
        }
    }
}