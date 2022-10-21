namespace RobotDemonstration.Robot.Kinematics;

using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using System;

// Six axes robot
//                                                 / 
//                                                / n6 (Adapter)
//                                               /
//                                              \
//                                             /  Alpha6
//                                            /
//                                           / n5 (Wrist)
//       Alpha3    (Forearm)      (Ulna)    /
//              O-------------|------------O
//             /      n3   Alpha4   n4       Alpha5
//            /           
//           /
//          / n2 (Upperarm)
//         /
//        /    
//       /
//      O Alpha2
//      |
//      |
//      |
//      |
//      | n1 (Shoulder)
//      |
//      |
//      |
//      |
//     --- Alpha1

public class RobotService
{
    private const double n1 = 240.0; // Shoulder
    private const double n2 = 200.0; // Upperarm
    private const double n3 = 100.0; // Forearm
    private const double n4 = 45.0; // Ulna
    private const double n5 = 35; // Wrist
    private const double n6 = 10; // Adapter

    public static bool GetAxesForTransformation
    (
        Matrix44D transformation,
        ref double alpha1,
        ref double alpha2,
        ref double alpha3,
        ref double alpha4,
        ref double alpha5,
        ref double alpha6
    )
    {
        // Von Greifer zu Adapterkoordinatensystem
        var flangeTCP = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0));
        var mat = flangeTCP.Inverse();
        var adapter = transformation * mat;

        // Calculation of alpha 1
        //
        var wristPosition = adapter.Offset - adapter.Ez * (n5 + n6);
        var x1 = wristPosition.X;
        var y1 = wristPosition.Y;
        double newAlpha1;
        if (x1.EqualsTo(0.0) && y1.EqualsTo(0.0))
        {
            newAlpha1 = alpha1;
        }
        else
        {
            var anglePosWristToExOfOrigin = AngleMath.FindNextToPiAngleBasingOnAngle((x1, y1).ToAngle(), alpha1);
            newAlpha1 = anglePosWristToExOfOrigin;
        }

        var newAlpha1Deg = newAlpha1.ToDegree();

        // Calculation of alpha 2
        // 
        // 
        //                        B 
        //                        O  UpperArm
        //                       /|\
        //                      / | \
        //                     /  |  \
        //                    /   |   \
        //          n2 =  a  /    |    \  b = n3 + n4
        //                  /     | y   \
        //             |   /      |      \
        //          alpha2/       |       \
        //             | /        |        \
        //             |/        .|         \
        //  Shoulder A O----------*----------O C Wrist
        //              .   x     .   c-x   .
        //               .        .        .
        //                .       .       .
        //                 .      .      .
        //                  .     .-y   .
        //                   .    .    .
        //                    .   .   .
        //                     .  .  .
        //                      . . .
        //                       ...
        //                        O   UpperArm'
        //                        B'
        //			
        //	           |---------------------|
        //                        c = Length(Wrist - Shoulder)
        //
        //  x = (a^2 + c^2 - b^2) / (2 * c)
        //  y = a^2 - x^2
        //  => B(x,y)
        //  alpha2 = Angle(Ez(Shoulder), (Shoulder - Upperarm))
        //
        //
        var shoulderFrame = GetTransformation(newAlpha1);
        var transformationToShoulderFrame = shoulderFrame.Inverse();
        var shoulderToWristVector = (transformationToShoulderFrame * wristPosition).ToVector3D();

        var a = n2;
        var b = n3 + n4;
        var c = shoulderToWristVector.Length;
        var distanceX = (a * a + c * c - b * b) / (2 * c);
        var distanceY = Math.Sqrt(a * a - distanceX * distanceX);
        var xVec = distanceX * shoulderToWristVector.Normalize();
        var yVec = distanceY * (shoulderToWristVector & new Vector3D(0, 1, 0)).Normalize();

        var shoulderToupperArmVector1 = xVec + yVec;
        var shoulderToupperArmVector2 = xVec - yVec;

        var ez = new Vector3D(0, 0, 1);
        var eyRotationAxis = new Vector3D(0, 1, 0);

        var newAlpha2A = ez.CounterClockwiseAngleWith(shoulderToupperArmVector1, eyRotationAxis);
        var newAlpha2B = ez.CounterClockwiseAngleWith(shoulderToupperArmVector2, eyRotationAxis);
        var newAlpha2C = GetBestAngle(newAlpha2A, newAlpha2B, alpha2);

        var newAlpha2 = AngleMath.FindNextToPi2AngleBasingOnAngle(newAlpha2C, alpha2);

        var newAlpha2Deg = newAlpha2.ToDegree();


        // Calculation of alpha 3
        // 
        //                          /
        //                         /
        //               UpperArm O  Alpha 3 
        //                       / \
        //                      /   \
        //                     /     \
        //                    /       \
        //                n2 /         \  n3 + n4
        //                  /           \
        //             |   /             \
        //          alpha2/               \
        //             | /                 \
        //             |/                   \
        //  Shoulder A O----------*----------O C Wrist
        //			
        //

        var upperArmFrame = GetTransformation(newAlpha1, newAlpha2);
        var transformationToUpperArmFrame = upperArmFrame.Inverse();
        var upperArmToWristVector = (transformationToUpperArmFrame * wristPosition).ToVector3D();

        var ez3 = new Vector3D(0, 0, 1);
        var ey3RotationAxis = new Vector3D(0, 1, 0);

        var newAlpha3A = ez3.CounterClockwiseAngleWith(upperArmToWristVector, ey3RotationAxis);
        var newAlpha3 = AngleMath.FindNextToPi2AngleBasingOnAngle(newAlpha3A, alpha3);

        var newAlpha3Deg = newAlpha3.ToDegree();



        // Calculation of alpha 4
        //
        //
        //
        //    
        //          /Adapter
        //         /
        //      | /
        //      |/ Alpha4
        //      O--- ForeArmFrame
        //
        //
        //
        var foreArmFrame = GetTransformation(newAlpha1, newAlpha2, newAlpha3);
        var transformationToForeArmFrame = foreArmFrame.Inverse();
        var adapterVectorInForeArmFrame = transformationToForeArmFrame * adapter.Ez;

        double newAlpha4;
        if (adapterVectorInForeArmFrame.X.EqualsTo(0.0) && adapterVectorInForeArmFrame.Y.EqualsTo(0.0))
        {
            newAlpha4 = alpha4;
        }
        else
        {
            var anglePosAdapterToForeArm = AngleMath.FindNextToPiAngleBasingOnAngle((adapterVectorInForeArmFrame.X, adapterVectorInForeArmFrame.Y).ToAngle(), alpha4);
            newAlpha4 = anglePosAdapterToForeArm;
        }
        var newAlpha4Deg = newAlpha4.ToDegree();


        // Calculation of alpha 5
        var ulnaFrame = GetTransformation(newAlpha1, newAlpha2, newAlpha3, newAlpha4);
        var transformationToUlnaFrame = ulnaFrame.Inverse();
        var adapterVectorInUlnaFrame = transformationToUlnaFrame * adapter.Ez;
        double angleDirectionAdapterToUlnaDirection = (adapterVectorInUlnaFrame.Z, adapterVectorInUlnaFrame.X).ToAngle();
        var newAlpha5 = AngleMath.FindNextToPiAngleBasingOnAngle(angleDirectionAdapterToUlnaDirection, alpha5);
        var newAlpha5Deg = newAlpha5.ToDegree();


        // Calculation of alpha 6
        var wristFrame = GetTransformation(newAlpha1, newAlpha2, newAlpha3, newAlpha4, newAlpha5);
        var transformationToWristFrame = wristFrame.Inverse();
        var adapterVectorInWristFrame = transformationToWristFrame * adapter.Ex;

        double angleExAdapterToWristEx = (adapterVectorInWristFrame.X, adapterVectorInWristFrame.Y).ToAngle();
        var newAlpha6 = AngleMath.FindNextToPiAngleBasingOnAngle(angleExAdapterToWristEx, alpha6);
        var newAlpha6Deg = newAlpha6.ToDegree();

        alpha1 = newAlpha1;
        alpha2 = newAlpha2;
        alpha3 = newAlpha3;
        alpha4 = newAlpha4;
        alpha5 = newAlpha5;
        alpha6 = newAlpha6;

        return true;
    }



    private static double GetBestAngle(double angle1, double angle2, double alphaCompare)
    {
        // Calculates the possible new angles and deltas to actual angle
        double angleA = angle1.Modulo2Pi();
        double angleB = angle2.Modulo2Pi();
        double alphaCmp = alphaCompare.Modulo2Pi();

        var distA = angleA.DistantAngleTo(alphaCmp);
        var distB = angleB.DistantAngleTo(alphaCmp);

        if (distA < distB)
        {
            return angleA;
        }
        else
        {
            return angleB;
        }
    }

    private static void GetBestAngle(double angle1, double angle2, double alphaCompare, out double alpha, out int config)
    {
        // Calculates the possible new angles and deltas to actual angle
        double angleA = angle1.Modulo2Pi();
        double angleB = angle2.Modulo2Pi();
        double alphaCmp = alphaCompare.Modulo2Pi();

        var distA = angleA.DistantAngleTo(alphaCmp);
        var distB = angleB.DistantAngleTo(alphaCmp);

        if (distA < distB)
        {
            alpha = angleA;
            config = 0;
        }
        else
        {
            alpha = angleB;
            config = 1;
        }
    }




    public static Matrix44D GetTransformation
    (
        double alpha1,
        double alpha2,
        double alpha3,
        double alpha4,
        double alpha5,
        double alpha6
    )
    {
        var flangeTCP = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0));
        alpha1 = alpha1.Modulo2Pi();
        alpha2 = alpha2.Modulo2Pi();
        alpha3 = alpha3.Modulo2Pi();
        alpha4 = alpha4.Modulo2Pi();
        alpha5 = alpha5.Modulo2Pi();
        alpha6 = alpha6.Modulo2Pi();
        var origin = new Position3D(0.0, 0.0, 0.0);
        var ex = new Vector3D(1.0, 0.0, 0.0);
        var ey = new Vector3D(0.0, 1.0, 0.0);
        var ez = new Vector3D(0.0, 0.0, 1.0);
        var m6 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n6), ex, ey, ez);
        var r6 = Matrix44D.CreateRotation(origin, ez, alpha6);
        var m5 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n5), ex, ey, ez);
        var r5 = Matrix44D.CreateRotation(origin, ey, alpha5);
        var m4 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n4), ex, ey, ez);
        var r4 = Matrix44D.CreateRotation(origin, ez, alpha4);
        var m3 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n3), ex, ey, ez);
        var r3 = Matrix44D.CreateRotation(origin, ey, alpha3);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n2), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n1), ex, ey, ez);
        var r1 = Matrix44D.CreateRotation(origin, ez, alpha1);
        var tcp = r1 * (m1 * (r2 * (m2 * (r3 * (m3 * (r4 * (m4 * (r5 * (m5 * (r6 * (m6 * flangeTCP)))))))))));
        return tcp;
    }

    private static Matrix44D GetTransformation
    (
        double alpha1
    )
    {
        alpha1 = alpha1.Modulo2Pi();
        var origin = new Position3D(0.0, 0.0, 0.0);
        var ex = new Vector3D(1.0, 0.0, 0.0);
        var ey = new Vector3D(0.0, 1.0, 0.0);
        var ez = new Vector3D(0.0, 0.0, 1.0);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n1), ex, ey, ez);
        var r1 = Matrix44D.CreateRotation(origin, ez, alpha1);
        var tcp = r1 * m1;
        return tcp;
    }

    private static Matrix44D GetTransformation
    (
        double alpha1,
        double alpha2
    )
    {
        alpha1 = alpha1.Modulo2Pi();
        alpha2 = alpha2.Modulo2Pi();
        var origin = new Position3D(0.0, 0.0, 0.0);
        var ex = new Vector3D(1.0, 0.0, 0.0);
        var ey = new Vector3D(0.0, 1.0, 0.0);
        var ez = new Vector3D(0.0, 0.0, 1.0);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n2), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n1), ex, ey, ez);
        var r1 = Matrix44D.CreateRotation(origin, ez, alpha1);
        var tcp = r1 * (m1 * (r2 * m2));
        return tcp;
    }

    private static Matrix44D GetTransformation
    (
        double alpha1,
        double alpha2,
        double alpha3
    )
    {
        var flangeTCP = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0));
        alpha1 = alpha1.Modulo2Pi();
        alpha2 = alpha2.Modulo2Pi();
        alpha3 = alpha3.Modulo2Pi();
        var origin = new Position3D(0.0, 0.0, 0.0);
        var ex = new Vector3D(1.0, 0.0, 0.0);
        var ey = new Vector3D(0.0, 1.0, 0.0);
        var ez = new Vector3D(0.0, 0.0, 1.0);
        var m3 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n3), ex, ey, ez);
        var r3 = Matrix44D.CreateRotation(origin, ey, alpha3);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n2), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n1), ex, ey, ez);
        var r1 = Matrix44D.CreateRotation(origin, ez, alpha1);
        var tcp = r1 * (m1 * (r2 * (m2 * (r3 * m3))));
        return tcp;
    }

    private static Matrix44D GetTransformation
    (
        double alpha1,
        double alpha2,
        double alpha3,
        double alpha4
    )
    {
        var flangeTCP = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0));
        alpha1 = alpha1.Modulo2Pi();
        alpha2 = alpha2.Modulo2Pi();
        alpha3 = alpha3.Modulo2Pi();
        alpha4 = alpha4.Modulo2Pi();
        var origin = new Position3D(0.0, 0.0, 0.0);
        var ex = new Vector3D(1.0, 0.0, 0.0);
        var ey = new Vector3D(0.0, 1.0, 0.0);
        var ez = new Vector3D(0.0, 0.0, 1.0);
        var m4 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n4), ex, ey, ez);
        var r4 = Matrix44D.CreateRotation(origin, ez, alpha4);
        var m3 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n3), ex, ey, ez);
        var r3 = Matrix44D.CreateRotation(origin, ey, alpha3);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n2), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n1), ex, ey, ez);
        var r1 = Matrix44D.CreateRotation(origin, ez, alpha1);
        var tcp = r1 * (m1 * (r2 * (m2 * (r3 * (m3 * (r4 * m4))))));
        return tcp;
    }

    private static Matrix44D GetTransformation
    (
        double alpha1,
        double alpha2,
        double alpha3,
        double alpha4,
        double alpha5
    )
    {
        var flangeTCP = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0));
        alpha1 = alpha1.Modulo2Pi();
        alpha2 = alpha2.Modulo2Pi();
        alpha3 = alpha3.Modulo2Pi();
        alpha4 = alpha4.Modulo2Pi();
        alpha5 = alpha5.Modulo2Pi();
        var origin = new Position3D(0.0, 0.0, 0.0);
        var ex = new Vector3D(1.0, 0.0, 0.0);
        var ey = new Vector3D(0.0, 1.0, 0.0);
        var ez = new Vector3D(0.0, 0.0, 1.0);
        var m5 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n5), ex, ey, ez);
        var r5 = Matrix44D.CreateRotation(origin, ey, alpha5);
        var m4 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n4), ex, ey, ez);
        var r4 = Matrix44D.CreateRotation(origin, ez, alpha4);
        var m3 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n3), ex, ey, ez);
        var r3 = Matrix44D.CreateRotation(origin, ey, alpha3);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n2), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, n1), ex, ey, ez);
        var r1 = Matrix44D.CreateRotation(origin, ez, alpha1);
        var tcp = r1 * (m1 * (r2 * (m2 * (r3 * (m3 * (r4 * (m4 * (r5 * m5))))))));
        return tcp;
    }
}