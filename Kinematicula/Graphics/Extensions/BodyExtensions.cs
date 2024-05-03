namespace Kinematicula.Graphics.Extensions;

using Kinematicula.Mathematics;

public static class BodyExtensions
{
    public static IEnumerable<Body> GetBodyAndDescendants(this Body body)
    {
        yield return body;
        var children = body.Children.SelectMany(child => child.GetBodyAndDescendants());
        foreach (var child in children)
        {
            yield return child;
        }
    }

    public static List<Position3D> GetPointCloud(this IEnumerable<Body> bodies, Matrix44D transform)
    {
        var allBodies = bodies.SelectMany(body => body.GetBodyAndDescendants()).ToList();
        var pointCloud =  GetPointCloudFromBodies(allBodies, transform);
        return pointCloud;
    }

    private static List<Position3D> GetPointCloudFromBodies(this IEnumerable<Body> bodies, Matrix44D transform)
    {
        var points = bodies.AsParallel().SelectMany(body => body.GetPointCloudFromBody(transform)).ToList();
        return points;
    }

    private static IEnumerable<Position3D> GetPointCloudFromBody(this Body body, Matrix44D transform)
    {
        var matrix = transform * body.Frame;
        var points = body.Points.AsParallel().Select(p => matrix * p.Position);
        return points;
    }
}
