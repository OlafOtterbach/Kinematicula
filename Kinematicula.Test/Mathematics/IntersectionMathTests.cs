﻿using Kinematicula.Mathematics;
using Kinematicula.Mathematics.Extensions;
using System;
using Xunit;

namespace Kinematicula.Tests.Mathematics
{
    public class IntersectionMathTests
    {
        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest1()
        {
            Assert.True(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 1, 10, 10, 1));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest2()
        {
            Assert.True(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 5-1, 5+1, 5+1, 5-1));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest3()
        {
            Assert.False(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 20, 10, 30, 1));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest4()
        {
            Assert.False(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 10, 10, 20, 1));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest5()
        {
            Assert.False(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 1, 30, 10, 20));
        }

        [Fact]
        public void AreLinesBoundedBoxesOverlappedTest6()
        {
            Assert.False(IntersectionMath.AreLinesBoundedBoxesOverlapped(1, 1, 10, 10, 1, 20, 10, 10));
        }

        [Fact]
        public void Check2DLineWithLineTest1()
        {
            var (hasIntersection, ix, iy) = IntersectionMath.Check2DLineWithLine(0, 0, 10, 10, 0, 10, 10, 0);

            Assert.True(hasIntersection);
            Assert.Equal(5.0, ix, 2);
            Assert.Equal(5.0, iy, 2);
        }

        [Fact]
        public void Check2DLineWithLineTest2()
        {
            var (hasIntersection, ix, iy) = IntersectionMath.Check2DLineWithLine(0, 0, 10, 10, 0, 10, 4, 6);

            Assert.False(hasIntersection);
        }
        
        // [Fact]
        public void Check2DLineWithLineTest3()
        {
            var (hasIntersection, ix, iy) = IntersectionMath.Check2DLineWithLine(0, 0, 10, 10, 0, 10, 5, 5);

            Assert.False(hasIntersection);
        }

        [Fact]
        public void Check2DLineWithLineTest4()
        {
            var (hasIntersection, ix, iy) = IntersectionMath.Check2DLineWithLine(0, 0, 10, 10, 10, 10, 0, 0);

            Assert.False(hasIntersection);
        }

