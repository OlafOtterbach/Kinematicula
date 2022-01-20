namespace Kinematicula.Graphics.Saving
{
    public abstract class State<T> : IState
    {
        protected State(T target)
        {
            Target = target;
        }

        public T Target { get; }

        public abstract void Restore();
    }
}
