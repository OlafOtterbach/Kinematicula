namespace Kinematicula.Test.LogicViewing.Services.FitView.Services;

using Kinematicula.LogicViewing.Services.FitInView.Services;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class DistanceMathmaticsTests
{
    [Fact]
    public void MinDistanceToPlaneTest()
    {
        // Arrange
        var positions = new List<Position3D>
        {
            new Position3D(-100, 100, 10),
            new Position3D(100, 100, -10),
            new Position3D(100, 100, -2),
            new Position3D(-100, -100, 4),
        };

        var plane = new Plane3D(new Position3D(0, 0, 0), new Vector3D(0, 0, 1));

        // Act
        var nearestPosition = positions.MinDistanceToPlane(plane);

        // Assert
        Assert.Equal(positions[2], nearestPosition);
    }

    [Fact]
    public void GetNonIntersectionDistanceOfBoundedBoxTest()
    {
        // Arrange
        var positions = new List<Position3D>
        {
            new Position3D(-10, -10, -10),
            new Position3D( 10, -10, -10),
            new Position3D(-10,  10, -10),
            new Position3D( 10,  10, -10),
            new Position3D(-10, -10,  10),
            new Position3D( 10, -10,  10),
            new Position3D(-10,  10,  10),
            new Position3D( 10,  10,  10),
        };
        
        var boundedBox = positions.GetBoundedBox();

        var horizontalAngle = 90.0.ToRadiant();
        var verticalAngle = 45.0.ToRadiant();

        // Act
        var mat = boundedBox.GetNonIntersectionDistanceOfBoundedBox(horizontalAngle, verticalAngle);

        // Assert
        var translated = positions.Select(p => mat * p).ToList();
        var planeWest = FrustumViewPlanes.CreateWest(horizontalAngle);
        var planeEast = FrustumViewPlanes.CreateWest(horizontalAngle);
        var planeNorth = FrustumViewPlanes.CreateWest(verticalAngle);
        var planeSouth = FrustumViewPlanes.CreateWest(verticalAngle);

        // All Positions in frustum of the four planes.
        Assert.True(translated.All(p => planeWest.DistanceTo(p) <= 0.0));
        Assert.True(translated.All(p => planeEast.DistanceTo(p) <= 0.0));
        Assert.True(translated.All(p => planeNorth.DistanceTo(p) <= 0.0));
        Assert.True(translated.All(p => planeSouth.DistanceTo(p) <= 0.0));
    }
}
