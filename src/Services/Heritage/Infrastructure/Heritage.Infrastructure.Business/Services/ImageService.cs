using AutoMapper;
using Heritage.Domain.Core.Entities;
using Heritage.Domain.Interfaces.Repositories;
using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Models.Construction;
using Heritage.Services.Interfaces.Models.Image;
using System;
using System.Threading.Tasks;

namespace Heritage.Infrastructure.Business.Services
{
    public class ImageService : IImageService
    {
        public readonly IImageRepository _imageRepository;
        public readonly IConstructionRepository _constructionRepository;
        public readonly IFileService _fileService;
        public readonly IMapper _mapper;
        public readonly IEventBus _rabbitMQService;

        public ImageService(IImageRepository imageRepository, IConstructionRepository constructionRepository, IFileService fileService, IMapper mapper, IEventBus rabbitMQService)
        {
            _imageRepository = imageRepository;
            _constructionRepository = constructionRepository;
            _fileService = fileService;
            _mapper = mapper;
            _rabbitMQService = rabbitMQService;
        }

        //Task<UserResponseCoreModel> DeleteImageAsync(ImageViewModel imageRequestModel);

        //Task<byte[]> GetImageByIdAsync(Guid id);

        public async Task<ImageCoreModel> AddImageAsync(ImageRequestModel imageRequestModel)
        {
            var startEndpoint = $"[{DateTime.UtcNow}] - EndPoint: [/addpic]  - Id: {imageRequestModel.Id} ";

            var image = new Image
            {
                ConstructionId = imageRequestModel.ConstructionId,
                Name = await _fileService.SaveImageFileAsync(imageRequestModel)
            };

            var construction = await _constructionRepository.GetByIdAsync(imageRequestModel.ConstructionId);

            var create = await _imageRepository.CreateAsync(image);

            construction.Images.Add(image);

            await _constructionRepository.UpdateAsync(construction);

            var result = _mapper.Map<ImageCoreModel>(create);

            try
            {
                _rabbitMQService.Publish(startEndpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
