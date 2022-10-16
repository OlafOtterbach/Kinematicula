using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using System.Collections.Generic;

namespace FormiculaDemonstration.Ant.AntLeg
{
    public class AntLeg : Body
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
                if (_axes.ContainsKey(number))
                {
                    _axes[number].Angle = values[number - 1];
                }
            }
        }

        public (double angle1, double angle2, double angle3, double angle4) GetAxes()
        {
            var legPartFourAnchor = _axes[3].Second;
            var legPartFour = legPartFourAnchor.Body;
            var B3D = legPartFour.Frame * legPartFourAnchor.ConnectionFrame;


            return (0, 0, 0, 0);
        }
    }
}
