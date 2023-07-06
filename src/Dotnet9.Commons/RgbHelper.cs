namespace Dotnet9.Commons;

public static class RgbHelper
{
    public static List<ColorCategory> Colors = new()
    {
        new ColorCategory
        {
            CategoryName = "gray",
            Shades = new List<ColorItem>
            {
                new() { Name = "gainsboro", HexCode = "#dcdcdc", Rgb = "220, 220, 220", Hsl = "0, 0%, 86%" },
                new() { Name = "lightgray", HexCode = "#d3d3d3", Rgb = "211, 211, 211", Hsl = "0, 0%, 83%" },
                new() { Name = "silver", HexCode = "#c0c0c0", Rgb = "192, 192, 192", Hsl = "0, 0%, 75%" },
                new() { Name = "darkgray", HexCode = "#a9a9a9", Rgb = "169, 169, 169", Hsl = "0, 0%, 66%" },
                new() { Name = "gray", HexCode = "#808080", Rgb = "128, 128, 128", Hsl = "0, 0%, 50%" },
                new() { Name = "dimgray", HexCode = "#696969", Rgb = "105, 105, 105", Hsl = "0, 0%, 41%" },
                new()
                {
                    Name = "lightslategray", HexCode = "#778899", Rgb = "119, 136, 153", Hsl = "210, 14%, 53%"
                },
                new() { Name = "slategray", HexCode = "#708090", Rgb = "112, 128, 144", Hsl = "210, 13%, 50%" },
                new()
                    { Name = "darkslategray", HexCode = "#2f4f4f", Rgb = "47, 79, 79", Hsl = "180, 25%, 25%" },
                new() { Name = "black", HexCode = "#000000", Rgb = "0, 0, 0", Hsl = "0, 0%, 0%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "red",
            Shades = new List<ColorItem>
            {
                new() { Name = "indianred", HexCode = "#cd5c5c", Rgb = "205, 92, 92", Hsl = "0, 53%, 58%" },
                new() { Name = "lightcoral", HexCode = "#f08080", Rgb = "240, 128, 128", Hsl = "0, 79%, 72%" },
                new() { Name = "salmon", HexCode = "#fa8072", Rgb = "250, 128, 114", Hsl = "6, 93%, 71%" },
                new() { Name = "darksalmon", HexCode = "#e9967a", Rgb = "233, 150, 122", Hsl = "15, 72%, 70%" },
                new()
                    { Name = "lightsalmon", HexCode = "#ffa07a", Rgb = "255, 160, 122", Hsl = "17, 100%, 74%" },
                new() { Name = "crimson", HexCode = "#dc143c", Rgb = "220, 20, 60", Hsl = "348, 83%, 47%" },
                new() { Name = "red", HexCode = "#ff0000", Rgb = "255, 0, 0", Hsl = "0, 100%, 50%" },
                new() { Name = "firebrick", HexCode = "#b22222", Rgb = "178, 34, 34", Hsl = "0, 68%, 42%" },
                new() { Name = "darkred", HexCode = "#8b0000", Rgb = "139, 0, 0", Hsl = "0, 100%, 27%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "orange",
            Shades = new List<ColorItem>
            {
                new()
                    { Name = "lightsalmon", HexCode = "#ffa07a", Rgb = "255, 160, 122", Hsl = "17, 100%, 74%" },
                new() { Name = "coral", HexCode = "#ff7f50", Rgb = "255, 127, 80", Hsl = "16, 100%, 66%" },
                new() { Name = "tomato", HexCode = "#ff6347", Rgb = "255, 99, 71", Hsl = "9, 100%, 64%" },
                new() { Name = "orangered", HexCode = "#ff4500", Rgb = "255, 69, 0", Hsl = "16, 100%, 50%" },
                new() { Name = "darkorange", HexCode = "#ff8c00", Rgb = "255, 140, 0", Hsl = "33, 100%, 50%" },
                new() { Name = "orange", HexCode = "#ffa500", Rgb = "255, 165, 0", Hsl = "39, 100%, 50%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "brown",
            Shades = new List<ColorItem>
            {
                new() { Name = "cornsilk", HexCode = "#fff8dc", Rgb = "255, 248, 220", Hsl = "48, 100%, 93%" },
                new()
                {
                    Name = "blanchedalmond", HexCode = "#ffebcd", Rgb = "255, 235, 205", Hsl = "36, 100%, 90%"
                },
                new() { Name = "bisque", HexCode = "#ffe4c4", Rgb = "255, 228, 196", Hsl = "33, 100%, 88%" },
                new()
                    { Name = "navajowhite", HexCode = "#ffdead", Rgb = "255, 222, 173", Hsl = "36, 100%, 84%" },
                new() { Name = "wheat", HexCode = "#f5deb3", Rgb = "245, 222, 179", Hsl = "39, 77%, 83%" },
                new() { Name = "burlywood", HexCode = "#deb887", Rgb = "222, 184, 135", Hsl = "34, 57%, 70%" },
                new() { Name = "tan", HexCode = "#d2b48c", Rgb = "210, 180, 140", Hsl = "34, 44%, 69%" },
                new() { Name = "rosybrown", HexCode = "#bc8f8f", Rgb = "188, 143, 143", Hsl = "0, 25%, 65%" },
                new() { Name = "sandybrown", HexCode = "#f4a460", Rgb = "244, 164, 96", Hsl = "28, 87%, 67%" },
                new() { Name = "goldenrod", HexCode = "#daa520", Rgb = "218, 165, 32", Hsl = "43, 74%, 49%" },
                new()
                    { Name = "darkgoldenrod", HexCode = "#b8860b", Rgb = "184, 134, 11", Hsl = "43, 89%, 38%" },
                new() { Name = "peru", HexCode = "#cd853f", Rgb = "205, 133, 63", Hsl = "30, 59%, 53%" },
                new() { Name = "chocolate", HexCode = "#d2691e", Rgb = "210, 105, 30", Hsl = "25, 75%, 47%" },
                new() { Name = "saddlebrown", HexCode = "#8b4513", Rgb = "139, 69, 19", Hsl = "25, 76%, 31%" },
                new() { Name = "sienna", HexCode = "#a0522d", Rgb = "160, 82, 45", Hsl = "19, 56%, 40%" },
                new() { Name = "brown", HexCode = "#a52a2a", Rgb = "165, 42, 42", Hsl = "0, 59%, 41%" },
                new() { Name = "maroon", HexCode = "#800000", Rgb = "128, 0, 0", Hsl = "0, 100%, 25%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "blue",
            Shades = new List<ColorItem>
            {
                new() { Name = "aqua", HexCode = "#00ffff", Rgb = "0, 255, 255", Hsl = "180, 100%, 50%" },
                new() { Name = "cyan", HexCode = "#00ffff", Rgb = "0, 255, 255", Hsl = "180, 100%, 50%" },
                new()
                    { Name = "lightcyan", HexCode = "#e0ffff", Rgb = "224, 255, 255", Hsl = "180, 100%, 94%" },
                new()
                    { Name = "paleturquoise", HexCode = "#afeeee", Rgb = "175, 238, 238", Hsl = "180, 65%, 81%" },
                new()
                    { Name = "aquamarine", HexCode = "#7fffd4", Rgb = "127, 255, 212", Hsl = "160, 100%, 75%" },
                new() { Name = "turquoise", HexCode = "#40e0d0", Rgb = "64, 224, 208", Hsl = "174, 72%, 56%" },
                new()
                {
                    Name = "mediumturquoise", HexCode = "#48d1cc", Rgb = "72, 209, 204", Hsl = "178, 60%, 55%"
                },
                new()
                    { Name = "darkturquoise", HexCode = "#00ced1", Rgb = "0, 206, 209", Hsl = "181, 100%, 41%" },
                new() { Name = "cadetblue", HexCode = "#5f9ea0", Rgb = "95, 158, 160", Hsl = "182, 25%, 50%" },
                new() { Name = "steelblue", HexCode = "#4682b4", Rgb = "70, 130, 180", Hsl = "207, 44%, 49%" },
                new()
                {
                    Name = "lightsteelblue", HexCode = "#b0c4de", Rgb = "176, 196, 222", Hsl = "214, 41%, 78%"
                },
                new()
                    { Name = "powderblue", HexCode = "#b0e0e6", Rgb = "176, 224, 230", Hsl = "187, 52%, 80%" },
                new() { Name = "lightblue", HexCode = "#add8e6", Rgb = "173, 216, 230", Hsl = "195, 53%, 79%" },
                new() { Name = "skyblue", HexCode = "#87ceeb", Rgb = "135, 206, 235", Hsl = "197, 71%, 73%" },
                new()
                    { Name = "lightskyblue", HexCode = "#87cefa", Rgb = "135, 206, 250", Hsl = "203, 92%, 75%" },
                new()
                    { Name = "deepskyblue", HexCode = "#00bfff", Rgb = "0, 191, 255", Hsl = "195, 100%, 50%" },
                new()
                    { Name = "dodgerblue", HexCode = "#1e90ff", Rgb = "30, 144, 255", Hsl = "210, 100%, 56%" },
                new()
                {
                    Name = "cornflowerblue", HexCode = "#6495ed", Rgb = "100, 149, 237", Hsl = "219, 79%, 66%"
                },
                new()
                {
                    Name = "mediumslateblue", HexCode = "#7b68ee", Rgb = "123, 104, 238", Hsl = "249, 80%, 67%"
                },
                new() { Name = "royalblue", HexCode = "#4169e1", Rgb = "65, 105, 225", Hsl = "225, 73%, 57%" },
                new() { Name = "blue", HexCode = "#0000ff", Rgb = "0, 0, 255", Hsl = "240, 100%, 50%" },
                new() { Name = "mediumblue", HexCode = "#0000cd", Rgb = "0, 0, 205", Hsl = "240, 100%, 40%" },
                new() { Name = "darkblue", HexCode = "#00008b", Rgb = "0, 0, 139", Hsl = "240, 100%, 27%" },
                new() { Name = "navy", HexCode = "#000080", Rgb = "0, 0, 128", Hsl = "240, 100%, 25%" },
                new() { Name = "midnightblue", HexCode = "#191970", Rgb = "25, 25, 112", Hsl = "240, 64%, 27%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "green",
            Shades = new List<ColorItem>
            {
                new()
                    { Name = "greenyellow", HexCode = "#adff2f", Rgb = "173, 255, 47", Hsl = "84, 100%, 59%" },
                new() { Name = "chartreuse", HexCode = "#7fff00", Rgb = "127, 255, 0", Hsl = "90, 100%, 50%" },
                new() { Name = "lawngreen", HexCode = "#7cfc00", Rgb = "124, 252, 0", Hsl = "90, 100%, 49%" },
                new() { Name = "lime", HexCode = "#00ff00", Rgb = "0, 255, 0", Hsl = "120, 100%, 50%" },
                new() { Name = "limegreen", HexCode = "#32cd32", Rgb = "50, 205, 50", Hsl = "120, 61%, 50%" },
                new() { Name = "palegreen", HexCode = "#98fb98", Rgb = "152, 251, 152", Hsl = "120, 93%, 79%" },
                new()
                    { Name = "lightgreen", HexCode = "#90ee90", Rgb = "144, 238, 144", Hsl = "120, 73%, 75%" },
                new()
                {
                    Name = "mediumspringgreen", HexCode = "#00fa9a", Rgb = "0, 250, 154", Hsl = "157, 100%, 49%"
                },
                new()
                    { Name = "springgreen", HexCode = "#00ff7f", Rgb = "0, 255, 127", Hsl = "150, 100%, 50%" },
                new()
                    { Name = "mediumseagreen", HexCode = "#3cb371", Rgb = "60, 179, 113", Hsl = "147, 50%, 47%" },
                new() { Name = "seagreen", HexCode = "#2e8b57", Rgb = "46, 139, 87", Hsl = "146, 50%, 36%" },
                new() { Name = "forestgreen", HexCode = "#228b22", Rgb = "34, 139, 34", Hsl = "120, 61%, 34%" },
                new() { Name = "green", HexCode = "#008000", Rgb = "0, 128, 0", Hsl = "120, 100%, 25%" },
                new() { Name = "darkgreen", HexCode = "#006400", Rgb = "0, 100, 0", Hsl = "120, 100%, 20%" },
                new() { Name = "yellowgreen", HexCode = "#9acd32", Rgb = "154, 205, 50", Hsl = "80, 61%, 50%" },
                new() { Name = "olivedrab", HexCode = "#6b8e23", Rgb = "107, 142, 35", Hsl = "80, 60%, 35%" },
                new() { Name = "olive", HexCode = "#808000", Rgb = "128, 128, 0", Hsl = "60, 100%, 25%" },
                new()
                    { Name = "darkolivegreen", HexCode = "#556b2f", Rgb = "85, 107, 47", Hsl = "82, 39%, 30%" },
                new()
                {
                    Name = "mediumaquamarine", HexCode = "#66cdaa", Rgb = "102, 205, 170", Hsl = "160, 51%, 60%"
                },
                new()
                    { Name = "darkseagreen", HexCode = "#8fbc8b", Rgb = "143, 188, 139", Hsl = "115, 27%, 64%" },
                new()
                    { Name = "lightseagreen", HexCode = "#20b2aa", Rgb = "32, 178, 170", Hsl = "177, 70%, 41%" },
                new() { Name = "darkcyan", HexCode = "#008b8b", Rgb = "0, 139, 139", Hsl = "180, 100%, 27%" },
                new() { Name = "teal", HexCode = "#008080", Rgb = "0, 128, 128", Hsl = "180, 100%, 25%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "pink",
            Shades = new List<ColorItem>
            {
                new() { Name = "pink", HexCode = "#ffc0cb", Rgb = "255, 192, 203", Hsl = "350, 100%, 88%" },
                new()
                    { Name = "lightpink", HexCode = "#ffb6c1", Rgb = "255, 182, 193", Hsl = "351, 100%, 86%" },
                new() { Name = "hotpink", HexCode = "#ff69b4", Rgb = "255, 105, 180", Hsl = "330, 100%, 71%" },
                new() { Name = "deeppink", HexCode = "#ff1493", Rgb = "255, 20, 147", Hsl = "328, 100%, 54%" },
                new()
                {
                    Name = "mediumvioletred", HexCode = "#c71585", Rgb = "199, 21, 133", Hsl = "322, 81%, 43%"
                },
                new()
                    { Name = "palevioletred", HexCode = "#db7093", Rgb = "219, 112, 147", Hsl = "340, 60%, 65%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "purple",
            Shades = new List<ColorItem>
            {
                new() { Name = "lavender", HexCode = "#e6e6fa", Rgb = "230, 230, 250", Hsl = "240, 67%, 94%" },
                new() { Name = "thistle", HexCode = "#d8bfd8", Rgb = "216, 191, 216", Hsl = "300, 24%, 80%" },
                new() { Name = "plum", HexCode = "#dda0dd", Rgb = "221, 160, 221", Hsl = "300, 47%, 75%" },
                new() { Name = "violet", HexCode = "#ee82ee", Rgb = "238, 130, 238", Hsl = "300, 76%, 72%" },
                new() { Name = "orchid", HexCode = "#da70d6", Rgb = "218, 112, 214", Hsl = "302, 59%, 65%" },
                new() { Name = "fuchsia", HexCode = "#ff00ff", Rgb = "255, 0, 255", Hsl = "300, 100%, 50%" },
                new() { Name = "magenta", HexCode = "#ff00ff", Rgb = "255, 0, 255", Hsl = "300, 100%, 50%" },
                new()
                    { Name = "mediumorchid", HexCode = "#ba55d3", Rgb = "186, 85, 211", Hsl = "288, 59%, 58%" },
                new()
                    { Name = "mediumpurple", HexCode = "#9370db", Rgb = "147, 112, 219", Hsl = "260, 60%, 65%" },
                new()
                    { Name = "rebeccapurple", HexCode = "#663399", Rgb = "102, 51, 153", Hsl = "270, 50%, 40%" },
                new() { Name = "blueviolet", HexCode = "#8a2be2", Rgb = "138, 43, 226", Hsl = "271, 76%, 53%" },
                new() { Name = "darkviolet", HexCode = "#9400d3", Rgb = "148, 0, 211", Hsl = "282, 100%, 41%" },
                new() { Name = "darkorchid", HexCode = "#9932cc", Rgb = "153, 50, 204", Hsl = "280, 61%, 50%" },
                new()
                    { Name = "darkmagenta", HexCode = "#8b008b", Rgb = "139, 0, 139", Hsl = "300, 100%, 27%" },
                new() { Name = "purple", HexCode = "#800080", Rgb = "128, 0, 128", Hsl = "300, 100%, 25%" },
                new() { Name = "indigo", HexCode = "#4b0082", Rgb = "75, 0, 130", Hsl = "275, 100%, 25%" },
                new() { Name = "slateblue", HexCode = "#6a5acd", Rgb = "106, 90, 205", Hsl = "248, 53%, 58%" },
                new()
                    { Name = "darkslateblue", HexCode = "#483d8b", Rgb = "72, 61, 139", Hsl = "248, 39%, 39%" },
                new()
                {
                    Name = "mediumslateblue", HexCode = "#7b68ee", Rgb = "123, 104, 238", Hsl = "249, 80%, 67%"
                }
            }
        },
        new ColorCategory
        {
            CategoryName = "white",
            Shades = new List<ColorItem>
            {
                new() { Name = "white", HexCode = "#ffffff", Rgb = "255, 255, 255", Hsl = "0, 0%, 100%" },
                new() { Name = "snow", HexCode = "#fffafa", Rgb = "255, 250, 250", Hsl = "0, 100%, 99%" },
                new() { Name = "honeydew", HexCode = "#f0fff0", Rgb = "240, 255, 240", Hsl = "120, 100%, 97%" },
                new()
                    { Name = "mintcream", HexCode = "#f5fffa", Rgb = "245, 255, 250", Hsl = "150, 100%, 98%" },
                new() { Name = "azure", HexCode = "#f0ffff", Rgb = "240, 255, 255", Hsl = "180, 100%, 97%" },
                new()
                    { Name = "aliceblue", HexCode = "#f0f8ff", Rgb = "240, 248, 255", Hsl = "208, 100%, 97%" },
                new()
                    { Name = "ghostwhite", HexCode = "#f8f8ff", Rgb = "248, 248, 255", Hsl = "240, 100%, 99%" },
                new() { Name = "whitesmoke", HexCode = "#f5f5f5", Rgb = "245, 245, 245", Hsl = "0, 0%, 96%" },
                new() { Name = "seashell", HexCode = "#fff5ee", Rgb = "255, 245, 238", Hsl = "25, 100%, 97%" },
                new() { Name = "beige", HexCode = "#f5f5dc", Rgb = "245, 245, 220", Hsl = "60, 56%, 91%" },
                new() { Name = "oldlace", HexCode = "#fdf5e6", Rgb = "253, 245, 230", Hsl = "39, 85%, 95%" },
                new()
                    { Name = "floralwhite", HexCode = "#fffaf0", Rgb = "255, 250, 240", Hsl = "40, 100%, 97%" },
                new() { Name = "ivory", HexCode = "#fffff0", Rgb = "255, 255, 240", Hsl = "60, 100%, 97%" },
                new()
                    { Name = "antiquewhite", HexCode = "#faebd7", Rgb = "250, 235, 215", Hsl = "34, 78%, 91%" },
                new() { Name = "linen", HexCode = "#faf0e6", Rgb = "250, 240, 230", Hsl = "30, 67%, 94%" },
                new()
                {
                    Name = "lavenderblush", HexCode = "#fff0f5", Rgb = "255, 240, 245", Hsl = "340, 100%, 97%"
                },
                new() { Name = "mistyrose", HexCode = "#ffe4e1", Rgb = "255, 228, 225", Hsl = "6, 100%, 94%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "yellow",
            Shades = new List<ColorItem>
            {
                new() { Name = "gold", HexCode = "#ffd700", Rgb = "255, 215, 0", Hsl = "51, 100%, 50%" },
                new() { Name = "yellow", HexCode = "#ffff00", Rgb = "255, 255, 0", Hsl = "60, 100%, 50%" },
                new()
                    { Name = "lightyellow", HexCode = "#ffffe0", Rgb = "255, 255, 224", Hsl = "60, 100%, 94%" },
                new()
                    { Name = "lemonchiffon", HexCode = "#fffacd", Rgb = "255, 250, 205", Hsl = "54, 100%, 90%" },
                new()
                {
                    Name = "lightgoldenrodyellow", HexCode = "#fafad2", Rgb = "250, 250, 210", Hsl = "60, 80%, 90%"
                },
                new()
                    { Name = "papayawhip", HexCode = "#ffefd5", Rgb = "255, 239, 213", Hsl = "37, 100%, 92%" },
                new() { Name = "moccasin", HexCode = "#ffe4b5", Rgb = "255, 228, 181", Hsl = "38, 100%, 85%" },
                new() { Name = "peachpuff", HexCode = "#ffdab9", Rgb = "255, 218, 185", Hsl = "28, 100%, 86%" },
                new()
                    { Name = "palegoldenrod", HexCode = "#eee8aa", Rgb = "238, 232, 170", Hsl = "55, 67%, 80%" },
                new() { Name = "khaki", HexCode = "#f0e68c", Rgb = "240, 230, 140", Hsl = "54, 77%, 75%" },
                new() { Name = "darkkhaki", HexCode = "#bdb76b", Rgb = "189, 183, 107", Hsl = "56, 38%, 58%" }
            }
        }
    };

    public static string Hex(int num)
    {
        if (num > 255)
        {
            throw new Exception("'" + num + "'' is greater than 255(0xff);");
        }

        string str = num.ToString("X2");
        return str;
    }

    public static string RgbaToLab(double red, double green, double blue)
    {
        // 将RGBA颜色值转换为XYZ颜色值
        double r = red / 255;
        double g = green / 255;
        double b = blue / 255;

        if (r > 0.04045)
            r = Math.Pow((r + 0.055) / 1.055, 2.4);
        else
            r /= 12.92;

        if (g > 0.04045)
            g = Math.Pow((g + 0.055) / 1.055, 2.4);
        else
            g /= 12.92;

        if (b > 0.04045)
            b = Math.Pow((b + 0.055) / 1.055, 2.4);
        else
            b /= 12.92;

        r *= 100;
        g *= 100;
        b *= 100;

        double x = r * 0.4124564 + g * 0.3575761 + b * 0.1804375;
        double y = r * 0.2126729 + g * 0.7151522 + b * 0.0721750;
        double z = r * 0.0193339 + g * 0.1191920 + b * 0.9503041;

        // 将XYZ颜色值转换为Lab颜色值
        x /= 95.047;
        y /= 100.000;
        z /= 108.883;

        if (x > 0.008856)
            x = Math.Pow(x, 1.0 / 3.0);
        else
            x = (903.3 * x + 16) / 116;

        if (y > 0.008856)
            y = Math.Pow(y, 1.0 / 3.0);
        else
            y = (903.3 * y + 16) / 116;

        if (z > 0.008856)
            z = Math.Pow(z, 1.0 / 3.0);
        else
            z = (903.3 * z + 16) / 116;

        double l = (116 * y) - 16;
        double a = 500 * (x - y);
        double bValue = 200 * (y - z);

        // 返回Lab颜色值字符串
        string labColor = $"lab({l:F2},{a:F2},{bValue:F2})";
        return labColor;
    }


    public class Color
    {
        public int? R { get; set; }
        public int? G { get; set; }
        public int? B { get; set; }
        public double A { get; set; }

        public Color(int? r = null, int? g = null, int? b = null, double a = 1)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public bool Valid()
        {
            return (R != null) && (G != null) && (B != null);
        }

        public string ToHex()
        {
            return "#" + Hex(R.Value) + Hex(G.Value) + Hex(B.Value);
        }

        public string ToRgb()
        {
            return "rgb(" + R + "," + G + "," + B + ")";
        }

        public string ToRgba()
        {
            return "rgba(" + R + "," + G + "," + B + "," + A.ToString("0.000") + ")";
        }

        public string ToArgb()
        {
            return "#" + Hex((int)(A * 255)) + Hex(R.Value) + Hex(G.Value) + Hex(B.Value);
        }

        public string ToLab()
        {
            return RgbaToLab(R.Value, G.Value, B.Value);
        }

        public string ToHsl()
        {
            double r = this.R.Value / 255.0, g = this.G.Value / 255.0, b = this.B.Value / 255.0;
            double max = Math.Max(r, Math.Max(g, b)), min = Math.Min(r, Math.Min(g, b));
            double h, s, l = (max + min) / 2;
            if (Math.Abs(max - min) <= 0)
            {
                h = s = 0;
            }
            else
            {
                double d = max - min;
                s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
                if (Math.Abs(max - r) <= 0)
                {
                    h = (g - b) / d + (g < b ? 6 : 0);
                }
                else if (Math.Abs(max - g) <= 0)
                {
                    h = (b - r) / d + 2;
                }
                else
                {
                    h = (r - g) / d + 4;
                }

                h /= 6;
            }

            return "hsl(" + Math.Round(h * 360) + "," + Math.Round(s * 100, 1) + "%" + "," + Math.Round(l * 100, 1) +
                   "%)";
        }

        private static string Hex(int num)
        {
            if (num > 255)
            {
                throw new Exception("'" + num + "'' is greater than 255(0xff);");
            }

            string str = num.ToString("X2");
            return str;
        }
    }


    public static Color? ParseHex(string hexColor)
    {
        hexColor = hexColor.TrimStart('#');

        if (hexColor.Length == 6)
        {
            int red = Convert.ToInt32(hexColor.Substring(0, 2), 16);
            int green = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int blue = Convert.ToInt32(hexColor.Substring(4, 2), 16);

            return new Color(red, green, blue);
        }
        else if (hexColor.Length == 8)
        {
            var alpha = Convert.ToInt32(hexColor.Substring(0, 2), 16) / 255.0;
            int red = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int green = Convert.ToInt32(hexColor.Substring(4, 2), 16);
            int blue = Convert.ToInt32(hexColor.Substring(6, 2), 16);

            return new Color(red, green, blue, alpha);
        }
        else
        {
            return null;
        }
    }

    public static Color? ParseRgbOrRgba(string color)
    {
        color = color.ToLower();
        if (color.StartsWith("rgba"))
        {
            var colorValues = RemoveThirdChar(color);
            if (colorValues.Length < 4)
            {
                return null;
            }

            return new Color(int.Parse(colorValues[0]),
                int.Parse(colorValues[1]), int.Parse(colorValues[2]), double.Parse(colorValues[3]));
        }
        else if (color.StartsWith("rgb"))
        {
            var colorValues = RemoveThirdChar(color);
            if (colorValues.Length < 3)
            {
                return null;
            }

            return new Color(int.Parse(colorValues[0]),
                int.Parse(colorValues[1]), int.Parse(colorValues[2]));
        }

        return null;

        static string[] RemoveThirdChar(string sourceColor)
        {
            sourceColor = sourceColor.Replace("rgba", "");
            sourceColor = sourceColor.Replace("rgb", "");
            sourceColor = sourceColor.Replace("(", "");
            sourceColor = sourceColor.Replace(")", "");
            return sourceColor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public static Color? ParseLab(string labColor)
    {
        labColor = labColor.ToLower();
        if (!labColor.StartsWith("lab"))
        {
            return null;
        }

        var labValues = RemoveThirdChar(labColor);
        if (labValues.Length < 3)
        {
            return null;
        }

        double l = double.Parse(labValues[0]);
        double a = double.Parse(labValues[1]);
        double b = double.Parse(labValues[2]);

        // 将Lab颜色值转换为RGB颜色值
        double y = (l + 16) / 116;
        double x = a / 500 + y;
        double z = y - b / 200;

        double r = Math.Pow(x, 3) > 0.008856 ? Math.Pow(x, 3) : (x - 16 / 116) / 7.787;
        double g = Math.Pow(y, 3) > 0.008856 ? Math.Pow(y, 3) : (y - 16 / 116) / 7.787;
        double bValue = Math.Pow(z, 3) > 0.008856 ? Math.Pow(z, 3) : (z - 16 / 116) / 7.787;

        // 将RGB颜色值转换为Hex颜色值
        int red = (int)(r * 255);
        int green = (int)(g * 255);
        int blue = (int)(bValue * 255);
        return new Color(red, green, blue);

        static string[] RemoveThirdChar(string sourceColor)
        {
            sourceColor = sourceColor.Replace("lab", "");
            sourceColor = sourceColor.Replace("(", "");
            sourceColor = sourceColor.Replace(")", "");
            return sourceColor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public static Color? ParseHsl(string hslValue)
    {
        Color color = new();

        try
        {
            Regex regex = new(@"hsl\((\d+),\s*([\d.]+)%,\s*([\d.]+)%\)");
            Match match = regex.Match(hslValue);

            if (match.Success)
            {
                var h = Convert.ToDouble(match.Groups[1].Value) / 360;
                double s = Convert.ToDouble(match.Groups[2].Value) / 100;
                double l = Convert.ToDouble(match.Groups[3].Value) / 100;

                double r, g, b;

                if (s == 0)
                {
                    r = g = b = l;
                }
                else
                {
                    static double Hue2Rgb(double p, double q, double t)
                    {
                        if (t < 0) t += 1;
                        if (t > 1) t -= 1;
                        if (t < 1.0 / 6) return p + (q - p) * 6 * t;
                        if (t < 1.0 / 2) return q;
                        if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
                        return p;
                    }

                    double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                    double p = 2 * l - q;
                    r = Hue2Rgb(p, q, h + 1 / 3.0);
                    g = Hue2Rgb(p, q, h);
                    b = Hue2Rgb(p, q, h - 1 / 3.0);
                }

                color.R = (int)Math.Round(r * 255);
                color.G = (int)Math.Round(g * 255);
                color.B = (int)Math.Round(b * 255);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return color;
    }

    public static Color ParseColor(string val)
    {
        val = val.Trim().ToLower();
        var color = ParseHex(val);
        if (color?.Valid() == true)
        {
            return color;
        }

        color = ParseRgbOrRgba(val);
        if (color?.Valid() == true)
        {
            return color;
        }

        color = ParseLab(val);
        if (color?.Valid() == true)
        {
            return color;
        }

        color = ParseHsl(val);
        if (color?.Valid() == true)
        {
            return color;
        }

        // Add your custom color parsing logic here

        return null;
    }
}

public class ColorItem
{
    public string Name { get; set; } = null!;
    public string HexCode { get; set; } = null!;
    public string Rgb { get; set; } = null!;
    public string Hsl { get; set; } = null!;
}

public class ColorCategory
{
    public string CategoryName { get; set; } = null!;
    public List<ColorItem> Shades { get; set; } = null!;
}