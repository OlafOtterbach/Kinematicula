namespace Kinematicula.Graphics
{
    public class Constraint
    {
        public Constraint(Anchor first, Anchor second)
        {
            First = first;
            Second = second;
            first.Body.Constraints.Add(this);
            second.Body.Constraints.Add(this);
        }

        public Anchor First { get; }

        public Anchor Second { get; }
    }
}
