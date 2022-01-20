using Kinematicula.Mathematics;
using Xunit;

namespace Kinematicula.Tests.Mathematics
{
    public class TransformMathTest
    {
        [Fact]
        public void CalculateAxesAndRotationTest()
        {
            var start = Matrix44D.CreateCoordinateSystem(new Vector3D(1, 0, 0), new Vector3D(0, 0, 1));
            var endEx = new Vector3D(1, 1, 0).Normalize();
            var end = Matrix44D.CreateCoordinateSystem(endEx, new Vector3D(0, 0, 1));

            Vector3D axisAlpha;
            Vector3D axisBeta;
            var angleAlpha = 0.0;
            var angleBeta = 0.0;
            TransformMath.CalculateAxesAndRotation(start, end, out axisAlpha, out angleAlpha, out axisBeta, out angleBeta);
            var alphaRotation = Matrix44D.CreateRotation(start.Offset, axisAlpha, angleAlpha);
            var betaRotation = Matrix44D.CreateRotation(start.Offset, axisBeta, angleBeta);
            var rotation = alphaRotation * betaRotation;
            var result = rotation * start;

            Assert.Equal(end, result);
        }

        [Fact]
        public void CalculateAxesAndRotationTest2()
        {
            var start = Matrix44D.CreateCoordinateSystem(new Vector3D(1, 0, 0), new Vector3D(0, 0, 1));
            var endEx = new Vector3D(-0.1, 1, 0).Normalize();
            var end = Matrix44D.CreateCoordinateSystem(endEx, new Vector3D(0, 0, 1));

            Vector3D axisAlpha;
            Vector3D axisBeta;
            var angleAlpha = 0.0;
            var angleBeta = 0.0;
            TransformMath.CalculateAxesAndRotation(start, end, out axisAlpha, out angleAlpha, out axisBeta, out angleBeta);
            var alphaRotation = Matrix44D.CreateRotation(start.Offset, axisAlpha, angleAlpha);
            var betaRotation = Matrix44D.CreateRotation(start.Offset, axisBeta, angleBeta);
            var rotation = alphaRotation * betaRotation;
            var result = rotation * start;

            Assert.Equal(end, result);
        }

        [Fact]
        public void CalculateAxesAndRotationTest3()
        {
            var start = Matrix44D.CreateCoordinateSystem(new Vector3D(1, 0, 0), new Vector3D(0, 0, 1));
            var end = Matrix44D.CreateCoordinateSystem(new Vector3D(1, 0, 0), new Vector3D(0, 0, 1));

            Vector3D axisAlpha;
            Vector3D axisBeta;
            var angleAlpha = 0.0;
            var angleBeta = 0.0;
            TransformMath.CalculateAxesAndRotation(start, end, out axisAlpha, out angleAlpha, out axisBeta, out angleBeta);
            var alphaRotation = Matrix44D.CreateRotation(start.Offset, axisAlpha, angleAlpha);
            var betaRotation = Matrix44D.CreateRotation(start.Offset, axisBeta, angleBeta);
            var rotation = alphaRotation * betaRotation;
            var result = rotation * start;

            Assert.Equal(end, result);
        }
    }
}
