using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;

namespace CrankShaftDemonstration.PistonMotor
{
    public class MotorCreator
    {
        public static Motor Create()
        {
            var motor = new Motor();

            var wheel1 = Cylinder.Create(16, 100, 50);
            motor.AddChild(wheel1);
            wheel1.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));

            var floorAnchor = new Anchor(motor, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 200), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0)));
            var wheel1AxisAnchor = new Anchor(wheel1, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25 - 50)));
            var floorToWheel1RotationAxisConstraint = new RotationAxisConstraint(floorAnchor, wheel1AxisAnchor);

            var wheel2 = Cylinder.Create(16, 100, 50);
            motor.AddChild(wheel2);
            wheel2.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));

            var wheelToWheelFixedToWorldConstraint
                = new FixedConstraint(
                    new Anchor(wheel2, Matrix44D.CreateTranslation(new Vector3D(0, 0, 75))),
                    new Anchor(wheel1, Matrix44D.CreateTranslation(new Vector3D(0, 0, -75))));

            var shaft = Oval.Create(10, 10, 50, 50, false, false, false, false, 300, 100, Matrix44D.Identity);
            motor.AddChild(shaft);

            var wheel1ShaftAnchor = new Anchor(wheel1, Matrix44D.CreateCoordinateSystem(new Position3D(50, 0, -25), new Vector3D(-1, 0, 0), new Vector3D(0, 0, -1)));
            var shaftWheel1Anchor = new Anchor(shaft, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50)));
            var wheel1ToShaftRotationAxisConstraint = new RotationAxisConstraint(wheel1ShaftAnchor, shaftWheel1Anchor);

            var pistonMounter1
             = Oval.Create(10, 1, 50, 50, false, true, false, false, 100, 50, Matrix44D.Identity);
            motor.AddChild(pistonMounter1);
            var pistonMounter1RotationAnchor = new Anchor(pistonMounter1, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
            var shaftPistonAnchor = new Anchor(shaft, Matrix44D.CreateTranslation(new Vector3D(300, 0, 50)));
            var ShaftToPistonRotationAxisConstraint = new RotationAxisConstraint(shaftPistonAnchor, pistonMounter1RotationAnchor);

            var pistonMounter2
             = Oval.Create(10, 1, 50, 50, false, true, false, false, 100, 50, Matrix44D.Identity);
            motor.AddChild(pistonMounter2);
            var piston1ToPiston2MounterFixedConstraint
                = new FixedConstraint(new Anchor(pistonMounter1, Matrix44D.CreateTranslation(new Vector3D(0, 0, -200))),
                                      new Anchor(pistonMounter2, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))));

            var piston = Cylinder.Create(10, 150, 200);
            motor.AddChild(piston);
            var pistonConnection
                = new FixedConstraint(new Anchor(pistonMounter2,
                                                 Matrix44D.CreateCoordinateSystem(
                                                     new Position3D(100, 0, 75),
                                                     new Vector3D(0, 0, -1), new Vector3D(1, 0, 0))),
                                      new Anchor(piston, Matrix44D.CreateTranslation(new Vector3D(0, 0, -100))));

            return motor;
        }
    }
}
