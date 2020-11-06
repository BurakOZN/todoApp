using ApiModels;
using MssqlRepository.ReturnModel;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<BaseResponse> AddUser(AddUserRequest model);
        Task<BaseResponse> Login(LoginInfo loginInfo);
    }
}