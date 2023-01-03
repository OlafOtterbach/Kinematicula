using Kinematicula.Mathematics;
using ThreeJsViewerApi.Converters;

namespace ThreeJsViewerApi.Test.Converters;

public class ConverterMatrix44DToEulerFrameTjsTest
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var rotX = Matrix44D.CreateRotation(new Vector3D(1, 0, 0), ConstantsMath.Pi / 2.0);
        var rotY = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), ConstantsMath.Pi / 2.0);
        var rotZ = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), ConstantsMath.Pi / 2.0);
        var mat = rotZ * rotY * rotX;

        // Act
        var eulerFarmeTjs = mat.ToEulerFrameTjs();

        // Assert
        var rotXTjs = Matrix44D.CreateRotation(new Vector3D(1, 0, 0), eulerFarmeTjs.AngleX);
        var rotYTjs = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), eulerFarmeTjs.AngleY);
        var rotZTjs = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), eulerFarmeTjs.AngleZ);
        var matTjs = rotZTjs * rotYTjs * rotXTjs;

        Assert.Equal(mat, matTjs);
    }

    [Fact]
    public void Test2()
    {
        var rand = new Random();

        for (int i = 0; i < 1000; i++)
        {
            // Arrange
            var angleX = rand.NextDouble() * ConstantsMath.Pi2;
            var angleY = rand.NextDouble() * ConstantsMath.Pi2;
            var angleZ = rand.NextDouble() * ConstantsMath.Pi2;


            var rotX = Matrix44D.CreateRotation(new Vector3D(1, 0, 0), angleX);
            var rotY = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), angleY);
            var rotZ = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), angleZ);
            var mat = rotZ * rotY * rotX;

            // Act
            var eulerFarmeTjs = mat.ToEulerFrameTjs();

            // Assert
            var rotXTjs = Matrix44D.CreateRotation(new Vector3D(1, 0, 0), eulerFarmeTjs.AngleX);
            var rotYTjs = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), eulerFarmeTjs.AngleY);
            var rotZTjs = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), eulerFarmeTjs.AngleZ);
            var matTjs = rotZTjs * rotYTjs * rotXTjs;

            Assert.Equal(mat, matTjs);
        }
    }

}
