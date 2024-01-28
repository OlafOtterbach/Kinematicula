namespace Kinematicula.Test.Mathematics.Extensions;

using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using Xunit;

public class Plane3DExtensionsTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var plane = new Plane3D(new Position3D(0, 0, 0), new Vector3D(0, 0, 1));
        var position1 = new Position3D(0, 0,  10);
        var position2 = new Position3D(0, 0, -10);

        // Act
        var relDist1 = plane.RelativeDistanceTo(position1);
        var relDist2 = plane.RelativeDistanceTo(position2);

        // Assert
        Assert.Equal(10.0, relDist1, 3);
        Assert.Equal(-10.0, relDist2, 3);
    }
}
