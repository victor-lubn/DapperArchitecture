using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS.Services.Contracts;

namespace RS.Api.Controllers
{
    /// <summary>
    /// The ValuesController.
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("RSOrigin")]
    ////[Authorize]
    public class ValuesController : Controller
    {
        private readonly IServiceFactory serviceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesController"/> class.
        /// </summary>
        /// <param name="serviceFactory">The serviceFactory.</param>
        public ValuesController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

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

            var container = serviceFactory.AzureStorageService.GetBlobContainer("images");
            var blockBlob = container.GetBlockBlobReference(DateTime.Now.Ticks.ToString() + file.FileName);
            await blockBlob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);

            return Ok(blockBlob.Uri);
        }
    }
}
