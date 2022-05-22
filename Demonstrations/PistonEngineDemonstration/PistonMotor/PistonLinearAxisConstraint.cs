using Kinematicula.Graphics;

namespace PistonEngineDemonstration.PistonMotor
{
    public class PistonLinearAxisConstraint : LinearAxisConstraint
    {
        public PistonLinearAxisConstraint(Anchor first, Anchor second, double position, double minimum, double maximum)
            : base(first, second, position, minimum, maximum)
        {
        }
    }
}
