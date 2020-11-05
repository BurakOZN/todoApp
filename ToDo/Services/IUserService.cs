using ApiModels;
using MssqlRepository.ReturnModel;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<BaseResponse> Login(LoginInfo loginInfo);
    }
}