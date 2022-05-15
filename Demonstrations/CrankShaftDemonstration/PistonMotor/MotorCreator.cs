using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;
using Kinematicula.Scening;

namespace PistonEngineDemonstration.PistonMotor
{
    public class MotorCreator
    {
        public static Motor Create(Scene scene)
        {
            var motor = new Motor();
            motor.Name = "piston motor";

            scene.AddInverseSolver(new WheelRotationInverseSolver());
            scene.AddInverseSolver(new PistonLinearAxisInverseSolver());

            var wheel1 = Cylinder.Create(16, 150, 50);
            wheel1.Name = "wheel 1";
            motor.AddChild(wheel1);
            wheel1.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));

            var floorAnchor = new Anchor(motor, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 0), new Vector3D(0, 0, -1), new Vector3D(1, 0, 0)));
            var wheel1AxisAnchor = new Anchor(wheel1, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25 - 50)));
            var motorToWheel1RotationAxisConstraint = new WheelRotationConstraint(floorAnchor, wheel1AxisAnchor);
            motor.AddAxis(motorToWheel1RotationAxisConstraint);

            var wheel2 = Cylinder.Create(16, 150, 50);
            wheel2.Name = "wheel 2";
            motor.AddChild(wheel2);
            wheel2.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1)));

            var wheelToWheelFixedToWorldConstraint
                = new FixedConstraint(
                    new Anchor(wheel2, Matrix44D.CreateTranslation(new Vector3D(0, 0, 75))),
                    new Anchor(wheel1, Matrix44D.CreateTranslation(new Vector3D(0, 0, -75))));

            var shaft = Oval.Create(10, 10, 50, 50, false, false, false, false, 300, 100, Matrix44D.Identity);
            shaft.Name = "shaft";
            motor.AddChild(shaft);

            var wheel1ShaftAnchor = new Anchor(wheel1, Matrix44D.CreateCoordinateSystem(new Position3D(100, 0, -25), new Vector3D(-1, 0, 0), new Vector3D(0, 0, -1)));
            var shaftWheel1Anchor = new Anchor(shaft, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50)));
            var wheel1ToShaftRotationAxisConstraint = new RotationAxisConstraint(wheel1ShaftAnchor, shaftWheel1Anchor, 67.5.ToRadiant(), double.MinValue, double.MaxValue);
            motor.AddAxis(wheel1ToShaftRotationAxisConstraint);

            var pistonMounter1
             = Oval.Create(10, 1, 50, 50, false, true, false, false, 100, 50, Matrix44D.Identity);
            pistonMounter1.Name = "piston mounter 1";
            motor.AddChild(pistonMounter1);
            var pistonMounter1RotationAnchor = new Anchor(pistonMounter1, Matrix44D.CreateTranslation(new Vector3D(0, 0, -25)));
            var shaftPistonAnchor = new Anchor(shaft, Matrix44D.CreateTranslation(new Vector3D(300, 0, 50)));
            var ShaftToPistonRotationAxisConstraint = new RotationAxisConstraint(shaftPistonAnchor, pistonMounter1RotationAnchor);
            motor.AddAxis(ShaftToPistonRotationAxisConstraint);

            var pistonMounter2
             = Oval.Create(10, 1, 50, 50, false, true, false, false, 100, 50, Matrix44D.Identity);
            pistonMounter2.Name = "piston mounter 2";
            motor.AddChild(pistonMounter2);
            var piston1ToPiston2MounterFixedConstraint
                = new FixedConstraint(new Anchor(pistonMounter1, Matrix44D.CreateTranslation(new Vector3D(0, 0, -200))),
                                      new Anchor(pistonMounter2, Matrix44D.CreateTranslation(new Vector3D(0, 0, -50))));

            var piston = Cylinder.Create(10, 150, 200);
            piston.Name = "piston";
            piston.AddSensor(new LinearSensor(new Vector3D(0, 0, 1)));
            motor.AddChild(piston);
            var pistonConnection
                = new FixedConstraint(new Anchor(pistonMounter2,
                                                 Matrix44D.CreateCoordinateSystem(
                                                     new Position3D(100, 0, 75),
                                                     new Vector3D(0, 0, -1), new Vector3D(1, 0, 0))),
                                      new Anchor(piston, Matrix44D.CreateTranslation(new Vector3D(0, 0, -100))));

            var linearAxis = new PistonLinearAxisConstraint(
                            new Anchor(motor, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 0), new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0))),
                            new Anchor(piston, Matrix44D.CreateCoordinateSystem(new Position3D(0, 0, 0), new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0))),
                            400,
                            400,
                            600);
            motor.AddAxis(linearAxis);

            var wheelAlpha = 0.0.ToRadiant(); // -240.0.ToRadiant();
            var (shaftAlpha, pistonAlpha, pistonPosition) = MotorService.GetAxesForWheelAngle(wheelAlpha, 100, 300);
            motor.SetAxes(wheelAlpha, shaftAlpha, pistonAlpha, pistonPosition);

            return motor;
        }
    }
}
