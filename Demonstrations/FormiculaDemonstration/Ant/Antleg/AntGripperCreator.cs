namespace FormiculaDemonstration.Ant.Antleg;

using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;
using RobotLib.Graphics;

public class AntGripperCreator
{
    public static Gripper Create()
    {
        var gripper = new Gripper(50.0, 80.0);
        gripper.Name = "gripper";
        var gripperSocket = new Anchor(gripper, Matrix44D.Identity);

        var gripperBody = Cuboid.Create(100, 50, 30);
        gripperBody.Name = "gripper-body";
        var gripperBodyPlug = new Anchor(gripperBody, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
        gripper.AddChild(gripperBody);

        var fixedGripperBodyToGripperConstraint = new FixedConstraint(gripperSocket, gripperBodyPlug);

        return gripper;
    }
}
