using FormiculaDemonstration.Ant.Antleg;
using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;

namespace FormiculaDemonstration.Ant
{
    public static class AntCreator
    {
        public static Body Create(Color edgeColor)
        {
            var ant = new Body();

            var antBody = Cuboid.Create(200, 50, 400, edgeColor);
            antBody.Frame = Matrix44D.CreateTranslation(new Vector3D(0.0, 0, 485.705078125));
            antBody.Name = "ant body";
            antBody.AddSensor(new PlaneSensor("left mouse button", new Vector3D(0, 1, 0)));
            antBody.AddSensor(new PlaneSensor("left mouse button and shift key", new Vector3D(0, 0, 1)));
            antBody.AddSensor(new SphereSensor("left mouse button and control key", new Position3D(0, 0, 25), 10.ToRadiant()));
            ant.AddChild(antBody);

            var robot1 = AntRobotCreator.Create(edgeColor);
            robot1.Name = "robot 1";
            robot1.Frame = Matrix44D.CreateTranslation(new Vector3D(-400, 0, 0));
            var fixedRobotToFloorConstraint1 = new FixedConstraint(
                new Anchor(ant, Matrix44D.CreateTranslation(new Vector3D(-400, 0, 0))),
                new Anchor(robot1, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
            ant.AddChild(robot1);

            var robot2 = AntRobotCreator.Create(edgeColor);
            robot2.Name = "robot 2";
            robot2.Frame = Matrix44D.CreateTranslation(new Vector3D(-400, -200, 0));
            var fixedRobotToFloorConstraint2 = new FixedConstraint(
                new Anchor(ant, Matrix44D.CreateCoordinateSystem(new Position3D(400, 0, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1))),
                new Anchor(robot2, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
            ant.AddChild(robot2);

            var robot3 = AntRobotCreator.Create(edgeColor);
            robot3.Name = "robot 3";
            robot3.Frame = Matrix44D.CreateTranslation(new Vector3D(-400, -200, 0));
            var fixedRobotToFloorConstraint3 = new FixedConstraint(
                new Anchor(ant, Matrix44D.CreateTranslation(new Vector3D(-400, -200, 0))),
                new Anchor(robot3, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
            ant.AddChild(robot3);

            var robot4 = AntRobotCreator.Create(edgeColor);
            robot4.Name = "robot 4";
            robot4.Frame = Matrix44D.CreateCoordinateSystem(new Position3D(400, -200, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1));
            var fixedRobotToFloorConstraint4 = new FixedConstraint(
                new Anchor(ant, Matrix44D.CreateCoordinateSystem(new Position3D(400, -200, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1))),
                new Anchor(robot4, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
            ant.AddChild(robot4);


            var robot5 = AntRobotCreator.Create(edgeColor);
            robot5.Name = "robot 5";
            robot5.Frame = Matrix44D.CreateTranslation(new Vector3D(-400, 200, 0));
            var fixedRobotToFloorConstraint5 = new FixedConstraint(
                new Anchor(ant, Matrix44D.CreateTranslation(new Vector3D(-400, 200, 0))),
                new Anchor(robot5, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
            ant.AddChild(robot5);

            var robot6 = AntRobotCreator.Create(edgeColor);
            robot6.Name = "robot 6";
            robot6.Frame = Matrix44D.CreateCoordinateSystem(new Position3D(400, 200, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1));
            var fixedRobotToFloorConstraint6 = new FixedConstraint(
                new Anchor(ant, Matrix44D.CreateCoordinateSystem(new Position3D(400, 200, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1))),
                new Anchor(robot6, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0))));
            ant.AddChild(robot6);

            var fixedRobot1ToAntBody = new FixedConstraint(
                new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(-100, 0, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0))),
                new Anchor(robot1.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

            var fixedRobot2ToAntBody = new FixedConstraint(
                new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(100, 0, 0), new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0))),
                new Anchor(robot2.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

            var fixedRobot3ToAntBody = new FixedConstraint(
                new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(-100, -100, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0))),
                new Anchor(robot3.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

            var fixedRobot4ToAntBody = new FixedConstraint(
                new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(100, -100, 0), new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0))),
                new Anchor(robot4.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

            var fixedRobot5ToAntBody = new FixedConstraint(
                new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(-100, 100, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0))),
                new Anchor(robot5.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

            var fixedRobot6ToAntBody = new FixedConstraint(
                new Anchor(antBody, Matrix44D.CreateCoordinateSystem(new Position3D(100, 100, 0), new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0))),
                new Anchor(robot6.Gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50))));

            return ant;
        }
    }
}
