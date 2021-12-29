using System.IO;
using System.Threading.Tasks;

namespace Dotnet9.Abp.BlobStoring.TencentCloud.Infrastructure
{
    public static class BinaryExtensions
    {
        public static async Task<byte[]> ToBytesAsync(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}