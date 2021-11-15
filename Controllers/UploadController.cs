using BlogocomApiV2.Settings;
using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
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
        private const string DirFilePath = "Files";
        private readonly ApiDbContext DB;
        public IConfiguration Configuration { get; }


        public UploadController(ApiDbContext context, IConfiguration configuration)
        {
            Configuration = configuration;
            DB = context;
        }
        //Upload Squeeze Picture
        [HttpPost]
        [Route("upload/picture/squeeze")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.File>> UploadSqueezePicture(
            IFormFile file,
            CancellationToken cancellationToken)
        {
            if (CheckAccessPictureFile(file))
            {
                Models.File? pic = await WritePictureSqueezeFile(file);
                if (pic != null)
                {
                    DB.Files.Add(pic);
                    DB.SaveChanges();
                    return Ok(pic);
                }
                else return BadRequest(new { message = "Error while loading!" });
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension!" });
            }
        }

        private bool CheckAccessPictureFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".tiff" ||
                    extension == ".jpg" ||
                    extension == ".raw" ||
                    extension == ".jpeg" ||
                    extension == ".bmp" ||
                    extension == ".gif" ||
                    extension == ".png"); // Change the extension based on your need
        }
        private async Task<Models.File> WritePictureSqueezeFile(IFormFile file)
        {
            string PreviewVideoPicWebPart = "";
            string fileName;
            Models.File? pic = null;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var ticks = DateTime.Now.Ticks;
                fileName = ticks + extension; //Create a new Name for the file due to security reasons.

                createDirectory();

                var path = Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                MagickImage image = new MagickImage(DirFilePath + "//" + fileName); // Получить объект изображения
                image.Quality = 100; // Выполнить сжатие без потерь
                image.Resize(1000, 1000); // Общая настройка размера
                string SaveToPath = Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, ticks + "squeeze" + extension); // Редактировать путь сохранения
                image.Write(SaveToPath); // Запись в целевой путь в потоке
                image.Dispose(); // Объект освобожден

                FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, ticks + "squeeze" + extension));

                pic = new Models.File
                {
                    OriginalName = file.FileName,
                    UniqueName = ticks + "squeeze" + extension,
                    Size = fileInfo.Length,
                    Type = extension.Replace(".", ""),
                    PreviewVideoPicWebPart = PreviewVideoPicWebPart,
                    WebPath = Configuration.GetConnectionString("Domen") + "/api/file/download/" + ticks + "squeeze" + extension,
                };

                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, fileName)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, fileName));
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }

            return pic;
        }

        private void createDirectory()
        {
            try
            {
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), DirFilePath);

                if (!Directory.Exists(pathFile))
                {
                    Directory.CreateDirectory(pathFile);
                }
            }
            catch (Exception e) { }
        }

        //Upload Original File
        [HttpPost]
        [Route("upload/file/original")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.File>> UploadOriginalFile(
           IFormFile file,
           CancellationToken cancellationToken)
        {
            if (CheckAccessFile(file))
            {
                Models.File? pic = await WriteOriginalFile(file);
                if (pic != null)
                {
                    DB.Files.Add(pic);
                    DB.SaveChanges();
                    return Ok(pic);
                }
                else return BadRequest(new { message = "Error while loading!" });
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension!" });
            }
        }

        private bool CheckAccessFile(IFormFile file)
        {
            /*var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".tiff" ||
                    extension == ".jpg" ||
                    extension == ".raw" ||
                    extension == ".jpeg" ||
                    extension == ".bmp" ||
                    extension == ".gif" ||
                    extension == ".png" ||
                    extension == ".avi" ||
                    extension == ".m4v" ||
                    extension == ".mov" ||
                    extension == ".mp4"
                    );*/ // Change the extension based on your need
            return true;
        }

        private async Task<Models.File> WriteOriginalFile(IFormFile file)
        {
            string PreviewVideoPicWebPart = "";
            string fileName;
            Models.File? pic = null;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var ticks = DateTime.Now.Ticks;
                fileName = ticks + extension; //Create a new Name for the file due to security reasons.

                createDirectory();

                var path = Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                pic = new Models.File
                {
                    OriginalName = file.FileName,
                    UniqueName = fileName,
                    Size = file.Length,
                    Type = extension.Replace(".", ""),
                    PreviewVideoPicWebPart = PreviewVideoPicWebPart,
                    WebPath = Configuration.GetConnectionString("Domen") + "/api/file/download/" + fileName,
                };
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }
            return pic;
        }

        //Upload Original Video
        [HttpPost]
        [Route("upload/video/original")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.File>> UploadOriginalVideo(
           IFormFile file,
           CancellationToken cancellationToken)
        {
            if (CheckAccessVideoFile(file))
            {
                Models.File? video = await WriteVideoOriginalFile(file);
                if (video != null)
                {
                    DB.Files.Add(video);
                    await DB.SaveChangesAsync(cancellationToken);
                    return Ok(video);
                }
                else return BadRequest(new { message = "Error while loading!" });
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension!" });
            }
        }

        private bool CheckAccessVideoFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".avi" ||
                    extension == ".m4v" ||
                    extension == ".mov" ||
                    extension == ".mp4"); // Change the extension based on your need
        }

        private async Task<Models.File> WriteVideoOriginalFile(IFormFile file)
        {
            string PreviewVideoPicWebPart = "";
            string fileName;
            Models.File? pic = null;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var ticks = DateTime.Now.Ticks;
                fileName = ticks + extension; //Create a new Name for the file due to security reasons.

                createDirectory();

                var path = Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                IConversion conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(
                    (Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, fileName)),
                    (Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, ticks + ".png")),
                    //DirFilePath + "/" + ticks + "preview.png", 
                    TimeSpan.FromSeconds(2));
                IConversionResult result = await conversion.Start();


                MagickImage image = new MagickImage(Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, ticks + ".png")); // Получить объект изображения
                image.Quality = 100; // Выполнить сжатие без потерь
                image.Resize(1000, 1000); // Общая настройка размера
                string SaveToPath = Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, ticks + "preview.png"); // Редактировать путь сохранения
                image.Write(SaveToPath); // Запись в целевой путь в потоке
                image.Dispose(); // Объект освобожден

                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, ticks + ".png")))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, ticks + ".png"));
                }


                PreviewVideoPicWebPart = Configuration.GetConnectionString("Domen") + "/api/file/download/" + ticks + "preview.png";


                pic = new Models.File
                {
                    OriginalName = file.FileName,
                    UniqueName = fileName,
                    Size = file.Length,
                    Type = extension.Replace(".", ""),
                    PreviewVideoPicWebPart = PreviewVideoPicWebPart,
                    WebPath = Configuration.GetConnectionString("Domen") + "/api/file/download/" + fileName,
                };
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }
            return pic;
        }

        //[Authorize]
        [HttpGet("download/{uniqueName}")]
        public async Task<ActionResult> DownloadFile(string uniqueName)
        {
            string filePath = (Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, uniqueName)); // Here, you should validate the request and the existance of the file.
            byte[] bytes;
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










































/*

        //[Authorize]
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
                    //DB.Files.Add(pic);
                    //DB.SaveChanges();
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
                Console.WriteLine(e.Message);
            }

            return pic;
        }

        //[Authorize]
        [HttpGet("download/{uniqueName}")]
        public async Task<ActionResult> DownloadFile(string uniqueName)
        {
            string filePath = (Path.Combine(Directory.GetCurrentDirectory(), DirFilePath, uniqueName)); // Here, you should validate the request and the existance of the file.
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
*/