        [Fact]
        public void Check2DLineWithLineTest5()
        {
            var x1 = 0.0079377614887504739;
            var y1 = -0.093839459278913212;
            var x2 = 0.0651634776216613;
            var y2 = -0.046422111432387225;
            var x3 = 0.0651634776216613;
            var y3 = -0.046422111432387225;
            var x4 = 0.0079377614887504687;
            var y4 = -0.0938394592789132;

            var xvec1 = x2 - x1;
            var yvec1 = y2 - y1;
            var xvec2 = x4 - x3;
            var yvec2 = y4 - y3;

            var q1 = xvec1 / xvec2;
            var q2 = yvec1 / yvec2;

            if(q1.EqualsTo(q2))
            {
                Console.WriteLine($"{q1}");
                Console.WriteLine($"{q2}");
            }



            var (hasIntersection, ix, iy)
                = IntersectionMath.Check2DLineWithLine(x1, y1, x2, y2, x3, y3, x4, y4);
                                                      /*0.0079377614887504739, -0.093839459278913212,
                                                     //0.0079377614887504687, -0.0938394592789132
                                                       0.0651634776216613, -0.046422111432387225,
                                                       0.0651634776216613, -0.046422111432387225,
                                                       0.0079377614887504687, -0.0938394592789132);*/

            Assert.False(hasIntersection);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenLineInPlaneXYAndPosition_ThenPlumpPointIsCalculated()
        {
            var position = new Position3D(100, 100, 200);
            var axis = new Axis3D(new Position3D(), new Vector3D(1, 1, 0));

            var plump = axis.CalculatePerpendicularPoint(position);
            var plump2 = position.CalculatePerpendicularPoint(axis);

            var expected = new Position3D(100, 100, 0);
            Assert.Equal(expected, plump);
            Assert.Equal(expected, plump2);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenLineInPlaneXZAndPosition_ThenPlumpPointIsCalculated()
        {
            var position = new Position3D(100, 200, 100);
            var axis = new Axis3D(new Position3D(0, 0, 0), new Vector3D(1, 0, 1));

            var plump = axis.CalculatePerpendicularPoint(position);
            var plump2 = position.CalculatePerpendicularPoint(axis);

            var expected = new Position3D(100, 0, 100);
            Assert.Equal(expected, plump);
            Assert.Equal(expected, plump2);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenLineInPlaneYZAndPosition_ThenPlumpPointIsCalculated()
        {
            var position = new Position3D(200, 100, 100);
            var axis = new Axis3D(new Position3D(0, 0, 0), new Vector3D(0, 1, 1));

            var plump = axis.CalculatePerpendicularPoint(position);
            var plump2 = position.CalculatePerpendicularPoint(axis);

            var expected = new Position3D(0, 100, 100);
            Assert.Equal(expected, plump);
            Assert.Equal(expected, plump2);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenTwoLinesInPararllelPlaneXY_ThenPlumpPointIsCalculated()
        {
            var baseAxis = new Axis3D(new Position3D(10, 20, 0), new Vector3D(1, 0, 0));
            var secondAxis = new Axis3D(new Position3D(10, 20, 100), new Vector3D(0, 1, 0));

            var (success, plumpOnBaseAxis) = baseAxis.CalculatePerpendicularPoint(secondAxis);

            Assert.True(success);
            Assert.Equal(baseAxis.Offset, plumpOnBaseAxis);
        }

        [Fact]
        public void CalculatePerpendicularPointTest_WhenTwoCrossingLines_ThenPlumpPointIsCrossPoint()
        {
            var baseAxis = new Axis3D(new Position3D(0, 0, 100), new Vector3D(1, 1, -1));
            var secondAxis = new Axis3D(new Position3D(0, 0, -100), new Vector3D(1, 1, 1));

            var (success, plumpOnBaseAxis) = baseAxis.CalculatePerpendicularPoint(secondAxis);

            Assert.True(success);
            Assert.Equal(new Position3D(100, 100, 0), plumpOnBaseAxis);
        }

        [Fact]
        public void IntersectTest_WhenStraightLineZIntersectsPlaneXY_ThenResultIsIntersection()
        {
            var plane = new Plane3D(new Position3D(0, 0, 100), new Vector3D(0, 0, 1));
            var axis = new Axis3D(new Position3D(100, 100, 0), new Vector3D(0, 0, 1));

            var (success, intersection) = plane.Intersect(axis);
            var (success2, intersection2) = axis.Intersect(plane);

            Assert.True(success);
            Assert.Equal(new Position3D(100, 100, 100), intersection);
            Assert.True(success2);
            Assert.Equal(new Position3D(100, 100, 100), intersection2);
        }

        [Fact]
        public void IntersectTest_WhenStraightLineXIntersectsPlaneYZ_ThenResultIsIntersection()
        {
            var plane = new Plane3D(new Position3D(100, 0, 0), new Vector3D(1, 0, 0));
            var axis = new Axis3D(new Position3D(0, 100, 100), new Vector3D(1, 0, 0));

            var (success, intersection) = plane.Intersect(axis);
            var (success2, intersection2) = axis.Intersect(plane);

            Assert.True(success);
            Assert.Equal(new Position3D(100, 100, 100), intersection);
            Assert.True(success2);
            Assert.Equal(new Position3D(100, 100, 100), intersection2);
        }

        [Fact]
        public void IntersectTest_WhenStraightLineYIntersectsPlaneXZ_ThenResultIsIntersection()
        {
            var plane = new Plane3D(new Position3D(0, 100, 0), new Vector3D(0, 1, 0));
            var axis = new Axis3D(new Position3D(100, 0, 100), new Vector3D(0, 1, 0));

            var (success, intersection) = plane.Intersect(axis);
            var (success2, intersection2) = axis.Intersect(plane);

            Assert.True(success);
            Assert.Equal(new Position3D(100, 100, 100), intersection);
            Assert.True(success2);
            Assert.Equal(new Position3D(100, 100, 100), intersection2);
        }

        [Fact]
        public void IntersectTest_WhenParallelStraightLineNotIntersectsPlane_ThenResultIsEmpty()
        {
            var plane = new Plane3D(new Position3D(100, 100, 0), new Vector3D(0, 0, 1));
            var axis = new Axis3D(new Position3D(100, 100, 100), new Vector3D(1, 1, 0));

            var (success, intersection) = plane.Intersect(axis);
            var (success2, intersection2) = axis.Intersect(plane);

            Assert.False(success);
            Assert.False(success2);
        }
    }
}