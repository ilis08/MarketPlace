using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Data.Entitites;
using Exceptions.NotFound;
using Microsoft.AspNetCore.Http;
using Repository.Contracts;

namespace Repository.Implementations;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient blobServiceClient;

    public BlobService(BlobServiceClient blobServiceClient)
    {
        this.blobServiceClient = blobServiceClient;
    }

    public Task DeleteFileBlobAsync(string blobFileName)
    {
        throw new NotImplementedException();
    }

    public async Task<Blob> GetBlobAsync(string name)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient("images");

        var blobClient = containerClient.GetBlobClient(name);

        if (await blobClient.ExistsAsync())
        {
            var data = await blobClient.OpenReadAsync();
            var blobContent = data;

            var content = await blobClient.DownloadContentAsync();

            return new Blob { Content = blobContent, Name = name, ContentType = content.Value.Details.ContentType };
        }

        throw new NotFoundException(1, "Blob");
    }

    public Task<IEnumerable<string>> ListBlobAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadFileBlobAsync(IFormFile file)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient("images");

        var name = Guid.NewGuid().ToString() + ".jpg";

        await using (var data = file.OpenReadStream())
        {
            await containerClient.UploadBlobAsync(name, data);
        }

        return name;
    }

    Task<IEnumerable<BlobInfo>> IBlobService.ListBlobAsync()
    {
        throw new NotImplementedException();
    }
}
