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
            var offset = moveEvent.Camera.Frame.Offset;

            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.StartMoveX, moveEvent.StartMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var startMoveDirection = startMoveOffset - offset;
            var startMoveRay = new Axis3D(startMoveOffset, startMoveDirection);

            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveEvent.EndMoveX, moveEvent.EndMoveY, moveEvent.CanvasWidth, moveEvent.CanvasHeight, moveEvent.Camera.NearPlane, moveEvent.Camera.Frame);
            var endMoveDirection = endMoveOffset - offset;
            var endMoveRay = new Axis3D(endMoveOffset, endMoveDirection);

            var bodyTouchPosition = moveEvent.Body.Frame * moveEvent.BodyTouchPosition;

            // Calculate spin
            var moveVec = (endMoveOffset - startMoveOffset).Normalize();

            var cylinderSensor = sensor as CylinderSensor;
            var bodyFrame = cylinderSensor.ReferenceBodyWithOrigin?.Frame ?? moveEvent.Body.Frame;
            var cylinderAxis = new Axis3D(bodyFrame * cylinderSensor.Offset, bodyFrame * cylinderSensor.Axis);

            var axisPerpendicularPoint = cylinderAxis.CalculatePerpendicularPoint(bodyTouchPosition);
            var ex = bodyTouchPosition - axisPerpendicularPoint;
            var axisFrame = Matrix44D.CreateCoordinateSystem(cylinderAxis.Offset, ex, cylinderAxis.Direction).Inverse();
            var p1 = new Position3D();
            var p2 = axisFrame * bodyTouchPosition;
            var p3 = axisFrame * (bodyTouchPosition + moveVec);

            var spin = TriangleMath.IsCounterClockwise(p1.X, p1.Y, p2.X, p2.Y, p3.X, p3.Y) ? 1.0 : -1.0;

            Process(sensor as CylinderSensor,
                    moveEvent.Body,
                    bodyTouchPosition,
                    moveEvent.StartMoveX,
                    moveEvent.StartMoveY,
                    startMoveRay,
                    moveEvent.EndMoveX,
                    moveEvent.EndMoveY,
                    endMoveRay,
                    moveEvent.CanvasWidth,
                    moveEvent.CanvasHeight,
                    moveEvent.Camera,
                    Scene,
                    spin);
        }

        private void Process(
            CylinderSensor cylinderSensor,
            Body body,
            Position3D bodyTouchPosition,
            double startX,
            double startY,
            Axis3D startMoveRay,
            double endX,
            double endY,
            Axis3D endMoveRay,
            double canvasWidth,
            double canvasHeight,
            Camera camera,
            Scene Scene,
            double spin)
        {
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;

            var bodyFrame = cylinderSensor.ReferenceBodyWithOrigin?.Frame ?? body.Frame;
            var cylinderAxis = new Axis3D(bodyFrame * cylinderSensor.Offset, bodyFrame * cylinderSensor.Axis);

            double angle;
            if (IsAxisIsInCameraPlane(cylinderAxis, camera))
            {
                angle = CalculateAngleForAxisLyingInCanvas(bodyTouchPosition, startX, startY, endMoveRay, cylinderAxis, canvasWidth, canvasHeight, camera,spin);
            }
            else
            {
                angle = CalculateAngleForAxisNotLyingInCanvas(startX, startY, startMoveRay, endX, endY, endMoveRay, cylinderAxis, canvasWidth, canvasHeight,spin);
            }

            if (Math.Abs(angle.RadToDeg()) > cylinderSensor.LimitationAngle)
            {
                angle = Math.Sign(angle) * cylinderSensor.LimitationAngle;
            }

            Rotate(body, cylinderAxis, angle, Scene);
        }

        private static bool IsAxisIsInCameraPlane(Axis3D cylinderAxis, Camera camera)
        {
            var cameraDirection = camera.Frame.Ey;
            double limitAngle = 25.0;
            double alpha = cylinderAxis.Direction.AngleWith(cameraDirection);
            alpha = alpha.Modulo2Pi();
            alpha = alpha.RadToDeg();
            var result = !((Math.Abs(alpha) < limitAngle) || (Math.Abs(alpha) > (180.0 - limitAngle)));

            return result;
        }

        private static double CalculateAngleForAxisLyingInCanvas(
            Position3D bodyTouchPosition,
            double startX,
            double startY,
            Axis3D endMoveRay,
            Axis3D cylinderAxis,
            double canvasWidth,
            double canvasHeight,
            Camera camera,
            double spin
        )
        {
            var angle = 0.0;
            var cameraDirection = camera.Frame.Ey;

            var direction = (cameraDirection & cylinderAxis.Direction).Normalize() * 10000.0;
            var offset = ViewProjection.ProjectCanvasToSceneSystem(startX, startY, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var p1 = offset - direction;
            var p2 = offset + direction;
            var projectedAxis = new Axis3D(p1, p2 - p1);
            var (success, plump) = projectedAxis.CalculatePerpendicularPoint(endMoveRay);
            if (success)
            {
                // Ermitteln des Vorzeichens für Drehung
                //var sign = (plump - p1).Length < (plump - p2).Length ? -1.0 : 1.0;

                // Ermitteln der Länge der Mausbewegung in Richtung senkrecht zur Rotationsachse
                var (endX, endY) = ViewProjection.ProjectSceneSystemToCanvas(plump, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
                double delta = Vector2DMath.Length(startX, startY, endX, endY);

                // Projektion Berührungspunkt auf Rotationsachse.
                // Ermittel Scheitelpunkte der Rotation auf Achse senkrecht zur Kamera und Rotationsachse.
                // Projektion der Scheitelpunkte auf Canvas.
                // Abstand Scheitelpunkte auf Achse ist die Mausbewegung für 180°.
                // Winkel ist Verhältnis Länge Mausbewegung zur Länge für 180°.
                var plumpPoint = cylinderAxis.CalculatePerpendicularPoint(bodyTouchPosition);
                var distance = (bodyTouchPosition - plumpPoint).Length;
                direction = direction.Normalize() * distance;
                var startPosition = cylinderAxis.Offset - direction;
                var endPosition = cylinderAxis.Offset + direction;
                (startX, startY) = ViewProjection.ProjectSceneSystemToCanvas(startPosition, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
                (endX, endY) = ViewProjection.ProjectSceneSystemToCanvas(endPosition, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
                var lengthOfHalfRotation = Vector2DMath.Length(startX, startY, endX, endY);

                var angleInDegree = 180.0 * delta / lengthOfHalfRotation;

                angle = spin * angleInDegree.DegToRad();
            }

            return angle;
        }

        private static double CalculateAngleForAxisNotLyingInCanvas(
            double startX,
            double startY,
            Axis3D startMoveRay,
            double endX,
            double endY,
            Axis3D endMoveRay,
            Axis3D cylinderAxis,
            double canvasWidth,
            double canvasHeight,
            double spin)
        {
            var axisPlane = new Plane3D(cylinderAxis.Offset, cylinderAxis.Direction);
            var startOffset = GetPosition(startMoveRay, axisPlane);
            var endOffset = GetPosition(endMoveRay, axisPlane);

            //double sign = CalculateAngleSign(cylinderAxis, startOffset, endOffset);
            var angle = spin * 1.0 * CalculateAngle(startX, startY, endX, endY, canvasWidth, canvasHeight);

            return angle;
        }

        private static Position3D GetPosition(Axis3D moveRay, Plane3D axisPlane)
        {
            var (success, position) = axisPlane.Intersect(moveRay);
            var result = success ? position : moveRay.Offset;
            return result;
        }

        private static double CalculateAngleSign(Axis3D cylinderAxis, Position3D start, Position3D end)
        {
            var axisFrame = Matrix44D.CreatePlaneCoordinateSystem(cylinderAxis.Offset, cylinderAxis.Direction);
            Matrix44D invframe = axisFrame.Inverse();

            var startInFrame = invframe * start;
            var endInFrame = invframe * end;

            var sx = startInFrame.X;
            var sy = startInFrame.Y;
            var ex = endInFrame.X;
            var ey = endInFrame.Y;

            var sign = TriangleMath.IsCounterClockwise(sx, sy, ex, ey, 0.0, 0.0) ? 1.0 : -1.0;

            return sign;
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
