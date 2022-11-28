namespace Kinematicula.Graphics;

using Kinematicula.Mathematics;
using System;

public interface IGraphics
{
    public Guid Id { get; }

    public Matrix44D Frame { get; set; }
}