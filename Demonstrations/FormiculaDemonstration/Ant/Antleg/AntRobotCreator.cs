namespace FormiculaDemonstration.Ant.Antleg;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;
using RobotLib.Graphics;
using System.Linq;

public static class AntRobotCreator
{
    public static Robot Create(Color edgeColor)
    {
        const int SEGMENTS1 = 2;
        const int SEGMENTS2 = 3;

        var robot = new Robot();
        var robotSocket = new Anchor(robot, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0)));

        var socketFix = Cuboid.Create(100,20,100, edgeColor);
        socketFix.Name = "socket-fix";
        var socketFixPlug = new Anchor(socketFix, Matrix44D.CreateTranslation(new Vector3D(0, 0, -10)));
        var socketFixSocket = new Anchor(socketFix, Matrix44D.CreateTranslation(new Vector3D(0, 0, 10)));
        robot.AddChild(socketFix);

        var fixedSocketToRobotConstraint = new FixedConstraint(robotSocket, socketFixPlug);

        var socketFlex = Cuboid.Create(100, 20, 100, edgeColor);
        socketFlex.Name = "socket-flex";
        var socketFlexPlug = new Anchor(socketFlex, Matrix44D.CreateTranslation(new Vector3D(0, 0, -10)));
        var socketFlexSocket = new Anchor(socketFlex, Matrix44D.CreateTranslation(new Vector3D(0, 0, 10)));
        socketFlex.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(socketFlex);

        var rotationSocketFlexBySocketFix = new RotationAxisConstraint(socketFixSocket, socketFlexPlug, 0.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationSocketFlexBySocketFix);

        var shoulder = AntSegment.Create(200, 50, 50, edgeColor);
        shoulder.Name = "shoulder";
        var shoulderPlug = new Anchor(shoulder, Matrix44D.CreateCoordinateSystem(new Position3D(200, 0, 0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0)));
        var shoulderSocket = new Anchor(shoulder, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 25), new Vector3D(-1, 0, 0), new Vector3D(0, 0, -1)));
        shoulder.AddSensor(new CylinderSensor(new Vector3D(1, 0, 0), new Position3D(0, 0, 0)));
        robot.AddChild(shoulder);

        var fixedShoulderToSocketFlexConstraint = new FixedConstraint(socketFlexSocket, shoulderPlug);

        var upperarm = AntSegment.Create(200, 50, 50, edgeColor);
        upperarm.Name = "upperarm";
        var upperarmPlug = new Anchor(upperarm, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -25), new Vector3D(1, 0, 0), new Vector3D(0, 0, -1)));
        var upperarmSocket = new Anchor(upperarm, Matrix44D.CreateCoordinateSystem(new Position3D(200, 0, -25), new Vector3D(1, 0, 0), new Vector3D(0, 0, -1)));
        upperarm.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(upperarm);

        var rotationUpperarmByShoulder = new RotationAxisConstraint(shoulderSocket, upperarmPlug, 0.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationUpperarmByShoulder);

        var forearm = AntSegment.Create(100, 50, 50, edgeColor);
        forearm.Name = "forearm";
        var forearmPlug = new Anchor(forearm, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -25), new Vector3D(1, 0, 0), new Vector3D(0, 0, 1)));
        var forearmSocket = new Anchor(forearm, Matrix44D.CreateCoordinateSystem(new Position3D(100, 0, 0), new Vector3D(0, 1, 0), new Vector3D(1, 0, 0)));
        forearm.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(forearm);

        var rotationforearmByUpperarm = new RotationAxisConstraint(upperarmSocket, forearmPlug, 45.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationforearmByUpperarm);

        var socketOfUlna = Cuboid.Create(50, 20, 50, edgeColor);
        socketOfUlna.Name = "socket-of-ulna";
        var socketOfUlnaPlug = new Anchor(socketOfUlna, Matrix44D.CreateTranslation(new Vector3D(0, 0, -10)));
        var socketOfUlnaSocket = new Anchor(socketOfUlna, Matrix44D.CreateTranslation(new Vector3D(0, 0, 10)));
        socketOfUlna.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(socketOfUlna);

        var rotationUlnaSocketByForearm = new RotationAxisConstraint(forearmSocket, socketOfUlnaPlug, 45.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationUlnaSocketByForearm);

        var ulna = AntSegment.Create(25, 30, 30, edgeColor);
        ulna.Name = "ulna";
        var ulnaPlug = new Anchor(ulna, Matrix44D.CreateCoordinateSystem(new Position3D(25, 0, 0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0)));
        var ulnaLeftSocket = new Anchor(ulna, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 15), new Vector3D(1, 0, 0), new Vector3D(0, 0, -1)));
        var ulnaRightSocket = new Anchor(ulna, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -25), new Vector3D(1, 0, 0), new Vector3D(0, 0, -1)));
        robot.AddChild(ulna);

        var ulnaToUlnaSocket = new FixedConstraint(socketOfUlnaSocket, ulnaPlug);

        var wristLeft = AntSegment.Create(25, 10, 20, edgeColor);
        wristLeft.Name = "wrist-left";
        var wristLeftPlug = new Anchor(wristLeft, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -5), new Vector3D(-1, 0, 0), new Vector3D(0, 0, -1)));
        var wristLeftSocket = new Anchor(wristLeft, Matrix44D.CreateCoordinateSystem(new Position3D(25, 0, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0)));
        wristLeft.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(wristLeft);

        var rotationWristByUlnaLeft = new RotationAxisConstraint(ulnaLeftSocket, wristLeftPlug, 45.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());

        var wristRight = AntSegment.Create(25, 10, 20, edgeColor);
        wristRight.Name = "wrist-right";
        var wristRightPlug = new Anchor(wristRight, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -5), new Vector3D(-1, 0, 0), new Vector3D(0, 0, -1)));
        var wristRightSocket = new Anchor(wristRight, Matrix44D.CreateCoordinateSystem(new Position3D(25, 0, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0)));
        wristRight.AddSensor(new CylinderSensor(new Vector3D(0, 0, -1)));
        robot.AddChild(wristRight);

        var rotationWristByUlnaRight = new RotationAxisConstraint(ulnaRightSocket, wristRightPlug, 45.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationWristByUlnaRight);

        var wrist = Cuboid.Create(50, 10, 50, edgeColor);
        wrist.Name = "wrist";
        var wristPlugLeft = new Anchor(wrist, Matrix44D.CreateTranslation(new Vector3D(-15 - 5, 0, -5)));
        var wristPlugRight = new Anchor(wrist, Matrix44D.CreateTranslation(new Vector3D(15 + 5, 0, -5)));
        var wristSocket = new Anchor(wrist, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 5), new Vector3D(0, -1, 0), new Vector3D(0, 0, 1)));
        wrist.AddSensor(new CylinderSensor(new Vector3D(1, 0, 0), new Position3D(0, 0, -5 - 25)));
        robot.AddChild(wrist);

        var fixedwristToWristLeftConstraint = new FixedConstraint(wristLeftSocket, wristPlugLeft);
        var fixedwristToWristRightConstraint = new FixedConstraint(wristRightSocket, wristPlugRight);

        var adapter = Cuboid.Create(50, 10, 50, edgeColor);
        adapter.Name = "adapter";
        adapter.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        var adapterPlug = new Anchor(adapter, Matrix44D.CreateTranslation(new Vector3D(0, 0, -5)));
        var adapterSocket = new Anchor(adapter, robot.AdapterToolCenterFrame);
        robot.AddChild(adapter);

        var rotationAdapterByWrist = new RotationAxisConstraint(wristSocket, adapterPlug, 10.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationAdapterByWrist);

        var gripper = AntThightCreator.Create(edgeColor);
        var gripperPlug = new Anchor(gripper, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 0), new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0)));
        robot.AddChild(gripper);
        gripper.Children.First().AddSensor(new PlaneSensor("left mouse button", new Vector3D(0, 1, 0), socketFlex));
        gripper.Children.First().AddSensor(new CylinderSensor("left mouse button and shift key", new Vector3D(0, 0, 1), robot));
        gripper.Children.First().AddSensor(new SphereSensor("left mouse button and control key", new Position3D(0, 0, 25), 10.ToRadiant()));

        var gripperToAdapter = new RobotConstraint(adapterSocket, gripperPlug);

        robot.SetAxisAngle(1, 0.ToRadiant());
        robot.SetAxisAngle(2, 30.ToRadiant());
        robot.SetAxisAngle(3, 30.ToRadiant());
        robot.SetAxisAngle(4, 0.ToRadiant());
        robot.SetAxisAngle(5, 30.ToRadiant());
        robot.SetAxisAngle(6, 0.ToRadiant());
        return robot;
    }
}