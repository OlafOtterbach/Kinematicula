﻿namespace RobotLib.Graphics;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;

public class GripperCreator
{
    public static Gripper Create()
    {
        return Create(
            new Color(0.0, 0.0, 0.0),
            new Color(0.4, 0.4, 0.4),
            new Color(0.2, 0.2, 0.2));
    }

    public static Gripper Create(
        Color gripperBodyColor,
        Color gripperClampColor,
        Color edgeColor)
    {
        var gripper = new Gripper(40.0, 80.0);
        gripper.Name = "gripper";
        var gripperSocket = new Anchor(gripper, Matrix44D.Identity);
        var gripperLeftSocket = new Anchor(gripper, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 50), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1)));
        var gripperRightSocket = new Anchor(gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50)));

        var gripperBody = Cuboid.Create(100, 50, 30, gripperBodyColor, edgeColor);

        gripperBody.Name = "gripper-body";
        var gripperBodyPlug = new Anchor(gripperBody, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
        gripper.AddChild(gripperBody);

        var fixedGripperBodyToGripperConstraint = new FixedConstraint(gripperSocket, gripperBodyPlug);

        var clampLeftBody = Cuboid.Create(10, 50, 30, gripperClampColor, edgeColor);

        clampLeftBody.Name = "clamp-left";
        clampLeftBody.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
        var clampLeftPlug = new Anchor(clampLeftBody, Matrix44D.CreateTranslation(new Vector3D(-5, 0, -25)));
        gripper.AddChild(clampLeftBody);

        var linearClampLeftConstraint = new GripperToClampConstraint(gripperLeftSocket, clampLeftPlug);

        var clampRightBody = Cuboid.Create(10, 50, 30, gripperClampColor, edgeColor);
        clampRightBody.Name = "clamp-right";
        clampRightBody.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
        var clampRightPlug = new Anchor(clampRightBody, Matrix44D.CreateTranslation(new Vector3D(-5, 0, -25)));
        gripper.AddChild(clampRightBody);

        var linearClampRightConstraint = new GripperToClampConstraint(gripperRightSocket, clampRightPlug);

        return gripper;
    }
}