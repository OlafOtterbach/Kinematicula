using System.Collections.Generic;
using System.Linq;

namespace Kinematicula.Kinematics.UniversalSolving.Sequences
{
    public class AxisSequencer
    {
        public IEnumerable<Axis[]> GetSequences(Axis[] originalAxes, double deltaRotation, double deltaLinear)
        {
            var iterations = GetIteration(originalAxes.Length);
            foreach (var iteration in iterations)
            {
                var axes = originalAxes
                           .Select((axis, i) => new Axis(axis.AxisType,
                                                         axis.Value + iteration[i] * (axis.AxisType == AxisType.RotationAxis ? deltaRotation : deltaLinear)))
                           .ToArray();
                yield return axes;
            }
        }

        private IEnumerable<int[]> GetIteration(int count)
        {
            var maximum = (int)System.Math.Pow(5, count);
            var result = Enumerable.Range(0, maximum).Select(i => Convert(i, count)).ToList();

            return result;
        }

        private int[] Convert(int value, int count)
        {
            var values = new int[count];
            var index = 0;
            while (value > 0)
                value = SetValue(values, value, index++);

            for (int i = index; i < count; i++)
                values[i] = -2;

            return values;
        }

        private int SetValue(int[] values, int value, int index)
        {
            values[index] = (value % 5) - 2;
            value = value / 5;
            return value;
        }
    }
}
