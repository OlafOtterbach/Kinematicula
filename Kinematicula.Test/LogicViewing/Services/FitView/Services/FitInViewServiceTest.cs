namespace Kinematicula.Test.LogicViewing.Services.FitView.Services;

using Kinematicula.LogicViewing.Services.FitInView.Services;
using Kinematicula.Mathematics;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class FitInViewServiceTest
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var pointCloud
            = new List<Position3D>
            {
                new Position3D(-1,-1,-1),
                new Position3D(-1,-1, 1),
                new Position3D(-1, 1,-1),
                new Position3D(-1, 1, 1),
                new Position3D( 1,-1,-1),
                new Position3D( 1,-1, 1),
                new Position3D( 1, 1,-1),
                new Position3D( 1, 1, 1),
            };
        var canvasWidth = 1.0;
        var canvasHeight = 1.0;
        var nearPlane = 1.0;
        var cameraFrame = Matrix44D.CreateCoordinateSystem(new Vector3D(1.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0));

        // Act
        var transformation = FitInViewService.FitInView(cameraFrame, 1.0, 1.0, 1.0, pointCloud);

        // Assert
        Assert.Equal(-3.0, transformation.Offset.X, 2);
    }

    [Fact]
    public void Test2()
    {
        // Arrange
        var pointCloud
            = new List<Position3D>
            {
                new Position3D( 0, 0,-1),
                new Position3D( 0, 0, 1),
                new Position3D( 0,-1, 0),
                new Position3D( 0, 1, 0),
            };
        var cameraFrame = Matrix44D.CreateCoordinateSystem(new Vector3D(1.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0));

        // Act
        var transformation = FitInViewService.FitInView(cameraFrame, 1.0, 1.0, 1.0, pointCloud);

        // Assert
        Assert.Equal(-2.0, transformation.Offset.X, 2);
    }

    [Fact]
    public void Test3()
    {
        // Arrange
        var pointCloud
            = new List<Position3D>
            {
                new Position3D(-1, 0, 0),
                new Position3D( 1, 0, 0),
                new Position3D( 0,-1, 0),
                new Position3D( 0, 1, 0),
            };
        var cameraFrame = Matrix44D.CreateCoordinateSystem(new Vector3D(0.0, 0.0, -1.0), new Vector3D(1.0, 0.0, 0.0));

        var cameraSystem = cameraFrame.Inverse();
        var pointCloudInCameraSystem = pointCloud.Select(p => cameraSystem * p).ToList();

        // Act
        var transformation = FitInViewService.FitInView(cameraFrame, 1.0, 1.0, 1.0, pointCloudInCameraSystem);

        // Assert
        Assert.Equal(2.0, transformation.Offset.Z, 2);
    }

}
