using Kinematicula.Graphics;
using Kinematicula.Mathematics;

namespace Kinematicula.Scening
{
    internal class FreezeScene
    {
        private Dictionary<Body, Matrix44D> _snapShot;

        public FreezeScene(Scene scene)
        {
            _snapShot = scene.Bodies.ToDictionary(body => body, body => body.Frame);
        }

        public void ResetScene()
        {
            _snapShot.ToList().ForEach(pair => pair.Key.Frame = pair.Value);
        }
    }
}
