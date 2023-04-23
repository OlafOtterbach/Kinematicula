namespace HiddenLineViewerApi.HiddenLine.Services;

using Kinematicula.Graphics;

public static class ColorToColorHL
{
    public static ushort ToColorHL(this Color color)
    {
        return RGBToColorHL(color.Alpha, color.Red, color.Green, color.Blue);
    }

    private static ushort RGBToColorHL(double alpha, double red, double green, double blue)
    {
        int ualpha = (((int)(alpha * 15.0)) << 12);
        int ured = (((int)(red * 15.0)) << 8);
        int ugreen = (((int)(green * 15)) << 4);
        int ublue = (int)(blue * 15);
        ushort ucolor = (ushort)(ualpha|ured | ugreen | ublue);
        return ucolor;
    }
}
