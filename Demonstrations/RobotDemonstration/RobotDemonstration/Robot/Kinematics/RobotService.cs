namespace RobotDemonstration.Robot.Kinematics;

using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using System;


// Six axes robot
//
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
//     --- Alpha1    public class RobotService
//
public class RobotService
{
    private const double SHOULDERLENGTH = 240.0; // Shoulder
    private const double UPPERARMLENGTH = 200.0; // Upperarm
    private const double FOREARMLENGTH = 100.0; // Forearm
    private const double ULNALENGTH = 45.0; // Ulna
    private const double WRISTLENGTH = 35; // Wrist
    private const double ADAPTERLENGTH = 10; // Adapter

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
        // Transform from gripper frame to adapter frame
        var flangeTCP = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, 0.0), new Vector3D(0.0, 0.0, 1.0), new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0));
        var mat = flangeTCP.Inverse();
        var adapterFrame = transformation * mat;

        // Calculate angles for adapter frame
        var newAlpha1 = CalculateAlpha1(alpha1, adapterFrame, WRISTLENGTH, ADAPTERLENGTH);
        if (!TryCalculateAlpha2(alpha2, adapterFrame, WRISTLENGTH, ADAPTERLENGTH, newAlpha1, out double newAlpha2)) return false;
        var newAlpha3 = CalculateAlpha3(alpha3, adapterFrame, WRISTLENGTH, ADAPTERLENGTH, newAlpha1, newAlpha2);
        var newAlpha4 = CalculateAlpha4(alpha4, adapterFrame, newAlpha1, newAlpha2, newAlpha3);
        var newAlpha5 = CalculateAlpha5(alpha5, adapterFrame, newAlpha1, newAlpha2, newAlpha3, newAlpha4);
        var newAlpha6 = CalculateAlpha6(alpha6, adapterFrame, newAlpha1, newAlpha2, newAlpha3, newAlpha4, newAlpha5);
        alpha1 = newAlpha1;
        alpha2 = newAlpha2;
        alpha3 = newAlpha3;
        alpha4 = newAlpha4;
        alpha5 = newAlpha5;
        alpha6 = newAlpha6;

        var newAlpha1Deg = newAlpha1.ToDegree();
        var newAlpha2Deg = newAlpha2.ToDegree();
        var newAlpha3Deg = newAlpha3.ToDegree();
        var newAlpha4Deg = newAlpha4.ToDegree();
        var newAlpha5Deg = newAlpha5.ToDegree();
        var newAlpha6Deg = newAlpha6.ToDegree();

        return true;
    }


    // Calculation of alpha 1
    //
	// View on Z-Axis of base socket of robot.
	// 
    //            O wrist
    //           /
    //        | /
    //        |/ Alpha1
    // Socket O---
    //
    private static double CalculateAlpha1(
        double orgAlpha1,
        Matrix44D adapterFrame,
        double wristLength,
        double adapterLength)
    {
        var wristPosition = adapterFrame.Offset - adapterFrame.Ez * (wristLength + adapterLength);
        var x1 = wristPosition.X;
        var y1 = wristPosition.Y;
        double newAlpha1;
        if (x1.EqualsTo(0.0) && y1.EqualsTo(0.0))
        {
            newAlpha1 = orgAlpha1;
        }
        else
        {
            var anglePosWristToExOfOrigin = AngleMath.FindNextToPiAngleBasingOnAngle((x1, y1).ToAngle(), orgAlpha1);
            newAlpha1 = anglePosWristToExOfOrigin;
        }

        return newAlpha1;
    }


    // Calculation of alpha 2
    //
    // Side view in y direction of upper arm base. Is view in second axis direction.
    //      
    //                             C 
    //                             O  UpperArm
    //                            /|\
    //                           / | \
    //                          /  |  \
    //                         /   |   \
    //     UPPERARMLENGTH = b /    |    \  a = FOREARMLENGTH + ULNALENGTH
    //                       /     | y   \
    //                  |   /      |      \
    //               alpha2/       |       \
    //                  | /        |        \
    //                  |/ alpha  .|         \
    //       Shoulder A O----------*----------O C-B Wrist
    //                   .   x     .   c-x   .
    //                    .        .        .
    //                     .       .       .
    //                      .      .      .
    //                       .     .-y   .
    //                        .    .    .
    //                         .   .   .
    //                          .  .  .
    //                           . . .
    //                            ...
    //                             O   UpperArm'
    //                             B'
    //	     		
    //	                |---------------------|
    //                             c = Length(Wrist - Shoulder)
    //
    // Using detecting intersection between two circles with radius a and b with cosine theorem to calculate alpha 2.
    //     a^2 = c^2 + b^2 - 2*b*c * cos(Alpha)
    // <=> 2*b*c * cos(Alpha) = b^2 + c^2 - a^2
    // <=> b * cos(Alpha) = (b^2 + c^2 - a^2) / (2 * c) AND  x = b * cos(Alpha)
    // <=> x = (b^2 + c^2 - a^2) / (2 * c)
    //
    //     x = (b^2 + c^2 - a^2) / (2 * c)
    //     y = b^2 - x^2
    //  => C(x,y) = Upperarm(x,y)
    //  => alpha2 = Angle(Ez(Shoulder), (Shoulder - Upperarm))
    //
    private static bool TryCalculateAlpha2(
        double orgAlpha2,
        Matrix44D adapterFrame,
        double wristLength,
        double adapterLength,
        double newAlpha1,
        out double newAlpha2)
    {
        newAlpha2 = 0.0;

        var wristPosition = adapterFrame.Offset - adapterFrame.Ez * (wristLength + adapterLength);
        var shoulderFrame = GetTransformation(newAlpha1);
        var transformationToShoulderFrame = shoulderFrame.Inverse();
        var shoulderToWristVector = (transformationToShoulderFrame * wristPosition).ToVector3D();

        var a = FOREARMLENGTH + ULNALENGTH;
        var b = UPPERARMLENGTH;
        var c = shoulderToWristVector.Length;
        var distanceX = (b * b + c * c - a * a) / (2 * c);
        var distanceYSquared = b * b - distanceX * distanceX;
        distanceYSquared = Math.Abs(distanceYSquared) < ConstantsMath.Epsilon ? 0.0 : distanceYSquared;
        if (distanceYSquared < 0.0)
        {
            // Robot transformation not possible to reach by alpha 2.
            return false;
        }

        var distanceY = Math.Sqrt(distanceYSquared);
        var xVec = distanceX * shoulderToWristVector.Normalize();
        var yVec = distanceY * (shoulderToWristVector & new Vector3D(0, 1, 0)).Normalize();

        var shoulderToUpperArmVector1 = xVec + yVec;
        var shoulderToUpperArmVector2 = xVec - yVec;

        var ez = new Vector3D(0, 0, 1);
        var eyRotationAxis = new Vector3D(0, 1, 0);

        var newAlpha2A = ez.CounterClockwiseAngleWith(shoulderToUpperArmVector1, eyRotationAxis);
        var newAlpha2B = ez.CounterClockwiseAngleWith(shoulderToUpperArmVector2, eyRotationAxis);
        var newAlpha2C = GetBestAngle(newAlpha2A, newAlpha2B, orgAlpha2);

        newAlpha2 = AngleMath.FindNextToPi2AngleBasingOnAngle(newAlpha2C, orgAlpha2);

        return true;
    }


    // Calculation of alpha 3
	//
    // Side view in y direction of forearm base. Is view in third axis direction.
	//
    //                          /
    //                         /
    //               UpperArm O  alpha 3 
    //                       / \
    //                      /   \
    //                     /     \
    //                    /       \
    //    UPPERARMLENGTH /         \  FOREARMLENGTH + ULNALENGTH
    //                  /           \
    //             |   /             \
    //          alpha2/               \
    //             | /                 \
    //             |/                   \
    //  Shoulder A O----------*----------O C Wrist
    //			
    private static double CalculateAlpha3(
        double orgAlpha3,
        Matrix44D adapterFrame,
        double wristLength,
        double adapterLength,
        double newAlpha1,
        double newAlpha2)
    {
        var wristPosition = adapterFrame.Offset - adapterFrame.Ez * (wristLength + adapterLength);

        var upperArmFrame = GetTransformation(newAlpha1, newAlpha2);
        var transformationToUpperArmFrame = upperArmFrame.Inverse();
        var upperArmToWristVector = (transformationToUpperArmFrame * wristPosition).ToVector3D();

        var ez3 = new Vector3D(0, 0, 1);
        var ey3RotationAxis = new Vector3D(0, 1, 0);

        var newAlpha3A = ez3.CounterClockwiseAngleWith(upperArmToWristVector, ey3RotationAxis);
        var newAlpha3 = AngleMath.FindNextToPi2AngleBasingOnAngle(newAlpha3A, orgAlpha3);

        return newAlpha3;
    }


    // Calculation of alpha 4
    //     
	// View on Z-Axis of fore arm of robot.
	//
    //      /Adapter
    //     /
    //  | /
    //  |/ Alpha4
    //  O--- ForeArmFrame
    //
    private static double CalculateAlpha4(
        double orgAlpha4,
        Matrix44D adapterFrame,
        double newAlpha1,
        double newAlpha2,
        double newAlpha3)
    {
        var foreArmFrame = GetTransformation(newAlpha1, newAlpha2, newAlpha3);
        var transformationToForeArmFrame = foreArmFrame.Inverse();
        var adapterVectorInForeArmFrame = transformationToForeArmFrame * adapterFrame.Ez;

        double newAlpha4;
        if (adapterVectorInForeArmFrame.X.EqualsTo(0.0) && adapterVectorInForeArmFrame.Y.EqualsTo(0.0))
        {
            newAlpha4 = orgAlpha4;
        }
        else
        {
            var anglePosAdapterToForeArm = AngleMath.FindNextToPiAngleBasingOnAngle((adapterVectorInForeArmFrame.X, adapterVectorInForeArmFrame.Y).ToAngle(), orgAlpha4);
            newAlpha4 = anglePosAdapterToForeArm;
        }

        return newAlpha4;
    }


    // Calculation of alpha 5
    //
    // Side view in y direction of wrist base. Is view in fifth axis direction.
    //	
    //                                     /Adapter 
    //                                    \ 
    //                                   / 
    //                                  / WRISTLENGTH (Wrist)
    //                                 /
    //                                /.....
    //                               /      ..
    //     (Forearm)      (Ulna)    /alpha5   .   
    //  O-------------|------------O------------->
    //
    private static double CalculateAlpha5(
        double orgAlpha5,
        Matrix44D adapterFrame,
        double newAlpha1,
        double newAlpha2,
        double newAlpha3,
        double newAlpha4)
    {
        var ulnaFrame = GetTransformation(newAlpha1, newAlpha2, newAlpha3, newAlpha4);
        var transformationToUlnaFrame = ulnaFrame.Inverse();
        var adapterVectorInUlnaFrame = transformationToUlnaFrame * adapterFrame.Ez;
        double angleDirectionAdapterToUlnaDirection = (adapterVectorInUlnaFrame.Z, adapterVectorInUlnaFrame.X).ToAngle();
        var newAlpha5 = AngleMath.FindNextToPiAngleBasingOnAngle(angleDirectionAdapterToUlnaDirection, orgAlpha5);

        return newAlpha5;
    }


    // Calculation of alpha 6
    //    
	// View on Z-Axis of asapter of robot.
	//
    //      /Adapter
    //     /
    //  | /
    //  |/ Alpha4
    //  O--- Wrist
    //
    private static double CalculateAlpha6(
        double orgAlpha6,
        Matrix44D adapterFrame,
        double newAlpha1,
        double newAlpha2,
        double newAlpha3,
        double newAlpha4,
        double newAlpha5)
    {
        var wristFrame = GetTransformation(newAlpha1, newAlpha2, newAlpha3, newAlpha4, newAlpha5);
        var transformationToWristFrame = wristFrame.Inverse();
        var adapterVectorInWristFrame = transformationToWristFrame * adapterFrame.Ex;

        double angleExAdapterToWristEx = (adapterVectorInWristFrame.X, adapterVectorInWristFrame.Y).ToAngle();
        var newAlpha6 = AngleMath.FindNextToPiAngleBasingOnAngle(angleExAdapterToWristEx, orgAlpha6);

        return newAlpha6;
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
        var m6 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, ADAPTERLENGTH), ex, ey, ez);
        var r6 = Matrix44D.CreateRotation(origin, ez, alpha6);
        var m5 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, WRISTLENGTH), ex, ey, ez);
        var r5 = Matrix44D.CreateRotation(origin, ey, alpha5);
        var m4 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, ULNALENGTH), ex, ey, ez);
        var r4 = Matrix44D.CreateRotation(origin, ez, alpha4);
        var m3 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, FOREARMLENGTH), ex, ey, ez);
        var r3 = Matrix44D.CreateRotation(origin, ey, alpha3);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, UPPERARMLENGTH), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, SHOULDERLENGTH), ex, ey, ez);
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
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, SHOULDERLENGTH), ex, ey, ez);
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
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, UPPERARMLENGTH), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, SHOULDERLENGTH), ex, ey, ez);
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
        var m3 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, FOREARMLENGTH), ex, ey, ez);
        var r3 = Matrix44D.CreateRotation(origin, ey, alpha3);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, UPPERARMLENGTH), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, SHOULDERLENGTH), ex, ey, ez);
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
        var m4 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, ULNALENGTH), ex, ey, ez);
        var r4 = Matrix44D.CreateRotation(origin, ez, alpha4);
        var m3 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, FOREARMLENGTH), ex, ey, ez);
        var r3 = Matrix44D.CreateRotation(origin, ey, alpha3);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, UPPERARMLENGTH), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, SHOULDERLENGTH), ex, ey, ez);
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
        var m5 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, WRISTLENGTH), ex, ey, ez);
        var r5 = Matrix44D.CreateRotation(origin, ey, alpha5);
        var m4 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, ULNALENGTH), ex, ey, ez);
        var r4 = Matrix44D.CreateRotation(origin, ez, alpha4);
        var m3 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, FOREARMLENGTH), ex, ey, ez);
        var r3 = Matrix44D.CreateRotation(origin, ey, alpha3);
        var m2 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, UPPERARMLENGTH), ex, ey, ez);
        var r2 = Matrix44D.CreateRotation(origin, ey, alpha2);
        var m1 = Matrix44D.CreateCoordinateSystem(new Position3D(0.0, 0.0, SHOULDERLENGTH), ex, ey, ez);
        var r1 = Matrix44D.CreateRotation(origin, ez, alpha1);
        var tcp = r1 * (m1 * (r2 * (m2 * (r3 * (m3 * (r4 * (m4 * (r5 * m5))))))));
        return tcp;
    }
}
