using Kinematicula.Graphics;
using Kinematicula.Graphics.Saving;

namespace Kinematicula.Kinematics.DirectInverseSolving
{
	public abstract class DirectInverseSolver<T> : IDirectInverseSolver where T : Constraint
	{
		public bool Solve(Constraint constraint, Body startEntity, Snapshot snapShot)
		{
			var success = true;
			if (constraint is T concreteConstraint)
			{
				if (startEntity == concreteConstraint.First.Body)
				{
					success = SolveFirstToSecond(concreteConstraint, snapShot);
				}

				if (startEntity == concreteConstraint.Second.Body)
				{
					success = SolveSecondToFirst(concreteConstraint, snapShot);
				}
			}

			return success;
		}

		protected abstract bool SolveFirstToSecond(T concreteConstraint, Snapshot snapShot);
		protected abstract bool SolveSecondToFirst(T concreteConstraint, Snapshot snapShot);
	}
}
