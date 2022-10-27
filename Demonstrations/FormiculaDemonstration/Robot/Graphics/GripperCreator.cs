namespace FormiculaDemonstration.Robot.Graphics;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;

public class GripperCreator
{
    public static Gripper Create()
    {
        var gripper = new Gripper(40.0, 80.0);
        gripper.Name = "gripper";
        var gripperSocket = new Anchor(gripper, Matrix44D.Identity);
        var gripperLeftSocket = new Anchor(gripper, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 50), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1)));
        var gripperRightSocket = new Anchor(gripper, Matrix44D.CreateTranslation(new Vector3D(0, 0, 50)));

        var gripperBody = Cuboid.Create(100, 50, 30);

        gripperBody.Name = "gripper-body";
        var gripperBodyPlug = new Anchor(gripperBody, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
        gripper.AddChild(gripperBody);

        var fixedGripperBodyToGripperConstraint = new FixedConstraint(gripperSocket, gripperBodyPlug);

        var clampLeftBody = Cuboid.Create(10, 50, 30);

        clampLeftBody.Name = "clamp-left";
        clampLeftBody.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
        var clampLeftPlug = new Anchor(clampLeftBody, Matrix44D.CreateTranslation(new Vector3D(-5, 0, -25)));
        gripper.AddChild(clampLeftBody);

        var linearClampLeftConstraint = new GripperToClampConstraint(gripperLeftSocket, clampLeftPlug);

        var clampRightBody = Cuboid.Create(10, 50, 30);
        clampRightBody.Name = "clamp-right";
        clampRightBody.AddSensor(new LinearSensor(new Vector3D(1, 0, 0)));
        var clampRightPlug = new Anchor(clampRightBody, Matrix44D.CreateTranslation(new Vector3D(-5, 0, -25)));
        gripper.AddChild(clampRightBody);

        var linearClampRightConstraint = new GripperToClampConstraint(gripperRightSocket, clampRightPlug);

        return gripper;
    }
}