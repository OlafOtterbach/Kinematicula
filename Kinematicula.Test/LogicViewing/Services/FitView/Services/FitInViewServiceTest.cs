namespace Kinematicula.Test.LogicViewing.Services.FitView.Services;

using Kinematicula.LogicViewing.Services.FitInView.Services;
using Kinematicula.Mathematics;
using Xunit;

public class FitInViewServiceTest
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var pointCloud
            = new[]
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




    }
}
