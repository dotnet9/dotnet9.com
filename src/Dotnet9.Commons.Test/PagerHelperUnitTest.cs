namespace Dotnet9.Commons.Test;

public class PagerHelperUnitTest
{
    [Fact]
    public void CalcPages_Return_Success()
    {
        var pages = PagerHelper.CalcPages(3, 5, 1);
        Assert.Equal(new List<int> { 1, 2, 3 }, pages);

        pages = PagerHelper.CalcPages(6, 5, 1);
        Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, pages);

        pages = PagerHelper.CalcPages(10, 5, 4);
        Assert.Equal(new List<int> { 2, 3, 4, 5, 6 }, pages);

        pages = PagerHelper.CalcPages(20, 5, 18);
        Assert.Equal(new List<int> { 16, 17, 18, 19, 20 }, pages);
    }
}