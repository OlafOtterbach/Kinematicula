using Kinematicula.Mathematics;
using System;

namespace Kinematicula.LogicViewing
{
    public class SelectedBodyState
    {
        public Guid SelectedBodyId { get; set; }

        public bool IsBodySelected { get; set; }

        public Position3D BodyIntersection { get; set; }
    }
}
