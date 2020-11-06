using ApiModels;
using MssqlRepository.ReturnModel;
using System.Threading.Tasks;

namespace Services
{
    public interface IJobService
    {
        Task<BaseResponse> Add(AddJobRequest model);
        Task<BaseResponse> Done(JobDoneRequest model);
        Task<BaseResponse> GetActive();
        Task<BaseResponse> GetAll();
        Task<BaseResponse> GetWithType(int type);
    }
}