using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using Xunit;

namespace Kinematicula.Tests.Mathematics.Extensions
{
    public class Vector3DExtensionsTest
    {
        [Fact]
        public void CounterClockwiseAngleWithTest1()
        {
            var first = new Vector3D(1, 0, 0);
            var second = new Vector3D(1, 1, 0);
            var axis = new Vector3D(0, 0, 1);

            var angle = first.CounterClockwiseAngleWith(second, axis).RadToDeg();

            Assert.Equal(45.0, angle, 3);
        }

        [Fact]
        public void CounterClockwiseAngleWithTest2()
        {
            var first = new Vector3D(1, 0, 0);
            var second = new Vector3D(1, -1, 0);
            var axis = new Vector3D(0, 0, 1);

            var angle = first.CounterClockwiseAngleWith(second, axis).RadToDeg();

            Assert.Equal(315.0, angle, 3);
        }

        [Fact]
        public void CounterClockwiseAngleWithTest3()
        {
            var first = new Vector3D(1, 0, 0);
            var second = new Vector3D(0, -1, 0);
            var axis = new Vector3D(0, 0, 1);

            var angle = first.CounterClockwiseAngleWith(second, axis).RadToDeg();

            Assert.Equal(270.0, angle, 3);
        }
    }
}
