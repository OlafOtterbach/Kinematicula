using Kinematicula.Mathematics;
using Kinematicula.Scening;

namespace Kinematicula.Graphics.Extensions
{
    public static class IntersectionSceneExtension
    {
        public static (bool isIntersected, Position3D localIntersection, Body body) GetIntersectionOfRayAndScene(this Scene Scene, Position3D rayOffset, Vector3D rayDirection)
        {
            var triangles = GetSceneTriangles(Scene);
            var minIntersectionResult
                  = triangles.Select(triangle => (triangle.Item1, IntersectionMath.GetIntersectionAndSquaredDistanceOfRayAndTriangle(rayOffset, rayDirection, triangle.Item2, triangle.Item3, triangle.Item4)))
                             .Where(result => result.Item2.isIntersected)
                             .AsParallel()
                             .Aggregate(((Body)null, (false, new Position3D(), double.MaxValue)), (acc, x) => x.Item2.squaredDistance < acc.Item2.Item3 ? x : acc);
            var isIntersected = minIntersectionResult.Item2.Item1;
            var body = minIntersectionResult.Item1;
            var localIntersection = isIntersected ? body.Frame.Inverse() * minIntersectionResult.Item2.Item2 : minIntersectionResult.Item2.Item2;

            return (isIntersected, localIntersection, body);
        }

        private static IEnumerable<(Body, Position3D, Position3D, Position3D)> GetSceneTriangles(Scene Scene)
        {
            return Scene.Bodies.SelectMany(GetBodyTriangles);
        }

        private static IEnumerable<(Body, Position3D, Position3D, Position3D)> GetBodyTriangles(Body body)
        {
            var triangles = body.Faces.SelectMany(face => face.Triangles);
            foreach (var triangle in triangles)
            {
                var p1 = body.Frame * triangle.P1.Point.Position;
                var p2 = body.Frame * triangle.P2.Point.Position;
                var p3 = body.Frame * triangle.P3.Point.Position;
                yield return (body, p1, p2, p3);
            }
        }
    }
}