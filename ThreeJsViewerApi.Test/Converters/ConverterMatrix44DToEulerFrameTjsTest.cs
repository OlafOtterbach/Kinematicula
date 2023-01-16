using Kinematicula.Mathematics;
using ThreeJsViewerApi.Converters;

namespace ThreeJsViewerApi.Test.Converters;

public class ConverterMatrix44DToEulerFrameTjsTest
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var frame = Matrix44D.Identity;

        // Act
        var euler = frame.ToEulerFrameTjs();
        var matrix = euler.ToMatrix44D();

        // Assert
        Assert.Equal(frame, matrix);
    }

    [Fact]
    public void Test2()
    {
        // Arrange
        var ex = new Vector3D(1, 0, 0);
        var ey = new Vector3D(0, 1, 0);
        var ez = new Vector3D(0, 0, 1);
        var rotY = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), ConstantsMath.HalfPi);
        var rotZ = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), 0.0);
        ex = rotZ * rotY * ex;
        ez = rotZ * rotY * ez;
        var frame = Matrix44D.CreateCoordinateSystem(ex, ez);

        // Act
        var euler = frame.ToEulerFrameTjs();
        var matrix = euler.ToMatrix44D();

        // Assert
        Assert.Equal(frame, matrix);
    }

    [Fact]
    public void Test3()
    {
        // Arrange
        var ex = new Vector3D(1, 0, 0);
        var ey = new Vector3D(0, 1, 0);
        var ez = new Vector3D(0, 0, 1);
        var rotY = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), 0.0);
        var rotZ = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), ConstantsMath.HalfPi);
        ex = rotZ * rotY * ex;
        ez = rotZ * rotY * ez;
        var frame = Matrix44D.CreateCoordinateSystem(ex, ez);

        // Act
        var euler = frame.ToEulerFrameTjs();
        var matrix = euler.ToMatrix44D();

        // Assert
        Assert.Equal(frame, matrix);
    }

    [Fact]
    public void Test77()
    {
        // Arrange
        var ex = new Vector3D(1, 0, 0);
        var ey = new Vector3D(0, 1, 0);
        var ez = ex & ey;
        var rotY = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), ConstantsMath.Pi / 4.0);
        var rotZ = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), ConstantsMath.Pi / 4.0);
        ex = rotZ * rotY * ex;
        ez = rotZ * rotY * ez;
        var frame = Matrix44D.CreateCoordinateSystem(ex, ez);

        // Act
        var euler = frame.ToEulerFrameTjs();
        var matrix = euler.ToMatrix44D();

        // Assert
        Assert.Equal(frame, matrix);
    }
}
