using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using Kinematicula.Scening;

namespace Kinematicula.LogicViewing.Services
{
    public class CylinderSensorService : IMoveSensorService
    {
        public bool CanProcess(ISensor sensor) => sensor.GetType() == typeof(CylinderSensor);

        public void Process(ISensor sensor, MoveAction moveEvent, Scene Scene)
        {
            var startX = moveEvent.StartMoveX;
            var startY = moveEvent.StartMoveY;
            var endX = moveEvent.EndMoveX;
            var endY = moveEvent.EndMoveY;
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;
            var cylinderSensor = sensor as CylinderSensor;

            // calculate spin
            var body = moveEvent.Body;
            var clyinderAxisFrame = cylinderSensor.ReferenceBodyWithOrigin?.Frame ?? body.Frame;
            var cylinderAxis = new Axis3D(clyinderAxisFrame * cylinderSensor.Offset, clyinderAxisFrame * cylinderSensor.Axis);
            var bodyTouchPosition = body.Frame * moveEvent.BodyTouchPosition;
            var axisPerpendicularPoint = cylinderAxis.CalculatePerpendicularPoint(bodyTouchPosition);
            var ex = (bodyTouchPosition - axisPerpendicularPoint).Normalize();
            var axisFrame = Matrix44D.CreateCoordinateSystem(axisPerpendicularPoint, ex, cylinderAxis.Direction).Inverse();

            var canvasWidth = moveEvent.CanvasWidth;
            var canvasHeight = moveEvent.CanvasHeight;
            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(startX, startY, canvasWidth, canvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(endX, endY, canvasWidth, canvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var moveVec = (endMoveOffset - startMoveOffset).Normalize();

            var middle = new Position3D();
            var p2 = axisFrame * bodyTouchPosition;
            var p3 = axisFrame * (bodyTouchPosition + moveVec);

            var spin = TriangleMath.IsCounterClockwise(middle.X, middle.Y, p2.X, p2.Y, p3.X, p3.Y) ? 1.0 : -1.0;

            // calculate angle
            var angle = spin * CalculateAngle(startX, startY, endX, endY, canvasWidth, canvasHeight);
            if (Math.Abs(angle.RadToDeg()) > cylinderSensor.LimitationAngle)
            {
                angle = Math.Sign(angle) * cylinderSensor.LimitationAngle;
            }

            // rotate body
            Rotate(body, cylinderAxis, angle, Scene);
        }

        private static double CalculateAngle(
            double startX,
            double startY,
            double endX,
            double endY,
            double canvasWidth,
            double canvasHeight)
        {
            var min = Math.Min(canvasWidth, canvasHeight);
            var delta = Vector2DMath.Length(startX, startY, endX, endY);
            var angle = (360.0 * delta / min).DegToRad();
            return angle;
        }

        private void Rotate(Body body, Axis3D axis, double angle, Scene Scene)
        {
            var rotation = Matrix44D.CreateRotation(axis.Offset, axis.Direction, angle);
            var changedBodyFrame = rotation * body.Frame;
            Scene.SetBodyFrame(body, changedBodyFrame);
        }
    }
}
