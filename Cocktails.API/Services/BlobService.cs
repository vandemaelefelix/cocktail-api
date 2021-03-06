using System;
using System.IO;
using System.Threading.Tasks;
using Cocktails.API.Config;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using BlobStorage = Microsoft.Azure.Storage;

namespace Cocktails.API.Services
{
    public interface IBlobService
    {
        Task UploadByteArray(string containerName, byte[] data, string fileName);
        Task DeleteBlob(string containerName, string fileName);
    }

    public class BlobService : IBlobService
    {
        private ConnectionStrings _connectionStrings;
        
        public BlobService(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        public async Task UploadByteArray(string containerName, byte[] data, string fileName)
        {
            try
            {
                BlobStorage.CloudStorageAccount storageAccount = BlobStorage.CloudStorageAccount.Parse(_connectionStrings.BlobStorage);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobClient.GetContainerReference(containerName);


                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
                
                using (var memoryStream = new MemoryStream())
                {
                    await blockBlob.UploadFromByteArrayAsync(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task DeleteBlob(string containerName, string fileName) 
        {
            try
            {
                BlobStorage.CloudStorageAccount storageAccount = BlobStorage.CloudStorageAccount.Parse(_connectionStrings.BlobStorage);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

                await blob.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
