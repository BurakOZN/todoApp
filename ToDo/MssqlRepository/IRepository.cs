using Entity;
using MssqlRepository.ReturnModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MssqlRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<BaseResponse> Add(List<T> entities, bool isGenerateId = true);
        Task<BaseResponse> Add(T entity, bool isGenerateId = true);
        Task<BaseResponse> Delete(string id);
        Task<BaseResponse> Find(string id);
        Task<BaseResponse> Get();
        Task<BaseResponse> Get(Expression<Func<T, bool>> where);
        Task<BaseResponse> Update(T entity, bool forDelete = false);
    }
}