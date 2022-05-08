using Kinematicula.Mathematics;
using static System.Math;

namespace CrankShaftDemonstration.PistonMotor
{
    public static class MotorService
    {
        public static (double ShaftAngle, double PistonAngle, double PistonPosition)
            GetAxesForWheelAngle(double wheelAngle, double wheelRadius, double shaftLength)
        {
            var angle = wheelAngle.Modulo2Pi();
            var angleDeg=angle.ToDegree();
            var sign = angle > PI ? -1.0 : 1.0;

            var dist = wheelRadius * Sin(angle);
            var alpha = Acos(dist / shaftLength);
            var alphaDeg = alpha.ToDegree();

            var gamma = PI/2.0 - angle;

            var shaftAngle = (alpha - gamma);
            var pistonAngle = /*sign */ (Abs(angle) - Abs(shaftAngle));

            var shaft = shaftAngle.ToDegree();
            var piston = pistonAngle.ToDegree();

            var distanceShort = Sqrt(wheelRadius * wheelRadius - dist * dist);
            var distanceLong = Sqrt(shaftLength * shaftLength - dist * dist);
            var pistonPosition = distanceLong - distanceShort + 100.0 + 100.0;

            return (shaftAngle, pistonAngle, pistonPosition);
        }
    }
}
