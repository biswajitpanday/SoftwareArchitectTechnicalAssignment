using System;
using System.IO;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace WebMVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadApiController : ControllerBase
    {
        private readonly ITransactionImportService _transactionImportService;

        public FileUploadApiController(ITransactionImportService transactionImportService)
        {
            _transactionImportService = transactionImportService;
        }

        public async Task Upload(IFormFile file)
        {
            try
            {
                var uploadFolderPath = EnsureUploadFolder();
                WriteToDisk(uploadFolderPath, file);
                await _transactionImportService.Import(uploadFolderPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        #region Supported Methods

        private string EnsureUploadFolder()
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            return uploadPath;
        }

        private void WriteToDisk(string uploadFolderPath, IFormFile file)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value;
            var filePath = Path.Combine(uploadFolderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        #endregion
    }
}