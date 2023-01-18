﻿namespace ThreeJsViewerApi.Converters;

using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using ThreeJsViewerApi.Model;

public static class ConverterCameraToCameraTjs
{
    public static CameraTjs ToCameraTjs(this Camera camera)
    {
        var offset = camera.Frame.Offset;
        var ex = camera.Frame.Ex;
        var ey = camera.Frame.Ey;
        var ez = camera.Frame.Ez;

        var rotY = Matrix44D.CreateRotation(ey, -ConstantsMath.HalfPi);
        var rotZ = Matrix44D.CreateRotation(ez, -ConstantsMath.HalfPi);
        var offsetTjs = offset;
        var exTjs = (rotY * rotZ * ex).Normalize();
        var eyTjs = (rotY * rotZ * ey).Normalize();
        var ezTjs = (rotY * rotZ * ez).Normalize();
        var frameTjs = Matrix44D.CreateCoordinateSystem(offsetTjs, exTjs, eyTjs, ezTjs);


        var cameraTjs = new CameraTjs(
            camera.Name,
            frameTjs.ToEulerFrameTjs(),
            camera.Frame.ToFrameTjs());

        return cameraTjs;
    }
}