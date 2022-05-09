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

            var dist = wheelRadius * Sin(angle);
            var alpha = Acos(dist / shaftLength);
            var gamma = PI/2.0 - angle;

            var shaftAngle = (alpha - gamma);
            var pistonAngle = (Abs(angle) - Abs(shaftAngle));

            var distanceSign = (angle > PI / 2.0) && (angle < 3.0 * PI / 2.0) ? -1.0 : 1.0;
            var distanceShort = Sqrt(wheelRadius * wheelRadius - dist * dist);
            var distanceLong = Sqrt(shaftLength * shaftLength - dist * dist);
            var pistonPosition = distanceLong - distanceSign * distanceShort + 100.0 + 100.0;

            return (shaftAngle, pistonAngle, pistonPosition);
        }
    }
}
