using Kinematicula.Graphics;
using System.Collections.Generic;

namespace PistonEngineDemonstration.PistonMotor
{
    public class Motor : Body
    {
        private Dictionary<int, Constraint> _axes = new Dictionary<int, Constraint>();

        public void AddAxis(Constraint axisConstraint)
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
                    if (_axes[number] is RotationAxisConstraint rotationAxis)
                    {
                        rotationAxis.Angle = values[number - 1];
                    }
                    else if (_axes[number] is LinearAxisConstraint linearAxis)
                    {
                        linearAxis.LinearPosition = values[number - 1];
                    }
                }
            }
        }

        public double GetWheelAxisValue()
        {
            var wheelAxis = _axes[1] as WheelRotationConstraint;
            return wheelAxis.Angle;
        }
    }
}
