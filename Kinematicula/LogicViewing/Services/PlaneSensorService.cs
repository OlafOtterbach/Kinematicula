using Kinematicula.Scening;
using Kinematicula.Graphics;
using Kinematicula.Mathematics;

namespace Kinematicula.LogicViewing.Services
{
    public class PlaneSensorService : IMoveSensorService
    {
        public bool CanProcess(ISensor sensor) => sensor.GetType() == typeof(PlaneSensor);

        public void Process(ISensor sensor, MoveAction moveAction, Scene Scene)
        {
            var offset = moveAction.Camera.Frame.Offset;

            var startMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveAction.StartMoveX, moveAction.StartMoveY, moveAction.CanvasWidth, moveAction.CanvasHeight, moveAction.Camera.NearPlane, moveAction.Camera.Frame);
            var startMoveDirection = startMoveOffset - offset;
            var startMoveRay = new Axis3D(startMoveOffset, startMoveDirection);

            var endMoveOffset = ViewProjection.ProjectCanvasToSceneSystem(moveAction.EndMoveX, moveAction.EndMoveY, moveAction.CanvasWidth, moveAction.CanvasHeight, moveAction.Camera.NearPlane, moveAction.Camera.Frame);
            var endMoveDirection = endMoveOffset - offset;
            var endMoveRay = new Axis3D(endMoveOffset, endMoveDirection);

            Process(sensor as PlaneSensor, moveAction.Body, startMoveRay, endMoveRay, Scene);
        }

        private void Process(
            PlaneSensor planeSensor,
            Body body,
            Axis3D startMoveRay,
            Axis3D endMoveRay,
            Scene Scene)
        {
            if (startMoveRay.Offset == endMoveRay.Offset) return;
            var referenceFrame = planeSensor.ReferenceBodyWithOrigin?.Frame ?? body.Frame;
            var moveVector = CalculateMove(referenceFrame, planeSensor.PlaneNormal, planeSensor.PlaneOffset, startMoveRay, endMoveRay);
            var moveFrame = Matrix44D.CreateTranslation(moveVector);

            var changedBodyFrame = moveFrame * body.Frame;
            Scene.SetBodyFrame(body, changedBodyFrame);
        }

        private static Vector3D CalculateMove(Matrix44D bodyFrame, Vector3D normal, Position3D offset, Axis3D startMoveRay, Axis3D endMoveRay)
        {
            var planeOffset = bodyFrame * offset;
            var planeNormal = bodyFrame * normal;
            var movePlane = new Plane3D(planeOffset, planeNormal);

            var (startIsIntersecting, startIntersection) = movePlane.Intersect(startMoveRay);
            var (endIsIntersecting, endIntersection) = movePlane.Intersect(endMoveRay);
            var moveVector = startIsIntersecting && endIsIntersecting ? endIntersection - startIntersection : new Vector3D();

            return moveVector;
        }
    }
}