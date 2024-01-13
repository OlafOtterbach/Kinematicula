namespace Kinematicula.LogicViewing.Services.FitInView.Services;

using Kinematicula.Mathematics;

public static class CardinalDirectionsCreator
{
    public static Plane CreateWest(Matrix44D cameraFrame, double angleBetweenWestAndEastPlane)
    {
        var rotation = Matrix44D.CreateRotation(cameraFrame.Offset, cameraFrame.Ez, (angleBetweenWestAndEastPlane + Math.PI) / 2.0);
        var planeNormal = rotation * cameraFrame.Ex;
        var planeOffset = cameraFrame.Offset;

        var plane = new Plane(planeOffset, planeNormal);
        return plane;
    }

    public static Plane CreateEast(Matrix44D cameraFrame, double cameraAngle)
    {
        var rotation = Matrix44D.CreateRotation(cameraFrame.Offset, cameraFrame.Ez, -(cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraFrame.Ex;
        var planeOffset = cameraFrame.Offset;

        var plane = new Plane(planeOffset, planeNormal);
        return plane;
    }

    public static Plane CreateSouth(Matrix44D cameraFrame, double cameraAngle)
    {
        var rotation = Matrix44D.CreateRotation(cameraFrame.Offset, cameraFrame.Ey, (cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraFrame.Ex;
        var planeOffset = cameraFrame.Offset;

        var plane = new Plane(planeOffset, planeNormal);
        return plane;
    }

    public static Plane CreateNorth(Matrix44D cameraFrame, double cameraAngle)
    {
        var rotation = Matrix44D.CreateRotation(cameraFrame.Offset, cameraFrame.Ey, -(cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraFrame.Ex;
        var planeOffset = cameraFrame.Offset;

        var plane = new Plane(planeOffset, planeNormal);
        return plane;
    }
}
