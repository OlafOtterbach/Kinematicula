namespace Kinematicula.LogicViewing.Services.FitInView.Services;

using Kinematicula.Mathematics;

public static class CardinalDirectionsCreator
{
    public static Plane3D CreateWest(double angleBetweenWestAndEastPlane)
    {
        var cameraOffset = new Position3D(0.0, 0.0, 0.0);
        var cameraDirection = new Vector3D(1.0, 0.0, 0.0);
        var cameraUp = new Vector3D(0.0, 0.0, 1.0);

        var rotation = Matrix44D.CreateRotation(cameraOffset, cameraUp, (angleBetweenWestAndEastPlane + Math.PI) / 2.0);
        var planeNormal = rotation * cameraDirection;
        var planeOffset = cameraOffset;

        var plane = new Plane3D(planeOffset, planeNormal);
        return plane;
    }

    public static Plane3D CreateEast(double angleBetweenWestAndEastPlane)
    {
        var cameraOffset = new Position3D(0.0, 0.0, 0.0);
        var cameraDirection = new Vector3D(1.0, 0.0, 0.0);
        var cameraUp = new Vector3D(0.0, 0.0, 1.0);

        var rotation = Matrix44D.CreateRotation(cameraOffset, cameraUp, -(angleBetweenWestAndEastPlane + Math.PI) / 2.0);
        var planeNormal = rotation * cameraDirection;
        var planeOffset = cameraOffset;

        var plane = new Plane3D(planeOffset, planeNormal);
        return plane;
    }

    public static Plane3D CreateSouth(double cameraAngle)
    {
        var cameraOffset = new Position3D(0.0, 0.0, 0.0);
        var cameraDirection = new Vector3D(1.0, 0.0, 0.0);
        var cameraLeft = new Vector3D(0.0, 1.0, 0.0);

        var rotation = Matrix44D.CreateRotation(cameraOffset, cameraLeft, (cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraDirection;
        var planeOffset = cameraOffset;

        var plane = new Plane3D(planeOffset, planeNormal);
        return plane;
    }

    public static Plane3D CreateNorth(double cameraAngle)
    {
        var cameraOffset = new Position3D(0.0, 0.0, 0.0);
        var cameraDirection = new Vector3D(1.0, 0.0, 0.0);
        var cameraLeft = new Vector3D(0.0, 1.0, 0.0);

        var rotation = Matrix44D.CreateRotation(cameraOffset, cameraLeft, -(cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraDirection;
        var planeOffset = cameraOffset;

        var plane = new Plane3D(planeOffset, planeNormal);
        return plane;
    }
}
