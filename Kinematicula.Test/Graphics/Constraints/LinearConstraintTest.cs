using Kinematicula.Graphics;
using Kinematicula.Mathematics;
using System.Linq;
using Xunit;

namespace Kinematicula.Tests.Graphics.Constraints

{
    public class LinearConstraintTest
    {
        [Fact]
        public void Test1()
        {
            Create(out Body rail, out Body wagon, out LinearAxisConstraint linearConstraint);

            Assert.Single(rail.Constraints);
            Assert.Single(wagon.Constraints);
            Assert.Equal(linearConstraint, rail.Constraints.First());
            Assert.Equal(linearConstraint, wagon.Constraints.First());
        }

        private void Create(out Body rail, out Body wagon, out LinearAxisConstraint linearConstraint)
        {
            rail = new Body();
            wagon = new Body();

            linearConstraint = new LinearAxisConstraint(
                new Anchor(rail, Matrix44D.Identity),
                new Anchor(wagon, Matrix44D.Identity));
        }
    }
}
