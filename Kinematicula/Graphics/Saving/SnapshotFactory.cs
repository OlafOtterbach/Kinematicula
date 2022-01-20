using System.Collections.Generic;
using System.Linq;
using Kinematicula.Scening;

namespace Kinematicula.Graphics.Saving
{
    public class SnapshotFactory : ISnapshotFactory
    {
        public SnapshotFactory(IStateFactory stateFactory)
        {
            StateFactory = stateFactory;
        }

        public IStateFactory StateFactory { get; }

        public Snapshot TakeSnapshot(Scene Scene)
        {
            var bodies = Scene.Bodies.ToList();
            var constraints = bodies.SelectMany(x => x.Constraints).Distinct().ToList();
            var objects = bodies.OfType<object>().Concat(constraints.OfType<object>()).ToList();
            var dict = new Dictionary<object, IState>();
            foreach (var obj in objects)
            {
                dict[obj] = StateFactory.Create(obj);
            }

            var screenshot = new Snapshot(dict);

            return screenshot;
        }
    }
}
