using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Services.Interfaces.Contracts.Generic
{
    public interface IGenericService<TCoreModel>
        where TCoreModel : class
    {
        Task<IEnumerable<TCoreModel>> GetAllAsync();

        Task<TCoreModel> GetIdAsync(int id);

        Task<TCoreModel> CreateAsync(TCoreModel item);

        Task<TCoreModel> UpdateAsync(TCoreModel item);

        Task DeleteAsync(int id);
    }
}