namespace Dotnet9.Commons.Test;

public class RgbHelperUnitTest
{
    [Fact]
    public void ParseColor_Return_Success()
    {
        string[] colors = new string[]
            { "#707B7C", "#AAF7DC6F", "rgb(72,201,176)", "rgba(241,148,138,0.5)", "hsl(204,70%,63%)", "lab(20,50,30)" };

        foreach (string color in colors)
        {
            try
            {
                var colorObj = RgbHelper.ParseColor(color!);

                var hex = colorObj.ToHex();
                var argb = colorObj.ToARGB();
                var _rgb = colorObj.ToRGB();
                var _rgba = colorObj.ToRGBA();
                var _hsl = colorObj.ToHSL();
                var _lab = colorObj.ToLab();
            }
            catch (Exception ex)
            {
                Assert.True(false);
            }
        }
    }
}