using Heritage.Services.Interfaces.Models.Image;
using System.Threading.Tasks;

namespace Heritage.Services.Interfaces.Contracts
{
    public interface IImageService
    {
        Task<ImageCoreModel> AddImageAsync(ImageRequestModel imageRequestModel);
    }
}
