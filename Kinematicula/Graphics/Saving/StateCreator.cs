using System;

namespace Kinematicula.Graphics.Saving
{
    public abstract class StateCreator<T> : IStateCreator
    {
        public bool CanCreate(object obj) => obj is T;

        public IState Create(object obj) => obj is T ? Create((T)obj) : null;

        protected abstract IState Create(T obj);
    }
}
