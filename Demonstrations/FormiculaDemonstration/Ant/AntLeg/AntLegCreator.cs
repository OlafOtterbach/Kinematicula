using Kinematicula.Graphics;
using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;
using Kinematicula.Scening;

namespace FormiculaDemonstration.Ant.AntLeg
{
    public static class AntLegCreator
    {
        public static AntLeg Create(Scene scene)
        {
            var antLeg = new AntLeg();
            antLeg.Name = "ant leg";

            // leg base
            var legBase = Cuboid.Create(100, 50, 100);
            legBase.Name = "leg base";
            antLeg.AddChild(legBase);

            var legBasteToAntLeg
                = new FixedConstraint(
                    new Anchor(antLeg, Matrix44D.Identity),
                    new Anchor(legBase, Matrix44D.CreateTranslation(new Vector3D(0, 0, 25))));

            // leg part one
            var legPartOne = Cuboid.Create(200, 50, 50);
            legPartOne.Name = "leg part one";
            antLeg.AddChild(legPartOne);

            legPartOne.AddSensor(new CylinderSensor(new Vector3D(0, 0, 1),legBase));
            var legPartOneToLegBase
                = new RotationAxisConstraint(
                    new Anchor(legBase, Matrix44D.CreateTranslation(new Vector3D(0,0,25))),
                    new Anchor(legPartOne, Matrix44D.CreateTranslation(new Vector3D(-75, 50, -25))),
                    0.0,
                    -45.0.ToRadiant(),
                    +45.0.ToRadiant());

            // leg part two
            var legPartTwo = Cuboid.Create(200, 50, 50);
            legPartTwo.Name = "leg part two";
            antLeg.AddChild(legPartTwo);

            legPartTwo.AddSensor(new CylinderSensor(new Vector3D(0, 1, 0), new Position3D(-75, 0, 0)));
            var legPartTwoToLegPartOne
                = new RotationAxisConstraint(
                    new Anchor(legPartOne, Matrix44D.CreateCoordinateSystem(new Position3D(75,25,0), new Vector3D(1, 0, 0), new Vector3D(0,1,0))),
                    new Anchor(legPartTwo, Matrix44D.CreateCoordinateSystem(new Position3D(-75, -25, 0), new Vector3D(1, 0, 0), new Vector3D(0, 1, 0))),
                    0.0,
                    -60.0.ToRadiant(),
                    0.0.ToRadiant());

            // leg part three
            var legPartThree = Cuboid.Create(400, 50, 50);
            legPartThree.Name = "leg part three";
            antLeg.AddChild(legPartThree);

            legPartThree.AddSensor(new CylinderSensor(new Vector3D(0, 1, 0), new Position3D(-175, 0, 0)));
            var legPartThreeToLegPartTwo
                = new RotationAxisConstraint(
                    new Anchor(legPartTwo, Matrix44D.CreateCoordinateSystem(new Position3D(75, -25, 0), new Vector3D(1, 0, 0), new Vector3D(0, 1, 0))),
                    new Anchor(legPartThree, Matrix44D.CreateCoordinateSystem(new Position3D(-175, 25, 0), new Vector3D(1, 0, 0), new Vector3D(0, 1, 0))),
                    0.0,
                    0.0.ToRadiant(),
                    170.0.ToRadiant());

            // leg part four
            var legPartFour = Cuboid.Create(200, 50, 50);
            legPartFour.Name = "leg part four";
            antLeg.AddChild(legPartFour);

            legPartFour.AddSensor(new CylinderSensor(new Vector3D(0, 1, 0), new Position3D(-75, 0, 0)));
            var legPartFourToLegPartThree
                = new RotationAxisConstraint(
                    new Anchor(legPartThree, Matrix44D.CreateCoordinateSystem(new Position3D(175, 25, 0), new Vector3D(1, 0, 0), new Vector3D(0, 1, 0))),
                    new Anchor(legPartFour, Matrix44D.CreateCoordinateSystem(new Position3D(-75, -25, 0), new Vector3D(1, 0, 0), new Vector3D(0, 1, 0))),
                    0.0,
                    -60.0.ToRadiant(),
                    60.0.ToRadiant());


            return antLeg;
        }
    }
}
