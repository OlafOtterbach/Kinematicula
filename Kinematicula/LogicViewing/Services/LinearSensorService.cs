using Kinematicula.Scening;
using Kinematicula.Graphics;
using Kinematicula.Mathematics;

namespace Kinematicula.LogicViewing.Services
{
    public class LinearSensorService : IMoveSensorService
    {
        public bool CanProcess(ISensor sensor) => sensor.GetType() == typeof(LinearSensor);

        public void Process(ISensor sensor, MoveAction moveAction, Scene Scene)
        {
            var offset = moveAction.Camera.Frame.Offset;
            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveAction.StartMoveX, moveAction.StartMoveY, moveAction.CanvasWidth, moveAction.CanvasHeight, moveAction.Camera.NearPlane, moveAction.Camera.Frame);
            var startMoveDirection = startMoveOffset - offset;
            var startMoveRay = new Axis3D(startMoveOffset, startMoveDirection);

            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveAction.EndMoveX, moveAction.EndMoveY, moveAction.CanvasWidth, moveAction.CanvasHeight, moveAction.Camera.NearPlane, moveAction.Camera.Frame);
            var endMoveDirection = endMoveOffset - offset;
            var endMoveRay = new Axis3D(endMoveOffset, endMoveDirection);

            Process(sensor as LinearSensor, moveAction.Body, startMoveRay, endMoveRay, Scene);
        }

        private void Process(LinearSensor linearSensor, Body body, Axis3D startMoveRay, Axis3D endMoveRay, Scene Scene)
        {
            if (startMoveRay.Offset == endMoveRay.Offset) return;

            var referenceFrame = linearSensor.ReferenceBodyWithOrigin?.Frame ?? body.Frame;
            var bodyFrame = body.Frame;
            var moveVector = CalculateMove(referenceFrame, linearSensor.Axis, linearSensor.Offset, startMoveRay, endMoveRay);
            var moveFrame = Matrix44D.CreateTranslation(moveVector);

            var changedBodyFrame = moveFrame * bodyFrame;
            Scene.SetBodyFrame(body, changedBodyFrame);
        }

        private Vector3D CalculateMove(Matrix44D referenceFrame, Vector3D axis, Position3D offset, Axis3D startMoveRay, Axis3D endMoveRay)
        {
            var axisOffset = referenceFrame * offset;
            var axisDirection = referenceFrame * axis;
            var moveAxis = new Axis3D(axisOffset, axisDirection);

            var (startExist, startPlump) = moveAxis.CalculatePerpendicularPoint(startMoveRay);
            var (endExist, endPlump) = moveAxis.CalculatePerpendicularPoint(endMoveRay);
            var moveVector = startExist && endExist ? endPlump - startPlump : new Vector3D();

            return moveVector;
        }
    }
}

