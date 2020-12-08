using System.Threading.Tasks;
using Identity.Services.Interfaces.Models.User;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IFileService
    {
        Task<string> SaveImageFileAsync(ImageViewModel imageRequestModel);
        void DeleteImageFile(string imageName);
    }
}