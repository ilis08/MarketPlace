using Azure.Storage.Blobs.Models;
using Data.Entitites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IBlobService
    {
        Task<Blob> GetBlobAsync(string name);
        Task<IEnumerable<BlobInfo>> ListBlobAsync();
        Task<string> UploadFileBlobAsync(IFormFile file);
        Task DeleteFileBlobAsync(string blobFileName);
    }
}
