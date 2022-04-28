using Kinematicula.Mathematics;
using static System.Math;

namespace CrankShaftDemonstration.PistonMotor
{
    public static class MotorService
    {
        public static (double ShaftAngle, double PistonAlpha) GetAxesForWheelAngle(double wheelAngle, double wheelRadius, double shaftLength)
        {
            var angle = wheelAngle.Modulo2Pi();
            var sign = angle > PI ? -1.0 : 1.0;

            var d = wheelRadius * Sin(angle);
            var alpha = Acos(d / shaftLength);
            var alphaDeg = alpha.ToDegree();

            var gamma = PI/2.0 - wheelAngle;

            var shaftAngle = (alpha - gamma);
            var pistonAngle = sign * (Abs(wheelAngle) - Abs(shaftAngle));

            var shaft = shaftAngle.ToDegree();
            var piston = pistonAngle.ToDegree();


            return (shaftAngle, pistonAngle);
        }
    }
}
