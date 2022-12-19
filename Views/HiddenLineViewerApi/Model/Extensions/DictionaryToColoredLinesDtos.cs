using System.Collections.Generic;

namespace HiddenLineViewerApi
{
    public static class DictionaryToColoredLinesDtos
    {
        public static ColoredLinesDto[] ToColoredLines(this Dictionary<ushort, ushort[]> coloredDictionary)
        {
            var coloredLinesList = new List<ColoredLinesDto>();
            foreach(var color in coloredDictionary.Keys)
            {
                coloredLinesList.Add(new ColoredLinesDto()
                {
                    Color = color,
                    DrawLines = coloredDictionary[color]
                });
            }

            var coloredLines = coloredLinesList.ToArray();

            return coloredLines;
        }
    }
}
