using System;
using Kinematicula.Graphics;
using Kinematicula.LogicViewing.Mathmatics;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using Kinematicula.Scening;

namespace Kinematicula.LogicViewing.Services.Sensors
{
    public class SphereSensorService : IMoveSensorService
    {
        public bool CanProcess(ISensor sensor) => sensor.GetType() == typeof(SphereSensor);

        public void Process(ISensor sensor, MoveAction moveAction, Scene Scene)
        {
            var sphereSensor = sensor as SphereSensor;
            var startX = moveAction.StartMoveX;
            var startY = moveAction.StartMoveY;
            var endX = moveAction.EndMoveX;
            var endY = moveAction.EndMoveY;
            if (startX.EqualsTo(endX) && startY.EqualsTo(endY)) return;

            var offset = moveAction.Camera.Frame.Offset;
            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveAction.EndMoveX, moveAction.EndMoveY, moveAction.CanvasWidth, moveAction.CanvasHeight, moveAction.Camera.NearPlane, moveAction.Camera.Frame);
            var endMoveDirection = endMoveOffset - offset;
            var endMoveRay = new Axis3D(endMoveOffset, endMoveDirection);
            var camera = moveAction.Camera;
            var canvasWidth = moveAction.CanvasWidth;
            var canvasHeight = moveAction.CanvasHeight;
            var bodyTouchPosition = moveAction.BodyTouchPosition;

            var canvasOrigin = ViewProjection.ProjectCanvasToSceneSystem(0.0, 0.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var canvasExPos = ViewProjection.ProjectCanvasToSceneSystem(1.0, 0.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var canvasEyPos = ViewProjection.ProjectCanvasToSceneSystem(0.0, 1.0, canvasWidth, canvasHeight, camera.NearPlane, camera.Frame);
            var exAxis = (canvasExPos - canvasOrigin).Normalize();
            var ezAxis = (canvasEyPos - canvasOrigin).Normalize();

            var body = moveAction.Body;
            var bodyFrame = sphereSensor.ReferenceBodyWithOrigin?.Frame ?? body.Frame;
            var zCylinderAxis = new Axis3D(bodyFrame * sphereSensor.SphereOffset, ezAxis);
            var xCylinderAxis = new Axis3D(bodyFrame * sphereSensor.SphereOffset, exAxis);

            var angleZ = CalculateAngle(bodyTouchPosition, startX, startY, endMoveRay, zCylinderAxis, canvasWidth, canvasHeight, camera);
            var angleX = CalculateAngle(bodyTouchPosition, startX, startY, endMoveRay, xCylinderAxis, canvasWidth, canvasHeight, camera);
            var signZ = Math.Sign(angleZ);
            var alphaZ = Math.Abs(angleZ);
            var signX = Math.Sign(angleX);
            var alphaX = Math.Abs(angleX);

            if (alphaZ > sphereSensor.LimitationAngle)
            {
                alphaX = sphereSensor.LimitationAngle * alphaX / alphaZ;
                alphaZ = sphereSensor.LimitationAngle;
                angleX = signX * alphaX;
                angleZ = signZ * alphaZ;
            }

            if (alphaX > sphereSensor.LimitationAngle)
            {
                angleZ = signZ * sphereSensor.LimitationAngle * alphaZ / alphaX;
                angleX = signX * sphereSensor.LimitationAngle;
            }

            var rotationZ = Matrix44D.CreateRotation(zCylinderAxis.Offset, zCylinderAxis.Direction, angleZ);
            var rotationX = Matrix44D.CreateRotation(xCylinderAxis.Offset, xCylinderAxis.Direction, angleX);
            var changedFrame = rotationZ * rotationX * body.Frame;
            Scene.SetBodyFrame(moveAction.Body, changedFrame);
        }

        private static double CalculateAngle(
            Position3D bodyTouchPosition,
            double startX,
            double startY,
            Axis3D endMoveRay,
            Axis3D cylinderAxis,
            double canvasWidth,
            double canvasHeight,
            Camera camera
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
                var sign = (plump - p1).Length < (plump - p2).Length ? -1.0 : 1.0;

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

                angle = sign * angleInDegree.ToRadiant();
            }

            return angle;
        }
    }
}