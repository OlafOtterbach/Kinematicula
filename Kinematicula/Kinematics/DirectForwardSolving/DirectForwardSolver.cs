using Kinematicula.Graphics;

namespace Kinematicula.Kinematics.DirectForwardSolving
{
	public abstract class DirectForwardSolver<T> : IDirectForwardSolver where T : Constraint
	{
        public void Solve(Constraint constraint, Body startEntity)
        {
			if (constraint is T concreteConstraint)
			{
				if (startEntity == concreteConstraint.First.Body)
				{
					SolveFirstToSecond(concreteConstraint);
				}

				if (startEntity == concreteConstraint.Second.Body)
				{
					SolveSecondToFirst(concreteConstraint);
				}
			}
		}

		protected abstract void SolveFirstToSecond(T concreteConstraint);
        protected abstract void SolveSecondToFirst(T concreteConstraint);
    }
}
