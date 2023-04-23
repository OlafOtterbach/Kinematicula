namespace Kinematicula.Scening
{
    using Kinematicula.Graphics.Memento;

    internal class FreezeScene
    {
        private readonly List<IMemento> _mementos;

        public FreezeScene(Scene scene)
        {
            var bodyMementos = scene.Bodies.Select(body => body.GetMemento()).ToList();
            var constraintMementos = scene.Bodies.SelectMany(b => b.Constraints).Distinct().Select(constraint => constraint.GetMemento()).ToList();
            _mementos = bodyMementos.Concat(constraintMementos).ToList();
        }

        public void ResetScene()
        {
            foreach(var memento in _mementos)
            {
                memento.Restore();
            }
        }
    }
}