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

            // floor
            var floor = Floor.Create(10, 100);
            floor.Name = "Floor";
            scene.AddBody(floor);
            var fixedToWorldConstraint = new FixedConstraint(new Anchor(scene.World, Matrix44D.Identity), new Anchor(floor, Matrix44D.Identity));


            // first socket
            var firstSocket = Cube.Create(100);
            firstSocket.Name = "first socket";
            scene.AddBody(firstSocket);

            var fixedFirstSocketToFloor = new FixedConstraint(new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(-450, 0,0))), new Anchor(firstSocket, Matrix44D.CreateTranslation(new Vector3D(0,0,-50))));



            // first shifter
            var firstShifter = Cuboid.Create(500, 100, 100);
            firstShifter.Name = "first shifter";
            firstShifter.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
            scene.AddBody(firstShifter);

            // second shifter
            var secondShifter = Cuboid.Create(500, 100, 100);
            secondShifter.Name = "second shifter";
            secondShifter.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
            scene.AddBody(secondShifter);

            var firstAnchor = new Anchor(firstShifter, Matrix44D.CreateTranslation(new Vector3D(250, 0, 0)));
            var secondAnchor = new Anchor(secondShifter, Matrix44D.CreateTranslation(new Vector3D(-250, 0, 0)));
            var fixedFirstToSecondShifter = new FixedConstraint(firstAnchor, secondAnchor);

            scene.InitScene();
            return scene;
        }
    }
}