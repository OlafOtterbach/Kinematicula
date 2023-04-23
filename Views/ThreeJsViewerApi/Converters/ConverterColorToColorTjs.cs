namespace ThreeJsViewerApi.Converters;

using Kinematicula.Graphics;

public static class ConverterColorToColorTjs
{
    public static uint ToColorTjs(this Color color)
    {
        uint alpha = ((uint)(color.Alpha * 255.0)) * 16777216;
        uint red = (uint)(color.Red * 255.0) * 65536;
        uint green = (uint)(color.Green * 255.0) * 256;
        uint blue = (uint)(color.Blue * 255.0);

        uint colorTjs = alpha + red + green + blue;

        return colorTjs;
    }
}
