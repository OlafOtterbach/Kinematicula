using Kinematicula.Graphics;
using Kinematicula.Mathematics;

namespace Kinematicula.Scening
{
    internal class FreezeScene
    {
        private Dictionary<Body, Matrix44D> _snapShot;

        public FreezeScene(Scene scene)
        {
            var bodies = scene.Bodies.SelectMany(body => SelectManyBodies(body));
            _snapShot = bodies.ToDictionary(body => body, body => body.Frame);
        }

        public IEnumerable<Body> SelectManyBodies(Body body)
        {
            var bodies = new[] { body }.Concat(body.Children.SelectMany(child => SelectManyBodies(child))).ToList();
            return bodies;
        }

        public void ResetScene()
        {
            _snapShot.ToList().ForEach(pair => pair.Key.Frame = pair.Value);
        }
    }
}
