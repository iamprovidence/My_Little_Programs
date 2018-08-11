using System.Drawing;

namespace FancyControls
{
    internal static class ColorsPalettes
    {
        internal const int paletteSize = 5;
        internal static Color[] getPalette(ColorsPalette palette)
        {
            switch(palette)
            {
                default:
                case ColorsPalette.None:
                    return null;
                case ColorsPalette.Solid:
                    return new Color[paletteSize]
                    {
                        Color.FromArgb(254, 135, 135),
                        Color.FromArgb(250, 179, 64),
                        Color.FromArgb(109, 153, 162),
                        Color.FromArgb(2, 128, 143),
                        Color.FromArgb(18, 76, 96)
                    };
                case ColorsPalette.Light:
                    return new Color[paletteSize]
                    {
                        Color.FromArgb(231, 85, 85),
                        Color.FromArgb(169, 225, 133),
                        Color.FromArgb(108, 220, 223),
                        Color.FromArgb(244, 223, 98),
                        Color.FromArgb(244, 124, 191)
                    };
                case ColorsPalette.Pastel:
                    return new Color[paletteSize] 
                    {
                        Color.FromArgb(248, 83, 37),
                        Color.FromArgb(129, 188, 6),
                        Color.FromArgb(255, 186, 8),
                        Color.FromArgb(5, 166, 240),
                        Color.FromArgb(204, 0, 255)
                    };
                case ColorsPalette.Electric:
                    return new Color[paletteSize]
                    {
                        Color.FromArgb(255, 249, 105),
                        Color.FromArgb(219, 213, 84),
                        Color.FromArgb(57, 255, 210),
                        Color.FromArgb(0, 225, 197),
                        Color.FromArgb(0, 182, 132)
                    };
                case ColorsPalette.BlueGold:
                    return new Color[paletteSize]
                    {
                        Color.FromArgb(197, 207, 204),
                        Color.FromArgb(254, 208, 68),
                        Color.FromArgb(29, 128, 159),
                        Color.FromArgb(12, 80, 139),
                        Color.FromArgb(90, 127, 149)
                    };
                case ColorsPalette.Vibrant:
                    return new Color[paletteSize]
                    {
                        Color.FromArgb(23, 222, 238),
                        Color.FromArgb(255, 127, 80),
                        Color.FromArgb(255, 65, 98),
                        Color.FromArgb(236, 242, 132),
                        Color.FromArgb(16, 174, 178)
                    };
                case ColorsPalette.Tropic:
                    return new Color[paletteSize]
                    {
                        Color.FromArgb(243, 97, 60),
                        Color.FromArgb(48, 40, 77),
                        Color.FromArgb(25, 154, 142),
                        Color.FromArgb(231, 29, 54),
                        Color.FromArgb(233, 212, 76)
                    };


            }
        }
    }
}
