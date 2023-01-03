using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using ThreeJsViewerApi.Model;

namespace ThreeJsViewerApi.Converters;

public static class ConverterMatrix44DToEulerFrameTjs
{
    public static EulerFrameTjs ToEulerFrameTjs(this Matrix44D matrix)
    {
        // Define the rotation matrix
        //double[,] rotationMatrix = { { 0.5, 0.5, -0.5 }, { 0.5, 0.5, 0.5 }, { 0.5, -0.5, 0.5 } };

        // Calculate the Y-axis Euler angle (yaw)
        //double yaw = Math.Atan2(rotationMatrix[1, 0], rotationMatrix[0, 0]);
        //double yaw = Math.Atan2(matrix.A12, matrix.A11);
        double yaw = Math.Atan2(matrix.A21, matrix.A11);

        // Calculate the X-axis Euler angle (pitch)
        //double pitch = Math.Atan2(-rotationMatrix[2, 0],
        //  Math.Sqrt(rotationMatrix[2, 1] * rotationMatrix[2, 1] + rotationMatrix[2, 2] * rotationMatrix[2, 2]));
        //double pitch = Math.Atan2(-matrix.A13, Math.Sqrt(matrix.A23 * matrix.A23 + matrix.A33 * matrix.A33));
        double pitch = Math.Atan2(-matrix.A31, Math.Sqrt(matrix.A32 * matrix.A32 + matrix.A33 * matrix.A33));

        // Calculate the Z-axis Euler angle (roll)
        //double roll = Math.Atan2(rotationMatrix[2, 1], rotationMatrix[2, 2]);
        //double roll = Math.Atan2(matrix.A23, matrix.A33);
        double roll = Math.Atan2(matrix.A32, matrix.A33);


        var eulerTjs = new EulerFrameTjs(
            matrix.Offset.X,
            matrix.Offset.Y,
            matrix.Offset.Z,
            pitch,
            yaw,
            roll);

        return eulerTjs;
    }

    //public static EulerFrameTjs ToEulerFrameTjs(this Matrix44D matrix)
    //{
    //    var cardanFrame = matrix.ToCardanFrame();
    //    var eulerTjs = new EulerFrameTjs(cardanFrame.Offset.X, cardanFrame.Offset.Y, cardanFrame.Offset.Z, cardanFrame.AlphaAngleAxisX, cardanFrame.BetaAngleAxisY, cardanFrame.GammaAngleAxisZ);
    //    return eulerTjs;
    //}
}
