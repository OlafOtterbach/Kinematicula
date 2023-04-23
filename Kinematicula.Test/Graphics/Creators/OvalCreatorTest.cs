using Kinematicula.Graphics.Creators;
using Kinematicula.Mathematics;
using Xunit;

namespace Kinematicula.Test.Graphics.Creators;

public class OvalCreatorTest
{
    [Fact]
    public void CreateTest_One_side_is_round_other_is_flat()
    {
        var oval = Oval.Create(2, 1, 50, 50, false, true, false, true, 200, 50, Matrix44D.Identity);

        Assert.NotNull(oval);
    }
}
