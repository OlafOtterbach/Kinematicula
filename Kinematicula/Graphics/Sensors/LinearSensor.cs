using Kinematicula.Mathematics;

namespace Kinematicula.Graphics
{
    public class LinearSensor : ISensor
    {
        public LinearSensor(Vector3D axis) : this("", axis, new Position3D(), null, double.MaxValue)
        {
        }

        public LinearSensor(Vector3D axis, double limitationMoveDistance) : this("", axis, new Position3D(), null, limitationMoveDistance)
        {
        }

        public LinearSensor(Vector3D axis, Body referenceBodyWithOrigin) : this("", axis, new Position3D(), referenceBodyWithOrigin, double.MaxValue)
        {
        }

        public LinearSensor(Vector3D axis, Body referenceBodyWithOrigin, double limitationMoveDistance) : this("", axis, new Position3D(), referenceBodyWithOrigin, limitationMoveDistance)
        {
        }

        public LinearSensor(Vector3D axis, Position3D offset) : this("", axis, offset, null, double.MaxValue)
        {
        }

        public LinearSensor(Vector3D axis, Position3D offset, double limitationMoveDistance) : this("", axis, offset, null, limitationMoveDistance)
        {
        }

        public LinearSensor(Vector3D axis, Position3D offset, Body referenceBodyWithOrigin) : this("", axis, offset, referenceBodyWithOrigin, double.MaxValue)
        {
        }


        public LinearSensor(Vector3D axis, Position3D offset, Body referenceBodyWithOrigin, double limitationMoveDistance) : this("", axis, offset, referenceBodyWithOrigin, limitationMoveDistance)
        {
        }


        public LinearSensor(string eventSource, Vector3D axis) : this(eventSource, axis, new Position3D(), null, double.MaxValue)
        {
        }

        public LinearSensor(string eventSource, Vector3D axis, double limitationMoveDistance) : this(eventSource, axis, new Position3D(), null, limitationMoveDistance)
        {
        }

        public LinearSensor(string eventSource, Vector3D axis, Body referenceBodyWithOrigin) : this(eventSource, axis, new Position3D(), referenceBodyWithOrigin, double.MaxValue)
        {
        }

        public LinearSensor(string eventSource, Vector3D axis, Body referenceBodyWithOrigin, double limitationMoveDistance) : this(eventSource, axis, new Position3D(), referenceBodyWithOrigin, limitationMoveDistance)
        {
        }

        public LinearSensor(string eventSource, Vector3D axis, Position3D offset) : this(eventSource, axis, offset, null, double.MaxValue)
        {
        }

        public LinearSensor(string eventSource, Vector3D axis, Position3D offset, double limitationMoveDistance) : this(eventSource, axis, offset, null, limitationMoveDistance)
        {
        }

        public LinearSensor(string eventSource, Vector3D axis, Position3D offset, Body referenceBodyWithOrigin) : this(eventSource, axis, offset, referenceBodyWithOrigin, double.MaxValue)
        {
        }


        public LinearSensor(string eventSource, Vector3D axis, Position3D offset, Body referenceBodyWithOrigin, double limitationMoveDistance)
        {
            EventSource = eventSource;
            Axis = axis;
            Offset = offset;
            ReferenceBodyWithOrigin = referenceBodyWithOrigin;
            LimitationMoveDistance = limitationMoveDistance;
        }

        public Vector3D Axis { get; }

        public Position3D Offset { get; }

        public string EventSource { get; }

        public double LimitationMoveDistance { get; }

        public Body ReferenceBodyWithOrigin { get; }
    }
}
