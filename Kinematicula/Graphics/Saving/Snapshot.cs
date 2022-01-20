using Kinematicula.Extensions;
using Kinematicula.Mathematics;
using System.Collections.Generic;
using System.Linq;

namespace Kinematicula.Graphics.Saving
{
    public class Snapshot
    {
        private readonly Dictionary<object, IState> _stateDict;

        public Snapshot(Dictionary<object, IState> stateDict)
        {
            _stateDict = stateDict;
        }

        public Matrix44D GetFrameFor(Body body) => (_stateDict[body] as BodyState).Frame;

        public void ResetScene()
        {
            _stateDict.Values.ForEach(x => x.Restore());
        } 

        public void ResetSceneFor(Body body)
        {
            var neighbourBodies = body.Constraints
                                      .Select(c => c.First.Body != body ? c.First.Body : c.Second.Body);
            var bodies = body.Concat(neighbourBodies).ToList();
            ResetScene(bodies);
        }

        private void ResetScene(IEnumerable<Body> except)
        {
            var bodies = except.ToList();
            var constraints = bodies.SelectMany(body => body.Constraints)
                                    .Where(c => bodies.Contains(c.First.Body) && bodies.Contains(c.Second.Body))
                                    .ToList();
            var objects = bodies.Cast<object>().Concat(constraints.Cast<object>()).ToList();
            foreach(var key in _stateDict.Keys)
            {
                if(!objects.Contains(key))
                {
                    _stateDict[key].Restore();
                }
            }
        }
    }
}