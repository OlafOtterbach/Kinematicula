using System.Linq;
using System.Collections.Generic;
using Kinematicula.Graphics;
using Kinematicula.Scening;

namespace Kinematicula.LogicViewing.Services
{
    public class MoveSensorProcessor : IMoveSensorProcessor
    {
        private IMoveSensorService[] _moveSensorServices;

        public MoveSensorProcessor() : this(new CylinderSensorService(), new SphereSensorService(), new PlaneSensorService(), new LinearSensorService())
        { }

        public MoveSensorProcessor(params IMoveSensorService[] moveServices)
        {
            _moveSensorServices = moveServices.ToArray();
        }

        public bool Process(MoveAction moveAction, Scene Scene)
        {
            var eventIsProcessed = false;

            var senorsWithEventSource = moveAction.Body?.Sensors.Where(x => !string.IsNullOrEmpty(x.EventSource)) ?? new ISensor[0];
            var senorsWithoutEventSource = moveAction.Body?.Sensors.Where(x => string.IsNullOrEmpty(x.EventSource)) ?? new ISensor[0];
            var sensor = senorsWithEventSource.Concat(senorsWithoutEventSource).FirstOrDefault(sensor => sensor.EventSource == moveAction.EventSource || sensor.EventSource == "");
            if (sensor == null)
                return false;

            var services = _moveSensorServices.Where(s => s.CanProcess(sensor));
            foreach (var sensorService in services)
            {
                sensorService.Process(sensor, moveAction, Scene);
                eventIsProcessed = true;
            }

            return eventIsProcessed;
        }
    }
}