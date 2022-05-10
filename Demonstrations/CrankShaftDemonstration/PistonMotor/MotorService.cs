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
            var gamma = PI / 2.0 - angle;

            var shaftAngle = (alpha - gamma);
            var pistonAngle = (Abs(angle) - Abs(shaftAngle));

            var distanceSign = (angle > PI / 2.0) && (angle < 3.0 * PI / 2.0) ? -1.0 : 1.0;
            var distanceShort = Sqrt(wheelRadius * wheelRadius - dist * dist);
            var distanceLong = Sqrt(shaftLength * shaftLength - dist * dist);
            var pistonPosition = distanceLong - distanceSign * distanceShort + 100.0 + 100.0;

            var wheelAngleDeg = wheelAngle.ToDegree();
            var shaftAngleDeg = shaftAngle.ToDegree();
            var pistonAngleDeg = pistonAngle.ToDegree();

            var (a, b, c, d) = GetAxesForPistonPosition(pistonPosition, wheelRadius, shaftLength);

            return (shaftAngle, pistonAngle, pistonPosition);
        }

        public static (double wheelAngle, double ShaftAngle, double PistonAngle, double pistonPosition) GetAxesForPistonPosition(double pistonLinearPosition, double wheelRadius, double shaftLength)
        {
            var pistonPosition = pistonLinearPosition - 100.0 - 100.0;

            if (pistonPosition > wheelRadius + shaftLength)
                pistonPosition = wheelRadius + shaftLength;

            if (pistonPosition < 200.0)
                pistonPosition = 0.0;

            var (alpha, beta, gamma) = GetAngles(wheelRadius, shaftLength, pistonPosition);
            var wheelAngle = PI - beta.Modulo2Pi();
            var shaftAngle = gamma.Modulo2Pi();
            var pistonAngle = (Abs(wheelAngle) - Abs(shaftAngle));

            var alphaDegree = alpha.ToDegree();
            var betaDegree = beta.ToDegree();
            var gammaDegree = gamma.ToDegree();
            var wheelAngleDeg = wheelAngle.ToDegree();
            var shaftAngleDeg = shaftAngle.ToDegree();
            var pistonAngleDeg = pistonAngle.ToDegree();

            return (wheelAngle, shaftAngle, pistonAngle, pistonPosition + 200);
        }

        public static (double alpha, double beta, double gamma) GetAngles(double a, double b, double c)
        {
            var alpha = Acos((a * a - b * b - c * c) / (-2.0 * b * c));
            var beta = Acos((b * b - a * a - c * c) / (-2.0 * a * c));
            var gamma = PI - alpha - beta;
            if (gamma < 0.0) gamma = 0.0;

            return (alpha, beta, gamma);
        }
    }
}
