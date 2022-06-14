using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace upload_to_wwwroot_api.Controllers
{
    [Route("[controller]")]
    public class UploadStaticFileController : Controller
    {
        private readonly ILogger<UploadStaticFileController> _logger;

        public UploadStaticFileController(ILogger<UploadStaticFileController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pages", fileName);
                string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (!Directory.Exists(wwwrootPath))
                    Directory.CreateDirectory(wwwrootPath);
                string fileNamePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//pages");
                if (!Directory.Exists(fileNamePath))
                    Directory.CreateDirectory(fileNamePath);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(fileStream);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}