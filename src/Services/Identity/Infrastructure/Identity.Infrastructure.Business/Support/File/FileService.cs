using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Helpers.Options;
using Identity.Services.Interfaces.Models.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Business.Support.File
{
    public class FileService : IFileService
    {
        private readonly FileSettings _fileSettings;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileService(IOptions<FileSettings> fileSettings, IWebHostEnvironment hostEnvironment)
        {
            _fileSettings = fileSettings.Value;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> SaveImageFileAsync(ImageViewModel imageRequestModel)
        {
            var imageName = new string(Path.GetFileNameWithoutExtension(imageRequestModel.Id.ToString()).Take(10).ToArray()).Replace(' ', '-');
            //imageName = imageName + DateTime.Now.ToString(_fileSettings.SaveFormat) + Path.GetExtension(imageRequestModel.File.Name);

            //var imageName = imageRequestModel.Id.ToString();

            var imageExtension = Path.GetExtension(imageRequestModel.File.FileName);

            var imagePath = _fileSettings.Path + imageName + imageExtension;

            await using var fileStream = new FileStream(imagePath, FileMode.Create);

            await imageRequestModel.File.CopyToAsync(fileStream);

            return imagePath;
        }

        public void DeleteImageFile(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, _fileSettings.Path, imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}