﻿using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories.Generic;

namespace Identity.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        //Task<IPagedList<UserEntity>> GetUsingPaginationAsync(int pageNumber, int pageSize);
    }
}
