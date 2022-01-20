using Kinematicula.Scening;

namespace Kinematicula.LogicViewing.Services
{
    public interface IMoveSensorProcessor
    {
        bool Process(MoveAction moveEvent, Scene Scene);
    }
}
