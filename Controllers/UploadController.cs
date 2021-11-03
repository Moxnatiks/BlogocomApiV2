﻿using BlogocomApiV2.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace BlogocomApiV2.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ApiDbContext DB;
        public IConfiguration Configuration { get; }
        public UploadController (ApiDbContext context, IConfiguration configuration)
        {
            Configuration = configuration;
            DB = context;
        }


        [Authorize]
        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.File>> UploadFile(
         IFormFile file,
         CancellationToken cancellationToken)
        {
            if (CheckAccessFile(file))
            {
                Models.File? pic = await WriteFile(file);
                if (pic != null)
                {
                    DB.Files.Add(pic);
                    DB.SaveChanges();
                    return Ok(pic);
                }
                else return BadRequest(new { message = "Error!" });

            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }

           
        }



        /// <summary>
        /// Method to check if file is excel file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckAccessFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".tiff"|| 
                    extension == ".jpg" ||
                    extension == ".raw" ||
                    extension == ".jpeg"||
                    extension == ".bmp" ||
                    extension == ".gif" ||
                    extension == ".png" ||
                    extension == ".avi" ||
                    extension == ".m4v" ||
                    extension == ".mov" ||
                    extension == ".mp4"
                    ); // Change the extension based on your need
        }

        private async Task<Models.File> WriteFile(IFormFile file)
        {
            string DirFilePath = "Files";
            string PreviewVideoPicWebPart = "";

            string fileName;
            Models.File? pic = null;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var ticks = DateTime.Now.Ticks;
                fileName = ticks + extension; //Create a new Name for the file due to security reasons.

                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), DirFilePath);

                if (!Directory.Exists(pathFile))
                {
                    Directory.CreateDirectory(pathFile);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                if (extension == ".avi" ||
                    extension == ".m4v" ||
                    extension == ".mov" ||
                    extension == ".mp4")
                {
                    IConversion conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(DirFilePath +"/"+ fileName, DirFilePath +"/"+ ticks + "preview.png", TimeSpan.FromSeconds(3));
                    IConversionResult result = await conversion.Start();

                    PreviewVideoPicWebPart = Configuration.GetConnectionString("Domen") + "/api/file/download/" + ticks + "preview.png";
                }

                pic = new Models.File
                {
                    OriginalName = file.FileName,
                    UniqueName = fileName,
                    Size = file.Length,
                    Type =  extension.Replace(".", ""),
                    PreviewVideoPicWebPart = PreviewVideoPicWebPart,
                    WebPath = Configuration.GetConnectionString("Domen")+ "/api/file/download/" + fileName,
                };
            }
            catch (Exception e)
            {
                //log error
            }

            return pic;
        }

        [Authorize]
        [HttpGet("download/{uniqueName}")]
        public async Task<ActionResult> DownloadFile(string uniqueName)
        {
            string filePath = "Files/" + uniqueName; // Here, you should validate the request and the existance of the file.
            byte[] bytes ;
            try
            {
                 bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            }
            catch
            {
                return BadRequest(new { message = "File not found!" });
            }

            return File(bytes, "text/plain", Path.GetFileName(filePath));
        }
    }
}
