using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS.Api.Controllers.Base;
using RS.Services.Contracts;

namespace RS.Api.Controllers
{
    /// <summary>
    /// The values controller.
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class ValuesController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public ValuesController(IServiceFactory serviceFactory)
            : base(serviceFactory) { }

        /// <summary>
        /// Addes files.
        /// </summary>
        /// <param name="files">The file name.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFile()
        {
            byte[] fileBytes;

            if (Request.Form.Files.Count == 0)
                return BadRequest();

            var file = Request.Form.Files[0];
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            var container = ServiceFactory.AzureStorageService.GetBlobContainer("images");
            var blockBlob = container.GetBlockBlobReference(DateTime.Now.Ticks.ToString() + file.FileName);
            await blockBlob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);

            return Ok(blockBlob.Uri);
        }
    }
}
