using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dotnetools.Share.Helpers;

internal static partial class RgbHelper
{
    internal static List<ColorCategory> Colors = new List<ColorCategory>
    {
        new ColorCategory
        {
            CategoryName = "gray",
            Shades = new List<ColorItem>
            {
                new ColorItem { Name = "gainsboro", HexCode = "#dcdcdc", RGB = "220, 220, 220", HSL = "0, 0%, 86%" },
                new ColorItem { Name = "lightgray", HexCode = "#d3d3d3", RGB = "211, 211, 211", HSL = "0, 0%, 83%" },
                new ColorItem { Name = "silver", HexCode = "#c0c0c0", RGB = "192, 192, 192", HSL = "0, 0%, 75%" },
                new ColorItem { Name = "darkgray", HexCode = "#a9a9a9", RGB = "169, 169, 169", HSL = "0, 0%, 66%" },
                new ColorItem { Name = "gray", HexCode = "#808080", RGB = "128, 128, 128", HSL = "0, 0%, 50%" },
                new ColorItem { Name = "dimgray", HexCode = "#696969", RGB = "105, 105, 105", HSL = "0, 0%, 41%" },
                new ColorItem
                {
                    Name = "lightslategray", HexCode = "#778899", RGB = "119, 136, 153", HSL = "210, 14%, 53%"
                },
                new ColorItem { Name = "slategray", HexCode = "#708090", RGB = "112, 128, 144", HSL = "210, 13%, 50%" },
                new ColorItem
                    { Name = "darkslategray", HexCode = "#2f4f4f", RGB = "47, 79, 79", HSL = "180, 25%, 25%" },
                new ColorItem { Name = "black", HexCode = "#000000", RGB = "0, 0, 0", HSL = "0, 0%, 0%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "red",
            Shades = new List<ColorItem>
            {
                new ColorItem { Name = "indianred", HexCode = "#cd5c5c", RGB = "205, 92, 92", HSL = "0, 53%, 58%" },
                new ColorItem { Name = "lightcoral", HexCode = "#f08080", RGB = "240, 128, 128", HSL = "0, 79%, 72%" },
                new ColorItem { Name = "salmon", HexCode = "#fa8072", RGB = "250, 128, 114", HSL = "6, 93%, 71%" },
                new ColorItem { Name = "darksalmon", HexCode = "#e9967a", RGB = "233, 150, 122", HSL = "15, 72%, 70%" },
                new ColorItem
                    { Name = "lightsalmon", HexCode = "#ffa07a", RGB = "255, 160, 122", HSL = "17, 100%, 74%" },
                new ColorItem { Name = "crimson", HexCode = "#dc143c", RGB = "220, 20, 60", HSL = "348, 83%, 47%" },
                new ColorItem { Name = "red", HexCode = "#ff0000", RGB = "255, 0, 0", HSL = "0, 100%, 50%" },
                new ColorItem { Name = "firebrick", HexCode = "#b22222", RGB = "178, 34, 34", HSL = "0, 68%, 42%" },
                new ColorItem { Name = "darkred", HexCode = "#8b0000", RGB = "139, 0, 0", HSL = "0, 100%, 27%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "orange",
            Shades = new List<ColorItem>
            {
                new ColorItem
                    { Name = "lightsalmon", HexCode = "#ffa07a", RGB = "255, 160, 122", HSL = "17, 100%, 74%" },
                new ColorItem { Name = "coral", HexCode = "#ff7f50", RGB = "255, 127, 80", HSL = "16, 100%, 66%" },
                new ColorItem { Name = "tomato", HexCode = "#ff6347", RGB = "255, 99, 71", HSL = "9, 100%, 64%" },
                new ColorItem { Name = "orangered", HexCode = "#ff4500", RGB = "255, 69, 0", HSL = "16, 100%, 50%" },
                new ColorItem { Name = "darkorange", HexCode = "#ff8c00", RGB = "255, 140, 0", HSL = "33, 100%, 50%" },
                new ColorItem { Name = "orange", HexCode = "#ffa500", RGB = "255, 165, 0", HSL = "39, 100%, 50%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "brown",
            Shades = new List<ColorItem>
            {
                new ColorItem { Name = "cornsilk", HexCode = "#fff8dc", RGB = "255, 248, 220", HSL = "48, 100%, 93%" },
                new ColorItem
                {
                    Name = "blanchedalmond", HexCode = "#ffebcd", RGB = "255, 235, 205", HSL = "36, 100%, 90%"
                },
                new ColorItem { Name = "bisque", HexCode = "#ffe4c4", RGB = "255, 228, 196", HSL = "33, 100%, 88%" },
                new ColorItem
                    { Name = "navajowhite", HexCode = "#ffdead", RGB = "255, 222, 173", HSL = "36, 100%, 84%" },
                new ColorItem { Name = "wheat", HexCode = "#f5deb3", RGB = "245, 222, 179", HSL = "39, 77%, 83%" },
                new ColorItem { Name = "burlywood", HexCode = "#deb887", RGB = "222, 184, 135", HSL = "34, 57%, 70%" },
                new ColorItem { Name = "tan", HexCode = "#d2b48c", RGB = "210, 180, 140", HSL = "34, 44%, 69%" },
                new ColorItem { Name = "rosybrown", HexCode = "#bc8f8f", RGB = "188, 143, 143", HSL = "0, 25%, 65%" },
                new ColorItem { Name = "sandybrown", HexCode = "#f4a460", RGB = "244, 164, 96", HSL = "28, 87%, 67%" },
                new ColorItem { Name = "goldenrod", HexCode = "#daa520", RGB = "218, 165, 32", HSL = "43, 74%, 49%" },
                new ColorItem
                    { Name = "darkgoldenrod", HexCode = "#b8860b", RGB = "184, 134, 11", HSL = "43, 89%, 38%" },
                new ColorItem { Name = "peru", HexCode = "#cd853f", RGB = "205, 133, 63", HSL = "30, 59%, 53%" },
                new ColorItem { Name = "chocolate", HexCode = "#d2691e", RGB = "210, 105, 30", HSL = "25, 75%, 47%" },
                new ColorItem { Name = "saddlebrown", HexCode = "#8b4513", RGB = "139, 69, 19", HSL = "25, 76%, 31%" },
                new ColorItem { Name = "sienna", HexCode = "#a0522d", RGB = "160, 82, 45", HSL = "19, 56%, 40%" },
                new ColorItem { Name = "brown", HexCode = "#a52a2a", RGB = "165, 42, 42", HSL = "0, 59%, 41%" },
                new ColorItem { Name = "maroon", HexCode = "#800000", RGB = "128, 0, 0", HSL = "0, 100%, 25%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "blue",
            Shades = new List<ColorItem>
            {
                new ColorItem { Name = "aqua", HexCode = "#00ffff", RGB = "0, 255, 255", HSL = "180, 100%, 50%" },
                new ColorItem { Name = "cyan", HexCode = "#00ffff", RGB = "0, 255, 255", HSL = "180, 100%, 50%" },
                new ColorItem
                    { Name = "lightcyan", HexCode = "#e0ffff", RGB = "224, 255, 255", HSL = "180, 100%, 94%" },
                new ColorItem
                    { Name = "paleturquoise", HexCode = "#afeeee", RGB = "175, 238, 238", HSL = "180, 65%, 81%" },
                new ColorItem
                    { Name = "aquamarine", HexCode = "#7fffd4", RGB = "127, 255, 212", HSL = "160, 100%, 75%" },
                new ColorItem { Name = "turquoise", HexCode = "#40e0d0", RGB = "64, 224, 208", HSL = "174, 72%, 56%" },
                new ColorItem
                {
                    Name = "mediumturquoise", HexCode = "#48d1cc", RGB = "72, 209, 204", HSL = "178, 60%, 55%"
                },
                new ColorItem
                    { Name = "darkturquoise", HexCode = "#00ced1", RGB = "0, 206, 209", HSL = "181, 100%, 41%" },
                new ColorItem { Name = "cadetblue", HexCode = "#5f9ea0", RGB = "95, 158, 160", HSL = "182, 25%, 50%" },
                new ColorItem { Name = "steelblue", HexCode = "#4682b4", RGB = "70, 130, 180", HSL = "207, 44%, 49%" },
                new ColorItem
                {
                    Name = "lightsteelblue", HexCode = "#b0c4de", RGB = "176, 196, 222", HSL = "214, 41%, 78%"
                },
                new ColorItem
                    { Name = "powderblue", HexCode = "#b0e0e6", RGB = "176, 224, 230", HSL = "187, 52%, 80%" },
                new ColorItem { Name = "lightblue", HexCode = "#add8e6", RGB = "173, 216, 230", HSL = "195, 53%, 79%" },
                new ColorItem { Name = "skyblue", HexCode = "#87ceeb", RGB = "135, 206, 235", HSL = "197, 71%, 73%" },
                new ColorItem
                    { Name = "lightskyblue", HexCode = "#87cefa", RGB = "135, 206, 250", HSL = "203, 92%, 75%" },
                new ColorItem
                    { Name = "deepskyblue", HexCode = "#00bfff", RGB = "0, 191, 255", HSL = "195, 100%, 50%" },
                new ColorItem
                    { Name = "dodgerblue", HexCode = "#1e90ff", RGB = "30, 144, 255", HSL = "210, 100%, 56%" },
                new ColorItem
                {
                    Name = "cornflowerblue", HexCode = "#6495ed", RGB = "100, 149, 237", HSL = "219, 79%, 66%"
                },
                new ColorItem
                {
                    Name = "mediumslateblue", HexCode = "#7b68ee", RGB = "123, 104, 238", HSL = "249, 80%, 67%"
                },
                new ColorItem { Name = "royalblue", HexCode = "#4169e1", RGB = "65, 105, 225", HSL = "225, 73%, 57%" },
                new ColorItem { Name = "blue", HexCode = "#0000ff", RGB = "0, 0, 255", HSL = "240, 100%, 50%" },
                new ColorItem { Name = "mediumblue", HexCode = "#0000cd", RGB = "0, 0, 205", HSL = "240, 100%, 40%" },
                new ColorItem { Name = "darkblue", HexCode = "#00008b", RGB = "0, 0, 139", HSL = "240, 100%, 27%" },
                new ColorItem { Name = "navy", HexCode = "#000080", RGB = "0, 0, 128", HSL = "240, 100%, 25%" },
                new ColorItem { Name = "midnightblue", HexCode = "#191970", RGB = "25, 25, 112", HSL = "240, 64%, 27%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "green",
            Shades = new List<ColorItem>
            {
                new ColorItem
                    { Name = "greenyellow", HexCode = "#adff2f", RGB = "173, 255, 47", HSL = "84, 100%, 59%" },
                new ColorItem { Name = "chartreuse", HexCode = "#7fff00", RGB = "127, 255, 0", HSL = "90, 100%, 50%" },
                new ColorItem { Name = "lawngreen", HexCode = "#7cfc00", RGB = "124, 252, 0", HSL = "90, 100%, 49%" },
                new ColorItem { Name = "lime", HexCode = "#00ff00", RGB = "0, 255, 0", HSL = "120, 100%, 50%" },
                new ColorItem { Name = "limegreen", HexCode = "#32cd32", RGB = "50, 205, 50", HSL = "120, 61%, 50%" },
                new ColorItem { Name = "palegreen", HexCode = "#98fb98", RGB = "152, 251, 152", HSL = "120, 93%, 79%" },
                new ColorItem
                    { Name = "lightgreen", HexCode = "#90ee90", RGB = "144, 238, 144", HSL = "120, 73%, 75%" },
                new ColorItem
                {
                    Name = "mediumspringgreen", HexCode = "#00fa9a", RGB = "0, 250, 154", HSL = "157, 100%, 49%"
                },
                new ColorItem
                    { Name = "springgreen", HexCode = "#00ff7f", RGB = "0, 255, 127", HSL = "150, 100%, 50%" },
                new ColorItem
                    { Name = "mediumseagreen", HexCode = "#3cb371", RGB = "60, 179, 113", HSL = "147, 50%, 47%" },
                new ColorItem { Name = "seagreen", HexCode = "#2e8b57", RGB = "46, 139, 87", HSL = "146, 50%, 36%" },
                new ColorItem { Name = "forestgreen", HexCode = "#228b22", RGB = "34, 139, 34", HSL = "120, 61%, 34%" },
                new ColorItem { Name = "green", HexCode = "#008000", RGB = "0, 128, 0", HSL = "120, 100%, 25%" },
                new ColorItem { Name = "darkgreen", HexCode = "#006400", RGB = "0, 100, 0", HSL = "120, 100%, 20%" },
                new ColorItem { Name = "yellowgreen", HexCode = "#9acd32", RGB = "154, 205, 50", HSL = "80, 61%, 50%" },
                new ColorItem { Name = "olivedrab", HexCode = "#6b8e23", RGB = "107, 142, 35", HSL = "80, 60%, 35%" },
                new ColorItem { Name = "olive", HexCode = "#808000", RGB = "128, 128, 0", HSL = "60, 100%, 25%" },
                new ColorItem
                    { Name = "darkolivegreen", HexCode = "#556b2f", RGB = "85, 107, 47", HSL = "82, 39%, 30%" },
                new ColorItem
                {
                    Name = "mediumaquamarine", HexCode = "#66cdaa", RGB = "102, 205, 170", HSL = "160, 51%, 60%"
                },
                new ColorItem
                    { Name = "darkseagreen", HexCode = "#8fbc8b", RGB = "143, 188, 139", HSL = "115, 27%, 64%" },
                new ColorItem
                    { Name = "lightseagreen", HexCode = "#20b2aa", RGB = "32, 178, 170", HSL = "177, 70%, 41%" },
                new ColorItem { Name = "darkcyan", HexCode = "#008b8b", RGB = "0, 139, 139", HSL = "180, 100%, 27%" },
                new ColorItem { Name = "teal", HexCode = "#008080", RGB = "0, 128, 128", HSL = "180, 100%, 25%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "pink",
            Shades = new List<ColorItem>
            {
                new ColorItem { Name = "pink", HexCode = "#ffc0cb", RGB = "255, 192, 203", HSL = "350, 100%, 88%" },
                new ColorItem
                    { Name = "lightpink", HexCode = "#ffb6c1", RGB = "255, 182, 193", HSL = "351, 100%, 86%" },
                new ColorItem { Name = "hotpink", HexCode = "#ff69b4", RGB = "255, 105, 180", HSL = "330, 100%, 71%" },
                new ColorItem { Name = "deeppink", HexCode = "#ff1493", RGB = "255, 20, 147", HSL = "328, 100%, 54%" },
                new ColorItem
                {
                    Name = "mediumvioletred", HexCode = "#c71585", RGB = "199, 21, 133", HSL = "322, 81%, 43%"
                },
                new ColorItem
                    { Name = "palevioletred", HexCode = "#db7093", RGB = "219, 112, 147", HSL = "340, 60%, 65%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "purple",
            Shades = new List<ColorItem>
            {
                new ColorItem { Name = "lavender", HexCode = "#e6e6fa", RGB = "230, 230, 250", HSL = "240, 67%, 94%" },
                new ColorItem { Name = "thistle", HexCode = "#d8bfd8", RGB = "216, 191, 216", HSL = "300, 24%, 80%" },
                new ColorItem { Name = "plum", HexCode = "#dda0dd", RGB = "221, 160, 221", HSL = "300, 47%, 75%" },
                new ColorItem { Name = "violet", HexCode = "#ee82ee", RGB = "238, 130, 238", HSL = "300, 76%, 72%" },
                new ColorItem { Name = "orchid", HexCode = "#da70d6", RGB = "218, 112, 214", HSL = "302, 59%, 65%" },
                new ColorItem { Name = "fuchsia", HexCode = "#ff00ff", RGB = "255, 0, 255", HSL = "300, 100%, 50%" },
                new ColorItem { Name = "magenta", HexCode = "#ff00ff", RGB = "255, 0, 255", HSL = "300, 100%, 50%" },
                new ColorItem
                    { Name = "mediumorchid", HexCode = "#ba55d3", RGB = "186, 85, 211", HSL = "288, 59%, 58%" },
                new ColorItem
                    { Name = "mediumpurple", HexCode = "#9370db", RGB = "147, 112, 219", HSL = "260, 60%, 65%" },
                new ColorItem
                    { Name = "rebeccapurple", HexCode = "#663399", RGB = "102, 51, 153", HSL = "270, 50%, 40%" },
                new ColorItem { Name = "blueviolet", HexCode = "#8a2be2", RGB = "138, 43, 226", HSL = "271, 76%, 53%" },
                new ColorItem { Name = "darkviolet", HexCode = "#9400d3", RGB = "148, 0, 211", HSL = "282, 100%, 41%" },
                new ColorItem { Name = "darkorchid", HexCode = "#9932cc", RGB = "153, 50, 204", HSL = "280, 61%, 50%" },
                new ColorItem
                    { Name = "darkmagenta", HexCode = "#8b008b", RGB = "139, 0, 139", HSL = "300, 100%, 27%" },
                new ColorItem { Name = "purple", HexCode = "#800080", RGB = "128, 0, 128", HSL = "300, 100%, 25%" },
                new ColorItem { Name = "indigo", HexCode = "#4b0082", RGB = "75, 0, 130", HSL = "275, 100%, 25%" },
                new ColorItem { Name = "slateblue", HexCode = "#6a5acd", RGB = "106, 90, 205", HSL = "248, 53%, 58%" },
                new ColorItem
                    { Name = "darkslateblue", HexCode = "#483d8b", RGB = "72, 61, 139", HSL = "248, 39%, 39%" },
                new ColorItem
                {
                    Name = "mediumslateblue", HexCode = "#7b68ee", RGB = "123, 104, 238", HSL = "249, 80%, 67%"
                }
            }
        },
        new ColorCategory
        {
            CategoryName = "white",
            Shades = new List<ColorItem>
            {
                new ColorItem { Name = "white", HexCode = "#ffffff", RGB = "255, 255, 255", HSL = "0, 0%, 100%" },
                new ColorItem { Name = "snow", HexCode = "#fffafa", RGB = "255, 250, 250", HSL = "0, 100%, 99%" },
                new ColorItem { Name = "honeydew", HexCode = "#f0fff0", RGB = "240, 255, 240", HSL = "120, 100%, 97%" },
                new ColorItem
                    { Name = "mintcream", HexCode = "#f5fffa", RGB = "245, 255, 250", HSL = "150, 100%, 98%" },
                new ColorItem { Name = "azure", HexCode = "#f0ffff", RGB = "240, 255, 255", HSL = "180, 100%, 97%" },
                new ColorItem
                    { Name = "aliceblue", HexCode = "#f0f8ff", RGB = "240, 248, 255", HSL = "208, 100%, 97%" },
                new ColorItem
                    { Name = "ghostwhite", HexCode = "#f8f8ff", RGB = "248, 248, 255", HSL = "240, 100%, 99%" },
                new ColorItem { Name = "whitesmoke", HexCode = "#f5f5f5", RGB = "245, 245, 245", HSL = "0, 0%, 96%" },
                new ColorItem { Name = "seashell", HexCode = "#fff5ee", RGB = "255, 245, 238", HSL = "25, 100%, 97%" },
                new ColorItem { Name = "beige", HexCode = "#f5f5dc", RGB = "245, 245, 220", HSL = "60, 56%, 91%" },
                new ColorItem { Name = "oldlace", HexCode = "#fdf5e6", RGB = "253, 245, 230", HSL = "39, 85%, 95%" },
                new ColorItem
                    { Name = "floralwhite", HexCode = "#fffaf0", RGB = "255, 250, 240", HSL = "40, 100%, 97%" },
                new ColorItem { Name = "ivory", HexCode = "#fffff0", RGB = "255, 255, 240", HSL = "60, 100%, 97%" },
                new ColorItem
                    { Name = "antiquewhite", HexCode = "#faebd7", RGB = "250, 235, 215", HSL = "34, 78%, 91%" },
                new ColorItem { Name = "linen", HexCode = "#faf0e6", RGB = "250, 240, 230", HSL = "30, 67%, 94%" },
                new ColorItem
                {
                    Name = "lavenderblush", HexCode = "#fff0f5", RGB = "255, 240, 245", HSL = "340, 100%, 97%"
                },
                new ColorItem { Name = "mistyrose", HexCode = "#ffe4e1", RGB = "255, 228, 225", HSL = "6, 100%, 94%" }
            }
        },
        new ColorCategory
        {
            CategoryName = "yellow",
            Shades = new List<ColorItem>
            {
                new ColorItem { Name = "gold", HexCode = "#ffd700", RGB = "255, 215, 0", HSL = "51, 100%, 50%" },
                new ColorItem { Name = "yellow", HexCode = "#ffff00", RGB = "255, 255, 0", HSL = "60, 100%, 50%" },
                new ColorItem
                    { Name = "lightyellow", HexCode = "#ffffe0", RGB = "255, 255, 224", HSL = "60, 100%, 94%" },
                new ColorItem
                    { Name = "lemonchiffon", HexCode = "#fffacd", RGB = "255, 250, 205", HSL = "54, 100%, 90%" },
                new ColorItem
                {
                    Name = "lightgoldenrodyellow", HexCode = "#fafad2", RGB = "250, 250, 210", HSL = "60, 80%, 90%"
                },
                new ColorItem
                    { Name = "papayawhip", HexCode = "#ffefd5", RGB = "255, 239, 213", HSL = "37, 100%, 92%" },
                new ColorItem { Name = "moccasin", HexCode = "#ffe4b5", RGB = "255, 228, 181", HSL = "38, 100%, 85%" },
                new ColorItem { Name = "peachpuff", HexCode = "#ffdab9", RGB = "255, 218, 185", HSL = "28, 100%, 86%" },
                new ColorItem
                    { Name = "palegoldenrod", HexCode = "#eee8aa", RGB = "238, 232, 170", HSL = "55, 67%, 80%" },
                new ColorItem { Name = "khaki", HexCode = "#f0e68c", RGB = "240, 230, 140", HSL = "54, 77%, 75%" },
                new ColorItem { Name = "darkkhaki", HexCode = "#bdb76b", RGB = "189, 183, 107", HSL = "56, 38%, 58%" }
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


    public class Color
    {
        public int? r { get; set; }
        public int? g { get; set; }
        public int? b { get; set; }
        public double a { get; set; }

        public Color(int? _r = null, int? _g = null, int? _b = null, double a = 1)
        {
            this.r = _r;
            this.g = _g;
            this.b = _b;
            this.a = a;
        }

        public bool Valid()
        {
            return (r != null) && (g != null) && (b != null);
        }

        public string ToHex()
        {
            return "#" + Hex(r.Value) + Hex(g.Value) + Hex(b.Value);
        }

        public string ToRGB()
        {
            return "rgb(" + r + "," + g + "," + b + ")";
        }

        public string ToRGBA()
        {
            return "rgba(" + r + "," + g + "," + b + "," + a.ToString("0.000") + ")";
        }

        public string ToARGB()
        {
            return "#" + Hex((int)(a * 255 - 1)) + Hex(r.Value) + Hex(g.Value) + Hex(b.Value);
        }

        public string ToHSL()
        {
            double r = this.r.Value / 255.0, g = this.g.Value / 255.0, b = this.b.Value / 255.0;
            double max = Math.Max(r, Math.Max(g, b)), min = Math.Min(r, Math.Min(g, b));
            double h, s, l = (max + min) / 2;
            if (max == min)
            {
                h = s = 0;
            }
            else
            {
                double d = max - min;
                s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
                if (max == r)
                {
                    h = (g - b) / d + (g < b ? 6 : 0);
                }
                else if (max == g)
                {
                    h = (b - r) / d + 2;
                }
                else
                {
                    h = (r - g) / d + 4;
                }

                h /= 6;
            }

            return "hsl(" + Math.Round(h * 360) + "," + Math.Round(s * 100) + "%" + "," + Math.Round(l * 100) + "%)";
        }

        private string Hex(int num)
        {
            if (num > 255)
            {
                throw new Exception("'" + num + "'' is greater than 255(0xff);");
            }

            string str = num.ToString("X2");
            return str;
        }
    }


    public static Color? ParseHEX(string hexColor)
    {
        hexColor = hexColor.TrimStart('#');

        Regex regex = new(@"^([0-9a-fA-F]{2})([0-9a-fA-F]{2})([0-9a-fA-F]{2})$");
        var match = regex.Match(hexColor);

        if (match?.Success == true)
        {
            int red = Convert.ToInt32(match.Groups[1].Value, 16);
            int green = Convert.ToInt32(match.Groups[2].Value, 16);
            int blue = Convert.ToInt32(match.Groups[3].Value, 16);

            return new Color(red, green, blue);
        }
        else
        {
            return null;
        }
    }

    public static Color? ParseRGBA(string color)
    {
        Regex regex = new Regex(@"^rgba?\((\d+),\s*(\d+),\s*(\d+)(?:,\s*(\d+(?:\.\d+)?))?\)$");
        var match = regex.Match(color);

        if (match?.Success == true)
        {
            int r = int.Parse(match.Groups[1].Value);
            int g = int.Parse(match.Groups[2].Value);
            int b = int.Parse(match.Groups[3].Value);
            if (!double.TryParse(match.Groups[4].Value, out double a))
            {
                a = 1;
            }

            return new Color(r, g, b, a);
        }

        return null;
    }

    public static Color? ParseARGB(string val)
    {
        Match argb = Regex.Match(val, @"^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$",
            RegexOptions.IgnoreCase);
        if (argb?.Success == true)
        {
            var r = int.Parse(argb.Groups[2].Value, NumberStyles.HexNumber);
            var g = int.Parse(argb.Groups[3].Value, NumberStyles.HexNumber);
            var b = int.Parse(argb.Groups[4].Value, NumberStyles.HexNumber);
            var a = int.Parse(argb.Groups[1].Value, NumberStyles.HexNumber) / 255.0;
            return new Color(r, g, b, a);
        }

        return null;
    }

    public static Color? ParseHSL(string hslValue)
    {
        Color color = new Color();

        try
        {
            Regex regex = new Regex(@"hsl\((\d+),\s*([\d.]+)%,\s*([\d.]+)%\)");
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
                    double Hue2RGB(double p, double q, double t)
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
                    r = Hue2RGB(p, q, h + 1 / 3.0);
                    g = Hue2RGB(p, q, h);
                    b = Hue2RGB(p, q, h - 1 / 3.0);
                }

                color.r = (int)Math.Round(r * 255);
                color.g = (int)Math.Round(g * 255);
                color.b = (int)Math.Round(b * 255);
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
        Color color = ParseHEX(val);
        if (color?.Valid() == true)
        {
            return color;
        }

        color = ParseRGBA(val);
        if (color?.Valid() == true)
        {
            return color;
        }

        color = ParseARGB(val);
        if (color?.Valid() == true)
        {
            return color;
        }

        color = ParseHSL(val);
        if (color?.Valid() == true)
        {
            return color;
        }

        // Add your custom color parsing logic here

        return color;
    }
}

public class ColorItem
{
    public string Name { get; set; }
    public string HexCode { get; set; }
    public string RGB { get; set; }
    public string HSL { get; set; }
}

public class ColorCategory
{
    public string CategoryName { get; set; }
    public List<ColorItem> Shades { get; set; }
}