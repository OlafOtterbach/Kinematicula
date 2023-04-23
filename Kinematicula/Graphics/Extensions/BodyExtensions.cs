using System.Collections.Generic;
using System.Linq;

namespace Kinematicula.Graphics.Extensions
{
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
    }
}
