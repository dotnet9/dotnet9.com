using System.Reflection;
using SkiaSharp;

namespace Dotnet9Tools.Helper;

public class ImageHelper
{
    private static readonly SKTypeface _embeddedTypeface;

    static ImageHelper()
    {
        if (_embeddedTypeface == null)
        {
            string? dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(dir!, "Fonts",
                "LXGWWenKaiMono-Bold.ttf");
            using MemoryStream ms = new MemoryStream(File.ReadAllBytes(path));
            _embeddedTypeface = SKTypeface.FromStream(ms);
        }
    }

    /// <summary>
    ///     图片文字水印
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="text"></param>
    /// <param name="fontSize"></param>
    /// <returns></returns>
    public static byte[] WriteWaterTag(byte[] bytes, string text, int fontSize)
    {
        using MemoryStream ms = new MemoryStream(bytes);

        using SKBitmap? bitmap = SKBitmap.Decode(ms);
        SKImageInfo info = bitmap.Info;

        using SKSurface? surface = SKSurface.Create(info);

        using SKCanvas? canvas = surface.Canvas;

        canvas.DrawBitmap(bitmap, new SKRect(0, 0, info.Width, info.Height));

        using SKPaint textPaint = new SKPaint();
        textPaint.Color = new SKColor(255, 255, 255, 180);
        textPaint.TextSize = fontSize;
        textPaint.TextAlign = SKTextAlign.Center;

        textPaint.Typeface = _embeddedTypeface;
        textPaint.IsAntialias = true;
        float textWidth = textPaint.GetGlyphWidths(text).Sum();

        SKFontMetrics fontMetrics = textPaint.FontMetrics;
        float textHeight = fontMetrics.CapHeight; //fontMetrics.Bottom - fontMetrics.Top;

        using SKPaint rectPaint = new SKPaint();
        rectPaint.Color = new SKColor(0, 0, 0, 100);

        //计算水印背景色的宽高和位置
        float bgW = textWidth += textWidth / 3;
        float bgH = textHeight += textHeight;
        float bgX = info.Width - bgW - 20;
        float bgY = info.Height - bgH - 20;

        canvas.DrawRect(bgX, bgY, bgW, bgH, rectPaint);

        //根据背景色的位置，计算文字的位置s
        float textX = bgX + (textWidth / 2);

        float textY = bgY + (textHeight / 4 * 3);
        // Draw the watermark text
        canvas.DrawText(text, textX, textY, textPaint);
        using SKData? data = surface.Snapshot().Encode(SKEncodedImageFormat.Webp, 80);
        return data.ToArray();
    }
}