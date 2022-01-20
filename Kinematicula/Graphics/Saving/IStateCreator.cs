using System;

namespace Kinematicula.Graphics.Saving
{
    public interface IStateCreator
    {
        bool CanCreate(object obj);

        IState Create(object obj);
    }
}
