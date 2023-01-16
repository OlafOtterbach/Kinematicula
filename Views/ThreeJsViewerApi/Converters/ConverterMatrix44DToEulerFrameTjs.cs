namespace ThreeJsViewerApi.Converters;

using Kinematicula.Mathematics;
using ThreeJsViewerApi.Model;

public static class ConverterMatrix44DToEulerFrameTjs
{
    public static EulerFrameTjs ToEulerFrameTjs(this Matrix44D matrix)
    {
        var value = Math.Max(-1.0, Math.Min(1.0, matrix.A13));
        var angleY = Math.Asin(value);

        var isInRange = Math.Abs(matrix.A13) < (1.0 - ConstantsMath.Epsilon);
        var angleX = isInRange ? Math.Atan2(-matrix.A23, matrix.A33) : Math.Atan2(matrix.A32, matrix.A22);
        var angleZ = isInRange ? Math.Atan2(-matrix.A12, matrix.A11) : 0.0;

        var eulerFrameTjs = new EulerFrameTjs(
            matrix.Offset.X,
            matrix.Offset.Y,
            matrix.Offset.Z,
            angleX,
            angleY,
            angleZ);

        return eulerFrameTjs;
    }

    public static Matrix44D ToMatrix44D(this EulerFrameTjs euler)
    {
        var rotX = Matrix44D.CreateRotation(new Vector3D(1, 0, 0), euler.AngleX);
        var rotY = Matrix44D.CreateRotation(new Vector3D(0, 1, 0), euler.AngleY);
        var rotZ = Matrix44D.CreateRotation(new Vector3D(0, 0, 1), euler.AngleZ);
        var translation = Matrix44D.CreateTranslation(new Vector3D(euler.X, euler.Y, euler.Z));

        var matrix = translation * rotZ * rotY * rotX;

        return matrix;
    }
}
