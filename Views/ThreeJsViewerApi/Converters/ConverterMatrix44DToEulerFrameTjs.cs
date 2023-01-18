namespace ThreeJsViewerApi.Converters;

using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
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

    public static EulerFrameTjs ToEulerFrameTjs2(this Matrix44D matrix)
    {
        var angleX = 0.0;
        var angleY = 0.0;
        var angleZ = 0.0;

        if (matrix.A13.EqualsTo(1.0) || matrix.A13.EqualsTo(-1.0))
        {
            angleY = 0.0;
            var delta = Math.Atan2(matrix.A12, matrix.A13);
            if(matrix.A13.EqualsTo(-1.0))
            {
                angleY = ConstantsMath.HalfPi;
                angleX = delta;
            }
            else
            {
                angleY = -ConstantsMath.HalfPi;
                angleX = delta;
            }
        }
        else
        {
            angleY = -Math.Asin( matrix.A13 );
            angleX = Math.Atan2( matrix.A23 / Math.Cos(angleY), matrix.A33 / Math.Cos(angleY) );
            angleZ = Math.Atan2( matrix.A12 / Math.Cos(angleY), matrix.A11 / Math.Cos(angleY) );
        }

        var eulerFrameTjs = new EulerFrameTjs(
            matrix.Offset.X,
            matrix.Offset.Y,
            matrix.Offset.Z,
            angleX,
            angleY,
            angleZ);

        return eulerFrameTjs;

        /*
        function Eul = RotMat2Euler(R)

        if R(1,3) == 1 | R(1,3) == -1
          %special case
          E3 = 0; %set arbitrarily
          dlta = atan2(R(1,2),R(1,3));
          if R(1,3) == -1
            E2 = pi/2;
            E1 = E3 + dlta;
          else
            E2 = -pi/2;
            E1 = -E3 + dlta;
          end
        else
          E2 = - asin(R(1,3));
          E1 = atan2(R(2,3)/cos(E2), R(3,3)/cos(E2));
          E3 = atan2(R(1,2)/cos(E2), R(1,1)/cos(E2));
        end

        Eul = [E1 E2 E3]; 
        */
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
