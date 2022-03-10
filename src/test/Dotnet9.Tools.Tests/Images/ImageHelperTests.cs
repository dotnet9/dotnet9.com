using Dotnet9.Tools.Images;

namespace Dotnet9.Tools.Tests.Images;

public class ImageHelperTests
{
    [Fact]
    public void IsPicture()
    {
        var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", "logo.png");
        Assert.True(File.Exists(testFilePath));

        var isPicture = ImagingHelper.IsPicture(testFilePath, out var typename);

        Assert.True(isPicture);
    }

    [Fact]
    public void IsNotPicture()
    {
        var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", "test.txt");
        Assert.True(File.Exists(testFilePath));

        var isPicture = ImagingHelper.IsPicture(testFilePath, out var typename);

        Assert.False(isPicture);
    }

    [Fact]
    public void IsPngFile()
    {
        var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", "logo.png");
        Assert.True(File.Exists(testFilePath));

        var isPng = ImagingHelper.IsPictureType(testFilePath, ImageType.Png);

        Assert.True(isPng);
    }

    [Fact]
    public void ShouldConvertPngToIcon()
    {
        var sourcePng = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", "logo.png");
        var destIco = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFiles", "logo.ico");
        Assert.True(File.Exists(sourcePng));
        Assert.False(File.Exists(destIco));

        ImagingHelper.ConvertToIcon(sourcePng, destIco);

        Assert.True(File.Exists(destIco));
        File.Delete(destIco);
    }
}