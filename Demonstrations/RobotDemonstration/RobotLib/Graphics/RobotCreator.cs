namespace RobotLib.Graphics;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;
using System.Linq;

public static class RobotCreator
{
    public static Robot Create()
    {
        return Create(
            6,
            6,
            4);
    }

    public static Robot Create(
        int socketSegmentCount,
        int wheelSegmentCount,
        int halfWheelSegmentCount)
    {
        return Create(
            socketSegmentCount,
            wheelSegmentCount,
            halfWheelSegmentCount,
            true,
            new Color(1.0, 1.0, 1.0),
            new Color(1.0, 1.0, 1.0),
            new Color(0.0, 0.0, 1.0),
            new Color(0.8, 0.8, 0.8),
            new Color(0.0, 0.0, 1.0),
            new Color(0.8, 0.8, 0.8),
            new Color(0.0, 0.0, 1.0),
            new Color(0.0, 0.0, 1.0),
            new Color(0.8, 0.8, 0.8),
            new Color(0.8, 0.8, 0.8),
            new Color(0.0, 0.0, 0.0),
            new Color(0.4, 0.4, 0.4),
            new Color(0.2, 0.2, 0.2));
    }

    public static Robot Create(
        int socketSegmentCount,
        int wheelSegmentCount,
        int halfWheelSegmentCount,
        bool ovalFaceHasBorder,
        Color faceColor,
        Color edgeColor)
    {
        return Create(
            socketSegmentCount,
            wheelSegmentCount,
            halfWheelSegmentCount,
            ovalFaceHasBorder,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            faceColor,
            edgeColor);
    }

