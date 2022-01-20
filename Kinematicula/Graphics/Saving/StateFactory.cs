using System;
using System.Linq;
using System.Collections.Generic;

namespace Kinematicula.Graphics.Saving
{
    public class StateFactory : IStateFactory
    {
        private Dictionary<Type, IStateCreator> _creators;

        public StateFactory()
        {
            var creators = new List<IStateCreator>
            {
                new BodyStateCreator(),
                new FixedConstraintStateCreator(),
                new LinearConstraintStateCreator(),
                new RotationAxisConstraintStateCreator(),
                new TelescopeRotationAxisConstraintStateCreator(),
                new TelescopeLinearConstraintStateCreator(),
            };

            _creators = creators.ToDictionary(creator => creator.GetType());
        }

        public void AddCreator(IStateCreator creator)
        {
            if (creator != null && !_creators.ContainsKey(creator.GetType()))
            {
                _creators[creator.GetType()] = creator;
            }
        }

        public void AddCreators(IEnumerable<IStateCreator> creators)
        {
            foreach (var creator in creators)
            {
                AddCreator(creator);
            }
        }

        public IState Create(object obj)
        {
            var creator = _creators.Values.Where(creator => creator.CanCreate(obj)).FirstOrDefault();
            var state = creator?.Create(obj);

            return state;
        }
    }
}