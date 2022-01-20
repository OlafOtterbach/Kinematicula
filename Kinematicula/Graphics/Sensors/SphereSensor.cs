using Kinematicula.Mathematics;

namespace Kinematicula.Graphics
{
    public class SphereSensor : ISensor
    {
        public SphereSensor() : this("", new Position3D(), null, double.MaxValue)
        { }

        public SphereSensor(string eventSource) : this(eventSource, new Position3D(), null, double.MaxValue)
        { }

        public SphereSensor(double limitationAngle) : this("", new Position3D(), null, limitationAngle)
        { }

        public SphereSensor(string eventSource, double limitationAngle) : this(eventSource, new Position3D(), null, limitationAngle)
        { }

        public SphereSensor(Body referenceBodyWithOrigin) : this("", new Position3D(), referenceBodyWithOrigin, double.MaxValue)
        { }

        public SphereSensor(string eventSource, Body referenceBodyWithOrigin) : this(eventSource, new Position3D(), referenceBodyWithOrigin, double.MaxValue)
        { }

        public SphereSensor(Position3D sphereOffset) : this("", sphereOffset, null, double.MaxValue)
        { }

        public SphereSensor(string eventSource, Position3D sphereOffset) : this(eventSource, sphereOffset, null, double.MaxValue)
        { }

        public SphereSensor(Position3D sphereOffset, double limitationAngle) : this("", sphereOffset, null, limitationAngle)
        { }

        public SphereSensor(string eventSource, Position3D sphereOffset, double limitationAngle) : this(eventSource, sphereOffset, null, limitationAngle)
        { }

        public SphereSensor(Position3D sphereOffset, Body referenceBodyWithOrigin) : this("", sphereOffset, referenceBodyWithOrigin, double.MaxValue)
        { }

        public SphereSensor(string eventSource, Position3D sphereOffset, Body referenceBodyWithOrigin) : this(eventSource, sphereOffset, referenceBodyWithOrigin, double.MaxValue)
        { }


        public SphereSensor(string eventSource, Position3D sphereOffset, Body referenceBodyWithOrigin, double limitationAngle)
        {
            EventSource = eventSource;
            SphereOffset = sphereOffset;
            ReferenceBodyWithOrigin = referenceBodyWithOrigin;
            LimitationAngle = limitationAngle;
        }

        public string EventSource { get; }

        public Position3D SphereOffset { get; }

        public Body ReferenceBodyWithOrigin { get; }

        public double LimitationAngle { get; }
    }
}
