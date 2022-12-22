namespace Kinematicula.Test.Graphics.Creators;

using Kinematicula.Graphics.Creators;
using Xunit;

public class CylinderTest
{
    [Fact]
    public void Test()
    {
        var cylinder = Cylinder.Create(4, 100, 200);

        Assert.Equal(3, cylinder.Faces.Length);
    }
}
