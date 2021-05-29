using Identity.Services.Interfaces.Models.Sorting;
using Identity.Services.Interfaces.Models.User;
using Identity.Services.Interfaces.Models.User.ConfirmRegister;
using Identity.Services.Interfaces.Models.User.ForgotPassword;
using Identity.Services.Interfaces.Models.User.Login;
using Identity.Services.Interfaces.Models.User.Profile;
using Identity.Services.Interfaces.Models.User.Register;
using System;
using System.Threading.Tasks;

namespace Identity.Services.Interfaces.Contracts
{
    public interface IUserService
    {
        Task<IndexViewModel<UserResponseCoreModel>> GetUsingPaginationAsync(Guid? user, string email, int pageNumber, int pageSize, SortState sortOrder);

        Task<UserResponseCoreModel> GetUserByIdAsync(Guid id);

        Task<UserResponseCoreModel> SetUserRole(SetRoleRequestModel userResponseCoreModel);

        Task<UserResponseCoreModel> UpdateProfileAsync(ProfileRequestModel profileRequestModel);

        Task<UserResponseCoreModel> UpdateImageAsync(ImageViewModel imageRequestModel);

        Task<RegisterResponseModel> AuthenticateAsync(LoginRequestModel loginCoreModel);

        Task<RegisterResponseModel> RegisterAsync(RegisterRequestModel requestModel);

        Task ConfirmRegisterAsync();

        Task ConfirmEmailAsync(ConfirmRequestModel model);

        Task ForgotPasswordAsync(ForgotPasswordRequestModel model);

        Task<UserResponseCoreModel> DeleteImageAsync(ImageViewModel imageRequestModel);

        Task DeleteUserAsync(Guid id);
    }
}
