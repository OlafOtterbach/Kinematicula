namespace Kinematicula.Test.LogicViewing.Services.FitView.Services;

using Kinematicula.Mathematics;
using Xunit;

public class FitInViewService
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
        var 




    }
}
