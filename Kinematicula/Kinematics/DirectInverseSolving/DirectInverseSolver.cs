using Kinematicula.Graphics;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
	public abstract class DirectInverseSolver<T> : IDirectInverseSolver where T : Constraint
	{
		public bool Solve(Constraint constraint, Body startEntity)
		{
			var success = true;
			if (constraint is T concreteConstraint)
			{
				if (startEntity == concreteConstraint.First.Body)
				{
					success = SolveFirstToSecond(concreteConstraint);
				}

				if (startEntity == concreteConstraint.Second.Body)
				{
					success = SolveSecondToFirst(concreteConstraint);
				}
			}

			return success;
		}

		protected abstract bool SolveFirstToSecond(T concreteConstraint);
		protected abstract bool SolveSecondToFirst(T concreteConstraint);
	}
}
