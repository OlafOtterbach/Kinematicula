namespace RobotDemonstration.Test;

using Kinematicula.Mathematics;
using RobotDemonstration.Robot.Kinematics;
using Xunit;

public class RobotTest
{
    [Fact]
    public void Test1()
    {
        // Arrange
        double alpha1 = 30.ToRadiant();
        double alpha2 = 30.ToRadiant();
        double alpha3 = 30.ToRadiant();
        double alpha4 = 30.ToRadiant();
        double alpha5 = 30.ToRadiant();
        double alpha6 = 30.ToRadiant();
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);
        alpha1 = 29.ToRadiant();
        alpha2 = 29.ToRadiant();
        alpha3 = 29.ToRadiant();
        alpha4 = 29.ToRadiant();
        alpha5 = 29.ToRadiant();
        alpha6 = 29.ToRadiant();

        // Act
        var result = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.True(result);
        Assert.Equal(30.0, alpha1.ToDegree(), 3);
        Assert.Equal(30.0, alpha2.ToDegree(), 3);
        Assert.Equal(30.0, alpha3.ToDegree(), 3);
        Assert.Equal(30.0, alpha4.ToDegree(), 3);
        Assert.Equal(30.0, alpha5.ToDegree(), 3);
        Assert.Equal(30.0, alpha6.ToDegree(), 3);
    }

    [Fact]
    public void Test2()
    {
        // Arrange
        double alpha1 = 0.ToRadiant();
        double alpha2 = 90.ToRadiant();
        double alpha3 = -90.ToRadiant();
        double alpha4 = 0.ToRadiant();
        double alpha5 = 0.ToRadiant();
        double alpha6 = 0.ToRadiant();
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var res = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.Equal(0.0, alpha1, 3);
        Assert.Equal(90.0.ToRadiant(), alpha2, 3);
        Assert.Equal(-90.0.ToRadiant(), alpha3, 3);
        Assert.Equal(0.0, alpha4, 3);
        Assert.Equal(0.0, alpha5, 3);
        Assert.Equal(0.0, alpha6, 3);
    }

    //[Fact]
    public void Test3()
    {
        for (var alph1 = 0.0; alph1 < 359.0; alph1 += 1.0)
        {
            for (var alph2 = 0.0; alph2 < 359.0; alph2 += 1.0)
            {
                for (var alph3 = 0.0; alph3 < 359.0; alph3 += 1.0)
                {
                    for (var alph4 = 0.0; alph4 < 359.0; alph4 += 1.0)
                    {
                        for (var alph5 = 0.0; alph5 < 359.0; alph5 += 1.0)
                        {
                            for (var alph6 = 0.0; alph6 < 359.0; alph6 += 1.0)
                            {
                                var alpha11 = (alph1 + 1.0).ToRadiant();
                                var alpha12 = (alph2 + 1.0).ToRadiant();
                                var alpha13 = (alph3 + 1.0).ToRadiant();
                                var alpha14 = (alph4 + 1.0).ToRadiant();
                                var alpha15 = (alph5 + 1.0).ToRadiant();
                                var alpha16 = (alph6 + 1.0).ToRadiant();
                                var mat = RobotService.GetTransformation(alpha11, alpha12, alpha13, alpha14, alpha15, alpha16);
                                var alpha21 = (alph1).ToRadiant();
                                var alpha22 = (alph2).ToRadiant();
                                var alpha23 = (alph3).ToRadiant();
                                var alpha24 = (alph4).ToRadiant();
                                var alpha25 = (alph5).ToRadiant();
                                var alpha26 = (alph6).ToRadiant();
                                var res = RobotService.GetAxesForTransformation(mat, ref alpha21, ref alpha22, ref alpha23, ref alpha24, ref alpha25, ref alpha26);
                                var mat2 = RobotService.GetTransformation(alpha21, alpha22, alpha23, alpha24, alpha25, alpha26);

                                const double EPSILON = 0.01;
                                Assert.True(Math.Abs(mat2.A11 - mat.A11) < EPSILON);
                                Assert.True(Math.Abs(mat2.A12 - mat.A12) < EPSILON);
                                Assert.True(Math.Abs(mat2.A13 - mat.A13) < EPSILON);
                                Assert.True(Math.Abs(mat2.A14 - mat.A14) < EPSILON);

                                Assert.True(Math.Abs(mat2.A21 - mat.A21) < EPSILON);
                                Assert.True(Math.Abs(mat2.A22 - mat.A22) < EPSILON);
                                Assert.True(Math.Abs(mat2.A23 - mat.A23) < EPSILON);
                                Assert.True(Math.Abs(mat2.A24 - mat.A24) < EPSILON);

                                Assert.True(Math.Abs(mat2.A31 - mat.A31) < EPSILON);
                                Assert.True(Math.Abs(mat2.A32 - mat.A32) < EPSILON);
                                Assert.True(Math.Abs(mat2.A33 - mat.A33) < EPSILON);
                                Assert.True(Math.Abs(mat2.A34 - mat.A34) < EPSILON);

                                Assert.True(Math.Abs(mat2.A41 - mat.A41) < EPSILON);
                                Assert.True(Math.Abs(mat2.A42 - mat.A42) < EPSILON);
                                Assert.True(Math.Abs(mat2.A43 - mat.A43) < EPSILON);
                                Assert.True(Math.Abs(mat2.A44 - mat.A44) < EPSILON);
                                //Assert.True(Math.Abs(alpha11 - alpha21) < 0.1);
                                //Assert.True(Math.Abs(alpha12 - alpha22) < 0.1);
                                //Assert.True(Math.Abs(alpha13 - alpha23) < 0.1);
                                //Assert.True(Math.Abs(alpha14 - alpha24) < 0.1);
                                //Assert.True(Math.Abs(alpha15 - alpha25) < 0.1);
                                //Assert.True(Math.Abs(alpha16 - alpha26) < 0.1);
                            }
                        }
                    }
                }
            }
        }
    }

    [Fact]
    public void Test4()
    {
        // Arrange
        var alph1 = 0.0;
        var alph2 = 0.0;
        var alph3 = 0.0;
        var alph4 = 0.0;
        var alph5 = 179.0;
        var alph6 = 0.0;
        var alpha11 = (alph1 + 5.0).ToRadiant();
        var alpha12 = (alph2 + 5.0).ToRadiant();
        var alpha13 = (alph3 + 5.0).ToRadiant();
        var alpha14 = (alph4 + 5.0).ToRadiant();
        var alpha15 = (alph5 + 1.0).ToRadiant();
        var alpha16 = (alph6 + 5.0).ToRadiant();
        var mat = RobotService.GetTransformation(alpha11, alpha12, alpha13, alpha14, alpha15, alpha16);
        var alpha21 = alph1.ToRadiant();
        var alpha22 = alph2.ToRadiant();
        var alpha23 = alph3.ToRadiant();
        var alpha24 = alph4.ToRadiant();
        var alpha25 = alph5.ToRadiant();
        var alpha26 = alph6.ToRadiant();

        // Act
        var res = RobotService.GetAxesForTransformation(mat, ref alpha21, ref alpha22, ref alpha23, ref alpha24, ref alpha25, ref alpha26);
        var alpha21Deg = alpha21.ToDegree();
        var alpha22Deg = alpha22.ToDegree();
        var alpha23Deg = alpha23.ToDegree();
        var alpha24Deg = alpha24.ToDegree();
        var alpha25Deg = alpha25.ToDegree();
        var alpha26Deg = alpha26.ToDegree();
        var mat2 = RobotService.GetTransformation(alpha21, alpha22, alpha23, alpha24, alpha25, alpha26);
        var isEqual = mat == mat2;

        Assert.Equal(mat, mat2);
    }

    [Fact]
    public void PositionTest__Positions__SamePositionsOfTCP()
    {
        var offset = new Position3D(500, 0, 300);
        var ex = new Vector3D(1, 0, 0).Normalize();
        var ey = new Vector3D(0, 1, 0).Normalize();
        var ez = new Vector3D(0, 0, 1).Normalize();
        var frame = Matrix44D.CreateCoordinateSystem(offset, ex, ey, ez);
        var startPos = frame;

        // Rotation by ey
        var start = DateTime.Now;
        const double delta = 10.0;
        const double epsilon = 0.00001;
        for (double gamma = -90.0; gamma <= 90.0 + epsilon; gamma += delta)
        {
            var rotEz = Matrix44D.CreateRotation(offset, ez, gamma.ToRadiant());
            for (double alpha = -90.0; alpha <= 90.0 + epsilon; alpha += delta)
            {
                var rotEy = Matrix44D.CreateRotation(offset, ey, alpha.ToRadiant());
                for (double beta = -90.0; beta <= 90.0 + epsilon; beta += delta)
                {
                    var rotEx = Matrix44D.CreateRotation(offset, ex, beta.ToRadiant());
                    var iterPos = rotEz * (rotEy * (rotEx * startPos));

                    var alpha1 = 0.0;
                    var alpha2 = 0.0;
                    var alpha3 = 0.0;
                    var alpha4 = 0.0;
                    var alpha5 = 0.0;
                    var alpha6 = 0.0;
                    if (RobotService.GetAxesForTransformation(iterPos, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6))
                    {
                        //robot.ToolCenterPoint = iterPos;
                        //var robotTcp = robot.ToolCenterPoint;
                        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);
                        Console.Write($"{alpha1}, {alpha2}, {alpha3}, {alpha4}, {alpha5}, {alpha6}");

                        Assert.Equal(iterPos.A11, mat.A11, 3);
                        Assert.Equal(iterPos.A12, mat.A12, 3);
                        Assert.Equal(iterPos.A13, mat.A13, 3);
                        Assert.Equal(iterPos.A14, mat.A14, 3);

                        Assert.Equal(iterPos.A21, mat.A21, 3);
                        Assert.Equal(iterPos.A22, mat.A22, 3);
                        Assert.Equal(iterPos.A23, mat.A23, 3);
                        Assert.Equal(iterPos.A24, mat.A24, 3);

                        Assert.Equal(iterPos.A31, mat.A31, 3);
                        Assert.Equal(iterPos.A32, mat.A32, 3);
                        Assert.Equal(iterPos.A33, mat.A33, 3);
                        Assert.Equal(iterPos.A34, mat.A34, 3);

                        Assert.Equal(iterPos.A41, mat.A41, 3);
                        Assert.Equal(iterPos.A42, mat.A42, 3);
                        Assert.Equal(iterPos.A43, mat.A43, 3);
                        Assert.Equal(iterPos.A44, mat.A44, 3);
                    }
                }
            }
        }
        var end = DateTime.Now;
        var time = end - start;
        Console.Write("RobotTest Delta=10, Time: "); Console.WriteLine(start.ToString());
        Console.Write("RobotTest Delta=10, Time: "); Console.WriteLine(end.ToString());
        Console.Write("RobotTest Delta=10, Time: "); Console.WriteLine(time.ToString());
    }

    [Fact]
    public void Test5()
    {
        double alpha1 = 0.ToRadiant();
        double alpha2 = -0.32209312822548886; // 0.ToRadiant();
        double alpha3 = 2.0347165129926825;   // 0.ToRadiant();
        double alpha4 = 0.ToRadiant();
        double alpha5 = -0.14842251693467887; // 0.ToRadiant();
        double alpha6 = 0.ToRadiant();

        var orgAlpha1 = alpha1.ToDegree();
        var orgAlpha2 = alpha2.ToDegree();
        var orgAlpha3 = alpha3.ToDegree();
        var orgAlpha4 = alpha4.ToDegree();
        var orgAlpha5 = alpha5.ToDegree();
        var orgAlpha6 = alpha6.ToDegree();


        var shouldFrame = Matrix44D.CreateCoordinateSystem(new Position3D(125.23356628417969, 0, 409.2188720703125), new Vector3D(1, 0, -1.0579824447631836E-06), new Vector3D(1.0579824447631836E-06, 0, 1));
        var result = RobotService.GetAxesForTransformation(shouldFrame, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);
        var frame = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);


        Assert.True(frame.Ex.EqualsTo(shouldFrame.Ex, 3));
        const double EPSILON = 0.01;
        Assert.True(Math.Abs(shouldFrame.A11 - frame.A11) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A12 - frame.A12) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A13 - frame.A13) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A14 - frame.A14) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A21 - frame.A21) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A22 - frame.A22) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A23 - frame.A23) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A24 - frame.A24) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A31 - frame.A31) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A32 - frame.A32) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A33 - frame.A33) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A34 - frame.A34) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A41 - frame.A41) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A42 - frame.A42) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A43 - frame.A43) < EPSILON);
        Assert.True(Math.Abs(shouldFrame.A44 - frame.A44) < EPSILON);
    }

    [Fact]
    public void Test6()
    {
        // Arrange
        var angle = 90.0.ToRadiant();
        double alpha1 = 0.0;
        double alpha2 = angle;
        double alpha3 = 0.0;
        double alpha4 = 0.0;
        double alpha5 = 0.0;
        double alpha6 = 0.0;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var result = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.Equal(0.0, alpha1, 3);
        Assert.Equal(angle, alpha2, 3);
        Assert.Equal(0.0, alpha3, 3);
        Assert.Equal(0.0, alpha4, 3);
        Assert.Equal(0.0, alpha5, 3);
        Assert.Equal(0.0, alpha6, 3);
    }

    [Fact]
    public void Test7()
    {
        // Arrange
        var angle = 45.0.ToRadiant();
        double alpha1 = 0.0;
        double alpha2 = angle;
        double alpha3 = -angle;
        double alpha4 = angle;
        double alpha5 = 0.0;
        double alpha6 = 0.0;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var result = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.Equal(0.0, alpha1, 3);
        Assert.Equal(angle, alpha2, 3);
        Assert.Equal(-angle, alpha3, 3);
        Assert.Equal(angle, alpha4, 3);
        Assert.Equal(0.0, alpha5, 3);
        Assert.Equal(0.0, alpha6, 3);
    }

    [Fact]
    public void Test8()
    {
        // Arrange
        var angle = 45.0.ToRadiant();
        double alpha1 = angle;
        double alpha2 = angle;
        double alpha3 = angle;
        double alpha4 = 0.0;
        double alpha5 = 0.0;
        double alpha6 = 0.0;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var result = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.Equal(angle, alpha1, 3);
        Assert.Equal(angle, alpha2, 3);
        Assert.Equal(angle, alpha3, 3);
        Assert.Equal(0.0, alpha4, 3);
        Assert.Equal(0.0, alpha5, 3);
        Assert.Equal(0.0, alpha6, 3);
    }

    [Fact]
    public void Test9()
    {
        // Arrange
        var angle = 45.0.ToRadiant();
        double alpha1 = angle;
        double alpha2 = angle;
        double alpha3 = angle;
        double alpha4 = angle;
        double alpha5 = angle;
        double alpha6 = angle;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var result = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.Equal(angle, alpha1, 3);
        Assert.Equal(angle, alpha2, 3);
        Assert.Equal(angle, alpha3, 3);
        Assert.Equal(angle, alpha4, 3);
        Assert.Equal(angle, alpha5, 3);
        Assert.Equal(angle, alpha6, 3);
    }

    [Fact]
    public void Test10()
    {
        // Arrange
        var angle = 45.0.ToRadiant();
        double alpha1 = 0.0;
        double alpha2 = -angle;
        double alpha3 = -angle;
        double alpha4 = -angle;
        double alpha5 = 0.0;
        double alpha6 = 0.0;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var result = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.Equal(0.0, alpha1, 3);
        Assert.Equal(-angle, alpha2, 3);
        Assert.Equal(-angle, alpha3, 3);
        Assert.Equal(-angle, alpha4, 3);
        Assert.Equal(0.0, alpha5, 3);
        Assert.Equal(0.0, alpha6, 3);
    }

    [Fact]
    public void Test11()
    {
        // Arrange
        var angle = 45.0.ToRadiant();
        double alpha1 = 0.0;
        double alpha2 = -angle;
        double alpha3 = -angle;
        double alpha4 = -angle;
        double alpha5 = angle;
        double alpha6 = 0.0;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var result = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.Equal(0.0, alpha1, 3);
        Assert.Equal(-angle, alpha2, 3);
        Assert.Equal(-angle, alpha3, 3);
        Assert.Equal(-angle, alpha4, 3);
        Assert.Equal(angle, alpha5, 3);
        Assert.Equal(0.0, alpha6, 3);
    }

    [Fact]
    public void Test12()
    {
        // Arrange
        var angle = 90.0.ToRadiant();
        double alpha1 = -angle;
        double alpha2 = 0.0;
        double alpha3 = 0.0;
        double alpha4 = 0.0;
        double alpha5 = -angle;
        double alpha6 = 0.0;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var result = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Assert
        Assert.Equal( -angle, alpha1, 3);
        Assert.Equal( 0.0,    alpha2, 3);
        Assert.Equal( 0.0,    alpha3, 3);
        Assert.Equal( 0.0,    alpha4, 3);
        Assert.Equal( -angle, alpha5, 3);
        Assert.Equal( 0.0,    alpha6, 3);
    }

    [Fact]
    public void Test13()
    {
        // Arrange
        var alpha1 = 0.0;
        var alpha2 = 0.0;
        var alpha3 = 0.0;
        var alpha4 = 0.0;
        var alpha5 = 180.0.ToRadiant();
        var alpha6 = 0.0;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);

        // Act
        var res = RobotService.GetAxesForTransformation(mat, ref alpha1, ref alpha2, ref alpha3, ref alpha4, ref alpha5, ref alpha6);

        // Arrange
        Assert.Equal(0.0.ToRadiant(), alpha1, 3);
        Assert.Equal(0.0.ToRadiant(), alpha2, 3);
        Assert.Equal(0.0.ToRadiant(), alpha3, 3);
        Assert.Equal(0.0.ToRadiant(), alpha4, 3);
        Assert.Equal(180.0.ToRadiant(), alpha5, 3);
        Assert.Equal(0.0.ToRadiant(), alpha6, 3);
    }

    [Fact]
    public void Test14()
    {
        // Arrange
        Matrix44D shift(Matrix44D mat)
        {
            var shiftedMat = Matrix44D.CreateTranslation(new Vector3D(-1, 0, 0)) * mat;
            return shiftedMat;
        }

        var alpha1 = 0.0                    ;
        var alpha2 = 0.2939263379016005     ;
        var alpha3 = 3.1235708884478179     ;
        var alpha4 = 0.0                    ;
        var alpha5 = -1.8466998270375345    ;
        var alpha6 = 0.0;
        var mat = RobotService.GetTransformation(alpha1, alpha2, alpha3, alpha4, alpha5, alpha6);
        mat = shift(mat);
        var alpha1New = alpha1;
        var alpha2New = alpha2;
        var alpha3New = alpha3;
        var alpha4New = alpha4;
        var alpha5New = alpha5;
        var alpha6New = alpha6;

        // Act
        var res = RobotService.GetAxesForTransformation(mat, ref alpha1New, ref alpha2New, ref alpha3New, ref alpha4New, ref alpha5New, ref alpha6New);

        // Arrange
        Assert.False(res);

        Assert.Equal(alpha1, alpha1New, 3);
        Assert.Equal(alpha2, alpha2New, 3);
        Assert.Equal(alpha3, alpha3New, 3);
        Assert.Equal(alpha4, alpha4New, 3);
        Assert.Equal(alpha5, alpha5New, 3);
        Assert.Equal(alpha6, alpha6New, 3);
    }
}