using Kinematicula.Scening;

namespace Kinematicula.Graphics.Saving
{
    public interface ISnapshotFactory
    {
        IStateFactory StateFactory { get; }

        Snapshot TakeSnapshot(Scene Scene);
    }
}
