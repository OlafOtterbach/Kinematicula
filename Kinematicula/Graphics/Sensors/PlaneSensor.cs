namespace Kinematicula.Graphics;

using Kinematicula.Mathematics;

public class PlaneSensor : ISensor
{
    public PlaneSensor(Vector3D planeNormal) : this("", planeNormal, new Position3D(), null, double.MaxValue)
    {
    }

    public PlaneSensor(Vector3D planeNormal, double limitationMoveDistance) : this("", planeNormal, new Position3D(), null, limitationMoveDistance)
    {
    }

    public PlaneSensor(Vector3D planeNormal, Body referenceBodyWithOrigin) : this("", planeNormal, new Position3D(), referenceBodyWithOrigin, double.MaxValue)
    {
    }

    public PlaneSensor(Vector3D planeNormal, Body referenceBodyWithOrigin, double limitationMoveDistance) : this("", planeNormal, new Position3D(), referenceBodyWithOrigin, limitationMoveDistance)
    {
    }

    public PlaneSensor(Vector3D planeNormal, Position3D planeOffset) : this("", planeNormal, planeOffset, null, double.MaxValue)
    {
    }

    public PlaneSensor(Vector3D planeNormal, Position3D planeOffset, double limitationMoveDistance) : this("", planeNormal, planeOffset, null, limitationMoveDistance)
    {
    }

    public PlaneSensor(Vector3D planeNormal, Position3D planeOffset, Body referenceBodyWithOrigin) : this("", planeNormal, planeOffset, referenceBodyWithOrigin, double.MaxValue)
    {
    }


    public PlaneSensor(Vector3D planeNormal, Position3D planeOffset, Body referenceBodyWithOrigin, double limitationMoveDistance) : this("", planeNormal, planeOffset, referenceBodyWithOrigin, limitationMoveDistance)
    {
    }


    public PlaneSensor(string eventSource, Vector3D planeNormal) : this(eventSource, planeNormal, new Position3D(), null, double.MaxValue)
    {
    }

    public PlaneSensor(string eventSource, Vector3D planeNormal, double limitationMoveDistance) : this(eventSource, planeNormal, new Position3D(), null, limitationMoveDistance)
    {
    }

    public PlaneSensor(string eventSource, Vector3D planeNormal, Body referenceBodyWithOrigin) : this(eventSource, planeNormal, new Position3D(), referenceBodyWithOrigin, double.MaxValue)
    {
    }

    public PlaneSensor(string eventSource, Vector3D planeNormal, Body referenceBodyWithOrigin, double limitationMoveDistance) : this(eventSource, planeNormal, new Position3D(), referenceBodyWithOrigin, limitationMoveDistance)
    {
    }

    public PlaneSensor(string eventSource, Vector3D planeNormal, Position3D planeOffset) : this(eventSource, planeNormal, planeOffset, null, double.MaxValue)
    {
    }

    public PlaneSensor(string eventSource, Vector3D planeNormal, Position3D planeOffset, double limitationMoveDistance) : this(eventSource, planeNormal, planeOffset, null, limitationMoveDistance)
    {
    }

    public PlaneSensor(string eventSource, Vector3D planeNormal, Position3D planeOffset, Body referenceBodyWithOrigin) : this(eventSource, planeNormal, planeOffset, referenceBodyWithOrigin, double.MaxValue)
    {
    }


    public PlaneSensor(string eventSource, Vector3D planeNormal, Position3D planeOffset, Body referenceBodyWithOrigin, double limitationMoveDistance)
    {
        PlaneNormal = planeNormal;
        PlaneOffset = planeOffset;
        EventSource = eventSource;
        ReferenceBodyWithOrigin = referenceBodyWithOrigin;
        LimitationMoveDistance = limitationMoveDistance;
    }

    public Vector3D PlaneNormal { get; }

    public Position3D PlaneOffset { get; }

    public string EventSource { get; }

    public double LimitationMoveDistance { get; }

    public Body ReferenceBodyWithOrigin { get; }
}
