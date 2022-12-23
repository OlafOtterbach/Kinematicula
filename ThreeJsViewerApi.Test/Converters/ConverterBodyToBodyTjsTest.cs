using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;
using ThreeJsViewerApi.Converters;

namespace ThreeJsViewerApi.Test.Converters;

public class ConverterBodyToBodyTjsTest
{
    [Fact]
    public void Test1()
    {
        var cube = Cube.Create(100);
        cube.Name = "Hugo";
        cube.Frame = Matrix44D.CreateTranslation(new Vector3D(1, 2, 3));

        var cubeTjs = cube.ToBodyTjs();

        Assert.Equal(cube.Id, cubeTjs.Id);
        Assert.Equal(cube.Name, cubeTjs.Name);
        Assert.Equal(cube.Frame.Offset, new Position3D(cubeTjs.Frame.X, cubeTjs.Frame.Y, cubeTjs.Frame.Z));
        Assert.Equal(6 * 2, cubeTjs.Triangles.Length);
        Assert.Equal(6 * 4, cubeTjs.Vertices.Length);
        Assert.Equal(4 + 4 + 4, cubeTjs.Edges.Length);
        Assert.True(cube.Points.Select(p => p.Position).All(pos => cubeTjs.Vertices.Any(v => pos == new Position3D(v.Position.X, v.Position.Y, v.Position.Z))));
    }
}