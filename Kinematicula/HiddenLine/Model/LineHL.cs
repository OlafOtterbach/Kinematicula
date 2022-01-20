using System;

namespace Kinematicula.HiddenLineGraphics.Model
{
    public class LineHL
    {
        public PointHL Start { get; set; }
        public PointHL End { get; set; }

        public ushort Color => Edge.Color;

        public EdgeHL Edge { get; set; }

        public double Length
        {
            get
            {
                var deltaX = Math.Abs(End.X - Start.X);
                var deltaY = Math.Abs(End.Y - Start.Y);
                var length = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                return length;
            }
        }
    }
}