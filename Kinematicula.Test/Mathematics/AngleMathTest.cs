using Xunit;
using Kinematicula.Mathematics;
using System;

namespace Kinematicula.Tests.Mathematics
{
    public class AngleMathTest
    {
        [Fact]
        public void ToCounterAngleTest1()
        {
            Assert.Equal(350.0.DegToRad(), 10.0.DegToRad().ToCounterAngle(), 3);
        }

        [Fact]
        public void ToCounterAngleTest2()
        {
            Assert.Equal(10.0.DegToRad(), 350.0.DegToRad().ToCounterAngle(), 3);
        }

        [Fact]
        public void ToDistanceTest1()
        {
            Assert.Equal(10.0.DegToRad(), 10.0.DegToRad().DistantAngleTo(20.0.DegToRad()), 3);
        }

        [Fact]
        public void ToDistanceTest2()
        {
            Assert.Equal(10.0.DegToRad(), 20.0.DegToRad().DistantAngleTo(10.0.DegToRad()), 3);
        }

        [Fact]
        public void ToDistanceTest3()
        {
            Assert.Equal(10.0.DegToRad(), 5.0.DegToRad().DistantAngleTo(355.0.DegToRad()), 3);
        }

        [Fact]
        public void ToDistanceTest4()
        {
            Assert.Equal(10.0.DegToRad(), 355.0.DegToRad().DistantAngleTo(5.0.DegToRad()), 3);
        }

        [Fact]
        public void DegToRadTest1()
        {
            Assert.Equal(0.0, 0.0.DegToRad(), 4);
        }

        [Fact]
        public void DegToRadTest2()
        {
            Assert.Equal(Math.PI / 2.0, 90.0.DegToRad(), 4);
        }

        [Fact]
        public void DegToRadTest3()
        {
            Assert.Equal(Math.PI, 180.0.DegToRad(), 4);
        }

        [Fact]
        public void DegToRadTest4()
        {
            Assert.Equal(5.0 * Math.PI / 2.0, 450.0.DegToRad(), 4);
        }

        [Fact]
        public void DegToRadTest5()
        {
            Assert.Equal(-Math.PI / 2.0, (-90.0).DegToRad(), 4);
        }

        [Fact]
        public void DegToRadTest6()
        {
            Assert.Equal(-Math.PI, (-180.0).DegToRad(), 4);
        }

        [Fact]
        public void DegToRadTest7()
        {
            Assert.Equal(-5.0 * Math.PI / 2.0, (-450.0).DegToRad(), 4);
        }

        [Fact]
        public void RadToDegTest1()
        {
            Assert.Equal(0.0, 0.0.RadToDeg());
        }

        [Fact]
        public void RadToDegTest2()
        {
            Assert.Equal(90.0, (Math.PI / 2.0).RadToDeg());
        }

        [Fact]
        public void RadToDegTest3()
        {
            Assert.Equal(180.0, Math.PI.RadToDeg());
        }

        [Fact]
        public void RadToDegTest4()
        {
            Assert.Equal(450.0, ((5.0 / 2.0) * Math.PI).RadToDeg());
        }

        [Fact]
        public void RadToDegTest5()
        {
            Assert.Equal(-90.0, (-Math.PI / 2.0).RadToDeg());
        }

        [Fact]
        public void RadToDegTest6()
        {
            Assert.Equal(-180.0, -Math.PI.RadToDeg());
        }

        [Fact]
        public void RadToDegTest7()
        {
            Assert.Equal(-450.0, (-(5.0 / 2.0) * Math.PI).RadToDeg());
        }


        [Fact]
        public void ToAngleTest1()
        {
            Assert.Equal(0.0, (0.0, 0.0).ToAngle(), 3);
        }

        [Fact]
        public void ToAngleTest2()
        {
            Assert.Equal(0.0, (1.0, 0.0).ToAngle(), 3);
        }

        [Fact]
        public void ToAngleTest3()
        {
            Assert.Equal(90.0, (0.0, 1.0).ToAngle().RadToDeg(), 3);
        }

        [Fact]
        public void ToAngleTest4()
        {
            Assert.Equal(180.0, (-1.0, 0.0).ToAngle().RadToDeg(), 3);
        }

        [Fact]
        public void ToAngleTest5()
        {
            Assert.Equal(270.0, (0.0, -1.0).ToAngle().RadToDeg(), 3);
        }

        [Fact]
        public void ToAngleTest6()
        {
            Assert.Equal(45.0, (1.0, 1.0).ToAngle().RadToDeg(), 3);
        }

        [Fact]
        public void ToAngleTest7()
        {
            Assert.Equal(135.0, (-1.0, 1.0).ToAngle().RadToDeg(), 3);
        }

        [Fact]
        public void ToAngleTest8()
        {
            Assert.Equal(225.0, (-1.0, -1.0).ToAngle().RadToDeg(), 3);
        }

        [Fact]
        public void ToAngleTest9()
        {
            Assert.Equal(315.0, (1.0, -1.0).ToAngle().RadToDeg(), 3);
        }
    }
}
