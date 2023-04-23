using Kinematicula.Graphics.Memento;

namespace Kinematicula.Graphics
{
    public class FixedConstraint : Constraint
    {
        public FixedConstraint(Anchor first, Anchor second) : base(first, second)
        { }

        public override IMemento GetMemento()
        {
            return new FixedConstraintMemento();
        }
    }

    public class FixedConstraintMemento : IMemento
    {
        public void Restore() { }
    }
}
