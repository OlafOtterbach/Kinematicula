namespace Kinematicula.LogicViewing.Services.FitInView.Services;

using Kinematicula.Mathematics;

public static class CardinalDirectionsCreator
{
    public static (Position3D OffsetPlane, Vector3D NormalPlane) CreateWest(Matrix44D cameraFrame, double cameraAngle)
    {
        var rotation = Matrix44D.CreateRotation(cameraFrame.Offset, cameraFrame.Ez, (cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraFrame.Ex;
        var planeOffset = cameraFrame.Offset;

        return (planeOffset, planeNormal);
    }

    public static (Position3D OffsetPlane, Vector3D NormalPlane) CreateEast(Matrix44D cameraFrame, double cameraAngle)
    {
        var rotation = Matrix44D.CreateRotation(cameraFrame.Offset, cameraFrame.Ez, -(cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraFrame.Ex;
        var planeOffset = cameraFrame.Offset;

        return (planeOffset, planeNormal);
    }

    public static (Position3D OffsetPlane, Vector3D NormalPlane) CreateSouth(Matrix44D cameraFrame, double cameraAngle)
    {
        var rotation = Matrix44D.CreateRotation(cameraFrame.Offset, cameraFrame.Ey, (cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraFrame.Ex;
        var planeOffset = cameraFrame.Offset;

        return (planeOffset, planeNormal);
    }

    public static (Position3D OffsetPlane, Vector3D NormalPlane) CreateNorth(Matrix44D cameraFrame, double cameraAngle)
    {
        var rotation = Matrix44D.CreateRotation(cameraFrame.Offset, cameraFrame.Ey, -(cameraAngle + Math.PI) / 2.0);
        var planeNormal = rotation * cameraFrame.Ex;
        var planeOffset = cameraFrame.Offset;

        return (planeOffset, planeNormal);
    }
}
