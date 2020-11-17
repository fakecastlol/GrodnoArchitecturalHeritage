using AutoMapper;
using Identity.Domain.Core.Entities;
using Identity.Domain.Interfaces.Repositories;
using Identity.Services.Interfaces.Contracts;
using Identity.Services.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleCoreModel>> GetAllAsync()
        {
            var result = _mapper.Map<IEnumerable<RoleEntity>, IEnumerable<RoleCoreModel>>(await _roleRepository.GetAllAsync());

            return result;
        }

        public async Task<RoleCoreModel> GetIdAsync(int id)
        {
            var getEntity = await _roleRepository.GetIdAsync(id);

            var result = _mapper.Map<RoleCoreModel>(getEntity);

            return result;
        }

        public async Task<RoleCoreModel> CreateAsync(RoleCoreModel roleCoreModel)
        {
            var role = _mapper.Map<RoleEntity>(roleCoreModel);

            var createEntity = await _roleRepository.CreateAsync(role);

            var result = _mapper.Map<RoleCoreModel>(createEntity);

            return result;
        }

        public async Task<RoleCoreModel> UpdateAsync(RoleCoreModel roleCoreModel)
        {
            var role = _mapper.Map<RoleEntity>(roleCoreModel);

            var updateEntity = await _roleRepository.UpdateAsync(role);

            var result = _mapper.Map<RoleCoreModel>(updateEntity);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _roleRepository.DeleteAsync(id);
        }
    }
}
