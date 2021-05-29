using Heritage.Services.Interfaces.Models.Image;
using System.Threading.Tasks;

namespace Heritage.Services.Interfaces.Contracts
{
    public interface IFileService
    {
        Task<string> SaveImageFileAsync(ImageRequestModel imageRequestModel);
        Task<string> ConvertPathToBase64String(string path);
        void DeleteImageFile(string imageName);
    }
}
