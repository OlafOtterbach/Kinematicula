using System.Collections.Generic;

namespace Kinematicula.Graphics.Saving
{
    public interface IStateFactory
    {
        void AddCreator(IStateCreator creator);
        void AddCreators(IEnumerable<IStateCreator> creators);
        IState Create(object obj);
    }
}
