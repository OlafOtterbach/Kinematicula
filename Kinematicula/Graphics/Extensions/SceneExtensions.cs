using System;
using System.Linq;
using Kinematicula.Scening;

namespace Kinematicula.Graphics.Extensions
{
    public static class SceneExtensions
    {
        public static Body GetBody(this Scene Scene, Guid bodyId)
        {
            var body = Scene.Bodies.FirstOrDefault(body => body.Id == bodyId);
            return body;
        }
    }
}
