using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Helpers.Options;
using Heritage.Services.Interfaces.Models.Image;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Heritage.Infrastructure.Business.Support.File
{
    public class FileService : IFileService
    {
        private readonly FileSettings _fileSettings;

        public FileService(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;
        }

        public async Task<string> SaveImageFileAsync(ImageRequestModel imageRequestModel)
        {
            var imageName = new string(Path.GetFileNameWithoutExtension(imageRequestModel.ConstructionId.ToString()).Take(10).ToArray()).Replace(' ', '-') + "-" + Path.GetFileNameWithoutExtension(imageRequestModel.File.FileName);
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
            var imagePath = _fileSettings.Path + imageName;
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

        public async Task<string> ConvertPathToBase64String(string path)
        {
            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            var base64 = Convert.ToBase64String(bytes);

            return base64;
        }
    }
}
