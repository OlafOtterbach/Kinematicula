using Kinematicula.Scening;
using Kinematicula.Graphics;

namespace Kinematicula.LogicViewing.Services.Sensors
{
    public interface IMoveSensorService
    {
        bool CanProcess(ISensor sensor);

        void Process(ISensor sensor, MoveAction moveEvent, Scene Scene);
    }
}
