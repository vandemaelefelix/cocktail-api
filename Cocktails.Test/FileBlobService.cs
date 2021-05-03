using System.Threading.Tasks;
using System;
using Cocktails.API.Services;
using System.IO;

namespace Cocktails.Test
{
    public class FileBlobService : IBlobService
    {
        public async Task UploadByteArray(string containerName, byte[] data, string fileName)
        {
            await File.WriteAllBytesAsync($"C:\\src\\testImages\\{fileName}", data);
        }

        public async Task DeleteBlob(string containerName, string fileName)
        {
            Console.Write("Test");
        }
    }
}
