namespace Kinematicula.Graphics;

public class Color
{
    public Color(double red, double green, double blue) : this(1.0, red, green, blue)
    { }

    public Color(double alpha, double red, double green, double blue)
    {
        Alpha = alpha;
        Red = red;
        Green = green;
        Blue = blue;
    }

    public double Alpha { get; }
    public double Red { get; }
    public double Green { get; }
    public double Blue { get; }
}
