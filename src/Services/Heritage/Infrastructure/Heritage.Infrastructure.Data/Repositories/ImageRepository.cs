using Heritage.Domain.Core.Entities;
using Heritage.Domain.Interfaces.Repositories;
using Heritage.Infrastructure.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Heritage.Infrastructure.Data.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(DbContext context) : base(context)
        {

        }
    }
}