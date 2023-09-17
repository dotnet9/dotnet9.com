namespace Dotnet9.Core.Test;

[TestClass]
public class HashidsUtilUnitTest
{
    [TestMethod]
    public void GetHashids_Success()
    {
        var testSlug = "Some-experiences-and-lessons-learned-from-the-transformation-of-the-NET-system-architecture";

        var shortId1 = HashidsUtil.GetHashids(testSlug);
        var shortId2 = HashidsUtil.GetHashids(testSlug);

        Assert.AreEqual(shortId1, shortId2);
    }
}