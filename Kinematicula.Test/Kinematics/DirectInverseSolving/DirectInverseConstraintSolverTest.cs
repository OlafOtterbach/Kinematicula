using Xunit;
using NSubstitute;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Graphics.Saving;
using Kinematicula.Kinematics;
using Kinematicula.Kinematics.DirectInverseSolving;
using Kinematicula.Mathematics;
using Kinematicula.Scening;

namespace Kinematicula.Tests.Kinematics.DirectInverseSolving
{
    public class DirectInverseConstraintSolverTest
    {
        [Fact]
        public void Test()
        {
            IDirectForwardConstraintSolver forwardSolver = Substitute.For<IDirectForwardConstraintSolver>();
            IDirectInverseConstraintSolver inverseSolver = Substitute.For<IDirectInverseConstraintSolver>();
            ISnapshotFactory snapshotFactory = Substitute.For<ISnapshotFactory>();
            var scene = new Scene(forwardSolver, inverseSolver, snapshotFactory);
            var minimum = 0.0.DegToRad();
            var maximum = 90.0.DegToRad();

            var floor = Floor.Create(10, 5);
            scene.Bodies.Add(floor);
            floor.Name = "Floor";
            var fixedToWorldConstraint = new FixedConstraint(
                new Anchor(scene.World, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))),
                new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
            var cubeA = Cube.Create(2);
            cubeA.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
            cubeA.Name = "A";
            cubeA.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 1));
            scene.Bodies.Add(cubeA);
            var rotationAFloorB = new TelescopeRotationAxisConstraint(
                new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))),
                new Anchor(cubeA, Matrix44D.CreateTranslation(new Vector3D(0, 0, -1.0))),
                minimum,
                maximum);

            var mementorFactory = new StateFactory();

            var solver = new DirectInverseConstraintSolver();

            var snapShotFactory = new SnapshotFactory(mementorFactory);
            var snapShot = snapShotFactory.TakeSnapshot(scene);

            cubeA.Frame = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), 90.0.DegToRad()) * cubeA.Frame;

            // Act
            solver.Solve(cubeA, snapShot);

            // Arrange
        }

        [Fact]
        public void Test1()
        {
            IDirectForwardConstraintSolver forwardSolver = Substitute.For<IDirectForwardConstraintSolver>();
            IDirectInverseConstraintSolver inverseSolver = Substitute.For<IDirectInverseConstraintSolver>();
            ISnapshotFactory snapshotFactory = Substitute.For<ISnapshotFactory>();
            var scene = new Scene(forwardSolver, inverseSolver, snapshotFactory);
            var minimum = -90.0.DegToRad();
            var maximum = 90.0.DegToRad();

            var floor = Floor.Create(10, 5);
            scene.Bodies.Add(floor);
            floor.Name = "Floor";
            var fixedToWorldConstraint = new FixedConstraint(
                new Anchor(scene.World, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))),
                new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
            var cubeA = Cube.Create(2);
            cubeA.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
            cubeA.Name = "A";
            cubeA.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 1))
                          * Matrix44D.CreateRotation(new Vector3D(0, 0, 1), maximum);
            scene.Bodies.Add(cubeA);
            var rotationAFloorB = new TelescopeRotationAxisConstraint(
                new Anchor(floor, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))),
                new Anchor(cubeA, Matrix44D.CreateTranslation(new Vector3D(0, 0, -1.0))),
                minimum,
                maximum);
            var cubeB = Cube.Create(2);
            cubeB.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
            cubeB.Frame = Matrix44D.CreateTranslation(new Vector3D(0, 0, 3))
                          * Matrix44D.CreateRotation(new Vector3D(0, 0, 1), maximum);
            cubeB.Name = "B";
            scene.Bodies.Add(cubeB);
            var rotationAB = new TelescopeRotationAxisConstraint(
                new Anchor(cubeA, Matrix44D.CreateTranslation(new Vector3D(0, 0, 1))),
                new Anchor(cubeB, Matrix44D.CreateTranslation(new Vector3D(0, 0, -1.0))),
                minimum,
                maximum);

            var mementorFactory = new StateFactory();

            var solver = new DirectInverseConstraintSolver();

            var snapShotFactory = new SnapshotFactory(mementorFactory);
            var snapShot = snapShotFactory.TakeSnapshot(scene);

            cubeA.Frame = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), 10.0.DegToRad()) * cubeA.Frame;

            // Act
            solver.Solve(cubeA, snapShot);

            // Arrange
        }
    }
}