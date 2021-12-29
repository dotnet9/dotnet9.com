using System.IO;
using System.Threading.Tasks;
using COSXML.CosException;
using COSXML.Model.Bucket;
using COSXML.Model.Object;

namespace Dotnet9.Abp.BlobStoring.TencentCloud.Infrastructure
{
    public static class CosServerWarpObjectExtensions
    {
        public static async Task<bool> CheckBucketIsExistAsync(this CosServerWrapObject client, string bucketName)
        {
            try
            {
                var result = await client.CosXmlServer.ExecuteAsync<HeadBucketResult>(new HeadBucketRequest(bucketName));
                
                return result.IsSuccessful();
            }
            catch (CosServerException exception)
            {
                if (exception.statusCode == 404)
                {
                    return false;
                }

                throw;
            }
        }

        public static async Task<bool> CheckObjectIsExistAsync(this CosServerWrapObject client, string bucketName, string objectKey)
        {
            try
            {
                var result =
                    await client.CosXmlServer.ExecuteAsync<HeadObjectResult>(new HeadObjectRequest(bucketName, objectKey));
                
                return result.IsSuccessful();
            }
            catch (CosServerException exception)
            {
                if (exception.statusCode == 404)
                {
                    return false;
                }

                throw;
            }
        }

        public static async Task CreateBucketAsync(this CosServerWrapObject client, string bucketName)
        {
            await client.CosXmlServer.ExecuteAsync<PutBucketResult>(new PutBucketRequest(bucketName));
        }
        
        public static async Task UploadObjectAsync(this CosServerWrapObject client,
            string bucketName,
            string objectKey,
            Stream data)
        {
            await client.CosXmlServer.ExecuteAsync<PutObjectResult>(new PutObjectRequest(bucketName, objectKey,
                await data.ToBytesAsync()));
        }

        public static async Task<Stream> DownloadObjectAsync(this CosServerWrapObject client,
            string bucketName,
            string objectKey)
        {
            var result =
                await client.CosXmlServer.ExecuteAsync<GetObjectBytesResult>(
                    new GetObjectBytesRequest(bucketName, objectKey));
            
            return new MemoryStream(result.content);
        }

        public static async Task DeleteObjectAsync(this CosServerWrapObject client,
            string bucketName,
            string objectKey)
        {
            await client.CosXmlServer.ExecuteAsync<DeleteObjectResult>(new DeleteObjectRequest(bucketName, objectKey));
        }
    }
}