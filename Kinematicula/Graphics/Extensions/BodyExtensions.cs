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

    public static IEnumerable<Position3D> GetPointCloude(this IEnumerable<Body> bodies)
    {
        return bodies.GetPointCloude(Matrix44D.Identity);
    }

    public static IEnumerable<Position3D> GetPointCloude(this Body body)
    {
        return body.GetPointCloude(Matrix44D.Identity);
    }

    public static IEnumerable<Position3D> GetPointCloude(this IEnumerable<Body> bodies, Matrix44D transform)
    {
        var allBodies = bodies.SelectMany(body => body.GetBodyAndDescendants());
        return GetPointCloudeFromBodies(allBodies, transform);
    }

    public static IEnumerable<Position3D> GetPointCloude(this Body body, Matrix44D transform)
    {
        var bodies = body.GetBodyAndDescendants();
        return GetPointCloudeFromBodies(bodies, transform);
    }

    private static IEnumerable<Position3D> GetPointCloudeFromBodies(this IEnumerable<Body> bodies, Matrix44D transform)
    {
        var points = bodies.AsParallel().SelectMany(body => body.GetPointCloudeFromBody(transform));
        return points;
    }

    private static IEnumerable<Position3D> GetPointCloudeFromBody(this Body body, Matrix44D transform)
    {
        var matrix = transform * body.Frame;
        var points = body.Points.AsParallel().Select(p => matrix * p.Position);
        return points;
    }
}
