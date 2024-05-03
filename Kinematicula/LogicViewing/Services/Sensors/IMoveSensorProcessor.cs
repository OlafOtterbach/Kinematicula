using Kinematicula.Scening;

namespace Kinematicula.LogicViewing.Services.Sensors
{
    public interface IMoveSensorProcessor
    {
        bool Process(MoveAction moveEvent, Scene Scene);
    }
}
