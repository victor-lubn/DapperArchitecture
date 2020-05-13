using Microsoft.WindowsAzure.Storage.Blob;

namespace RS.Services.Azure.Contracts
{
    /// <summary>
    /// The IAzureStorageService
    /// </summary>
    public interface IAzureStorageService
    {
        /// <summary>
        /// Gets the BLOB container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns></returns>
        CloudBlobContainer GetBlobContainer(string containerName);
    }
}