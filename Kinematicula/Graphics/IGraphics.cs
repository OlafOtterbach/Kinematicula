using Kinematicula.Mathematics;
using System;

namespace Kinematicula.Graphics
{
    public interface IGraphics
    {
        public Guid Id { get; }

        public Matrix44D Frame { get; set; }
    }
}