using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RS.Services.Azure.Contracts;
using RS.Services.Base;
using RS.Services.Contracts;

namespace RS.Services.Azure
{
    /// <summary>
    /// The AzureStorageService
    /// </summary>
    /// <seealso cref="RS.Services.Base.BaseService" />
    /// <seealso cref="RS.Services.Azure.Contracts.IAzureStorageService" />
    public class AzureStorageService : BaseService, IAzureStorageService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageService"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public AzureStorageService(IServiceFactory serviceFactory) : base(serviceFactory) 
        {
        }
        /// <summary>
        /// Gets the BLOB container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns></returns>
        public CloudBlobContainer GetBlobContainer(string containerName)
        {
            var account = CloudStorageAccount.Parse(ServiceFactory.Configuration["AzureStorageConnectionString"]);
            var blobClient = account.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }
    }
}
