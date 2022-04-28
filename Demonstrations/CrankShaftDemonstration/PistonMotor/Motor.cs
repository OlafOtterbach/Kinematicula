using Kinematicula.Graphics;
using System.Collections.Generic;

namespace CrankShaftDemonstration.PistonMotor
{
    public class Motor : Body
    {
        private Dictionary<int, RotationAxisConstraint> _axes = new Dictionary<int, RotationAxisConstraint>();

        public void AddAxis(RotationAxisConstraint axisConstraint)
        {
            int axisNumber = _axes.Keys.Count + 1;
            _axes[axisNumber] = axisConstraint;
        }

        public void SetAxes(params double[] values)
        {
            for (int number = 1; number <= values.Length; number++)
            {
                if(_axes.ContainsKey(number))
                {
                    _axes[number].Angle = values[number - 1];
                }
            }
        }
    }
}
