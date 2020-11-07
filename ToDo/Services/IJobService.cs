using ApiModels;
using MssqlRepository.ReturnModel;
using System.Threading.Tasks;

namespace Services
{
    public interface IJobService
    {
        Task<BaseResponse> Add(AddJobRequest model);
        Task<BaseResponse> Done(JobDoneRequest model);
        Task<BaseResponse> GetActive(int type = -1);
        Task<BaseResponse> GetAll();
        Task<BaseResponse> GetIsDone(int type = -1);
        Task<BaseResponse> GetWithFilter(JobFilterRequest filter);
        Task<BaseResponse> GetWithType(int type);
    }
}