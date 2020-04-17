using Application.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PhotoHelper : IPhotoHelper
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public PhotoHelper(IConfiguration config, IHostingEnvironment env)
        {
            _config = config;

            _env = env;
        }

        public async Task<string> UploadPhotoAsync(IFormFile file, string folder)
        {
            var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));

            if (_config[$"PhotoExtensions:{ext}"] != null)
            {
                var path = $"{_env.WebRootPath}\\{folder}\\{file.FileName}";

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return file.FileName;
            }

            throw new Exception("Given extension is incorrect!!");
        }

        public void DeletePhotoAsync(string name, string folder)
        {
            var path = $"{_env.WebRootPath}\\{folder}\\{name}";

            if (File.Exists(path)) File.Delete(path);
        }
    }
}