using BlogocomApiV2.Models;
using BlogocomApiV2.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BlogocomApiV2.Controllers
{
    [Route("api/picture")]
    [ApiController]
//    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly ApiDbContext DB;
        public UploadController (ApiDbContext context)
        {
            DB = context;
        }

        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Picture>> UploadFile(
         IFormFile file,
         CancellationToken cancellationToken)
        {
            if (CheckIfExcelFile(file))
            {
                Picture pic = await WriteFile(file);
                if (pic != null)
                {
                    DB.Pictures.Add(pic);
                    await DB.SaveChangesAsync();
                    return Ok(pic);
                }
                else return BadRequest(new { message = "Error!" });

            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }

           
        }

        [HttpGet("download/{id:long}")]
        public async Task<ActionResult> DownloadFile(long id)
        {
            var filePath = $"Upload\\files\\{id}.jpg"; // Here, you should validate the request and the existance of the file.

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "text/plain", Path.GetFileName(filePath));
        }

        /// <summary>
        /// Method to check if file is excel file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".tiff" || 
                    extension == ".jpg" ||
                    extension == ".raw" ||
                    extension == ".jpeg" ||
                    extension == ".bmp" ||
                    extension == ".gif" ||
                    extension == ".png"
                    ); // Change the extension based on your need
        }

        private async Task<Picture> WriteFile(IFormFile file)
        {
            bool isSaveSuccess = false;
            string fileName;
            Picture? pic = null;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var ticks = DateTime.Now.Ticks;
                fileName = ticks + extension; //Create a new Name for the file due to security reasons.

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                pic = new Picture
                {
                    OriginalName = file.FileName,
                    UniqueName = fileName,
                    Size = file.Length,
                    Type =  extension.Replace(".", ""),
                    //Local
                    //WebPath = "https://localhost:5001/api/picture/download/" + ticks,
                    //Production
                    WebPath = "https://blogocomapiv2.azurewebsites.net" + ticks

                    
                };

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                //log error
            }

            return pic;
        }
    }
}