    public static Robot Create(
        int socketSegmentCount,
        int wheelSegmentCount,
        int halfWheelSegmentCount,
        bool ovalFaceHasBorder,
        Color socketFixColor,
        Color socketFlexColor,
        Color shoulderColor,
        Color upperarmColor,
        Color forearmColor,
        Color socketOfUlnaColor,
        Color ulnaColor,
        Color wristSideColor,
        Color wristColor,
        Color adapterColor,
        Color gripperBodyColor,
        Color gripperClampColor,
        Color edgeColor)
    {
        var robot = new Robot();
        var robotSocket = new Anchor(robot, Matrix44D.CreateTranslation(new Vector3D(0, 0, 0)));

        var socketFix = Cylinder.Create(socketSegmentCount, 100, 20, socketFixColor, edgeColor);
        socketFix.Name = "socket-fix";
        var socketFixPlug = new Anchor(socketFix, Matrix44D.CreateTranslation(new Vector3D(0, 0, -10)));
        var socketFixSocket = new Anchor(socketFix, Matrix44D.CreateTranslation(new Vector3D(0, 0, 10)));
        robot.AddChild(socketFix);

        var fixedSocketToRobotConstraint = new FixedConstraint(robotSocket, socketFixPlug);

        var socketFlex = Cylinder.Create(socketSegmentCount, 100, 20, socketFlexColor, edgeColor);
        socketFlex.Name = "socket-flex";
        var socketFlexPlug = new Anchor(socketFlex, Matrix44D.CreateTranslation(new Vector3D(0, 0, -10)));
        var socketFlexSocket = new Anchor(socketFlex, Matrix44D.CreateTranslation(new Vector3D(0, 0, 10)));
        socketFlex.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(socketFlex);

        var rotationSocketFlexBySocketFix = new RotationAxisConstraint(socketFixSocket, socketFlexPlug, 0.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationSocketFlexBySocketFix);

        var shoulder = Oval.Create(halfWheelSegmentCount, 1, 50, 50, ovalFaceHasBorder, ovalFaceHasBorder, false, true, 200, 50, Matrix44D.Identity, shoulderColor, edgeColor);
        shoulder.Name = "shoulder";
        var shoulderPlug = new Anchor(shoulder, Matrix44D.CreateCoordinateSystem(new Position3D(200, 0, 0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0)));
        var shoulderSocket = new Anchor(shoulder, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 25), new Vector3D(-1, 0, 0), new Vector3D(0, 0, -1)));
        shoulder.AddSensor(new CylinderSensor(new Vector3D(1, 0, 0), new Position3D(0, 0, 0)));
        robot.AddChild(shoulder);

        var fixedShoulderToSocketFlexConstraint = new FixedConstraint(socketFlexSocket, shoulderPlug);

        var upperarm = Oval.Create(halfWheelSegmentCount, halfWheelSegmentCount, 50, 25, false, false, false, false, 200, 50, Matrix44D.Identity, upperarmColor, edgeColor);
        upperarm.Name = "upperarm";
        var upperarmPlug = new Anchor(upperarm, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -25), new Vector3D(1, 0, 0), new Vector3D(0, 0, -1)));
        var upperarmSocket = new Anchor(upperarm, Matrix44D.CreateCoordinateSystem(new Position3D(200, 0, -25), new Vector3D(1, 0, 0), new Vector3D(0, 0, -1)));
        upperarm.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(upperarm);

        var rotationUpperarmByShoulder = new RotationAxisConstraint(shoulderSocket, upperarmPlug, 0.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationUpperarmByShoulder);

        var forearm = Oval.Create(halfWheelSegmentCount, 1, 25, 25, false, ovalFaceHasBorder, false, true, 100, 50, Matrix44D.Identity, forearmColor, edgeColor);
        forearm.Name = "forearm";
        var forearmPlug = new Anchor(forearm, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -25), new Vector3D(1, 0, 0), new Vector3D(0, 0, 1)));
        var forearmSocket = new Anchor(forearm, Matrix44D.CreateCoordinateSystem(new Position3D(100, 0, 0), new Vector3D(0, 1, 0), new Vector3D(1, 0, 0)));
        forearm.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(forearm);

        var rotationforearmByUpperarm = new RotationAxisConstraint(upperarmSocket, forearmPlug, 45.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationforearmByUpperarm);

        var socketOfUlna = Cylinder.Create(wheelSegmentCount, 30, 20, socketOfUlnaColor, edgeColor);
        socketOfUlna.Name = "socket-of-ulna";
        var socketOfUlnaPlug = new Anchor(socketOfUlna, Matrix44D.CreateTranslation(new Vector3D(0, 0, -10)));
        var socketOfUlnaSocket = new Anchor(socketOfUlna, Matrix44D.CreateTranslation(new Vector3D(0, 0, 10)));
        socketOfUlna.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(socketOfUlna);

        var rotationUlnaSocketByForearm = new RotationAxisConstraint(forearmSocket, socketOfUlnaPlug, 45.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationUlnaSocketByForearm);

        var ulna = Oval.Create(halfWheelSegmentCount, 1, 20, 20, false, ovalFaceHasBorder, false, true, 25, 30, Matrix44D.Identity, ulnaColor, edgeColor);
        ulna.Name = "ulna";
        var ulnaPlug = new Anchor(ulna, Matrix44D.CreateCoordinateSystem(new Position3D(25, 0, 0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0)));
        var ulnaLeftSocket = new Anchor(ulna, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 15), new Vector3D(1, 0, 0), new Vector3D(0, 0, -1)));
        var ulnaRightSocket = new Anchor(ulna, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -25), new Vector3D(1, 0, 0), new Vector3D(0, 0, -1)));
        robot.AddChild(ulna);

        var ulnaToUlnaSocket = new FixedConstraint(socketOfUlnaSocket, ulnaPlug);

        var wristLeft = Oval.Create(halfWheelSegmentCount, 1, 20, 20, false, ovalFaceHasBorder, false, true, 25, 10, Matrix44D.Identity, wristSideColor, edgeColor);
        wristLeft.Name = "wrist-left";
        var wristLeftPlug = new Anchor(wristLeft, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -5), new Vector3D(-1, 0, 0), new Vector3D(0, 0, -1)));
        var wristLeftSocket = new Anchor(wristLeft, Matrix44D.CreateCoordinateSystem(new Position3D(25, 0, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0)));
        wristLeft.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        robot.AddChild(wristLeft);

        var rotationWristByUlnaLeft = new RotationAxisConstraint(ulnaLeftSocket, wristLeftPlug, 45.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());

        var wristRight = Oval.Create(halfWheelSegmentCount, 1, 20, 20, false, ovalFaceHasBorder, false, true, 25, 10, Matrix44D.Identity, wristSideColor, edgeColor);
        wristRight.Name = "wrist-right";
        var wristRightPlug = new Anchor(wristRight, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, -5), new Vector3D(-1, 0, 0), new Vector3D(0, 0, -1)));
        var wristRightSocket = new Anchor(wristRight, Matrix44D.CreateCoordinateSystem(new Position3D(25, 0, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0)));
        wristRight.AddSensor(new CylinderSensor(new Vector3D(0, 0, -1)));
        robot.AddChild(wristRight);

        var rotationWristByUlnaRight = new RotationAxisConstraint(ulnaRightSocket, wristRightPlug, 45.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationWristByUlnaRight);

        var wrist = Cylinder.Create(halfWheelSegmentCount, 30, 10, wristColor, edgeColor);
        wrist.Name = "wrist";
        var wristPlugLeft = new Anchor(wrist, Matrix44D.CreateTranslation(new Vector3D(-15 - 5, 0, -5)));
        var wristPlugRight = new Anchor(wrist, Matrix44D.CreateTranslation(new Vector3D(15 + 5, 0, -5)));
        var wristSocket = new Anchor(wrist, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 5), new Vector3D(0, -1, 0), new Vector3D(0, 0, 1)));
        wrist.AddSensor(new CylinderSensor(new Vector3D(1, 0, 0), new Position3D(0, 0, -5 - 25)));
        robot.AddChild(wrist);

        var fixedwristToWristLeftConstraint = new FixedConstraint(wristLeftSocket, wristPlugLeft);
        var fixedwristToWristRightConstraint = new FixedConstraint(wristRightSocket, wristPlugRight);

        var adapter = Cylinder.Create(halfWheelSegmentCount, 30, 10, adapterColor, edgeColor);
        adapter.Name = "adapter";
        adapter.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));
        var adapterPlug = new Anchor(adapter, Matrix44D.CreateTranslation(new Vector3D(0, 0, -5)));
        var adapterSocket = new Anchor(adapter, robot.AdapterToolCenterFrame);
        robot.AddChild(adapter);

        var rotationAdapterByWrist = new RotationAxisConstraint(wristSocket, adapterPlug, 10.0.ToRadiant(), -360.0.ToRadiant(), 360.0.ToRadiant());
        robot.AddAxis(rotationAdapterByWrist);

        var gripper = GripperCreator.Create(gripperBodyColor, gripperClampColor, edgeColor);
        var gripperPlug = new Anchor(gripper, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 1), new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0)));
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