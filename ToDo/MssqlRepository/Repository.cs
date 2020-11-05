using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;
using MssqlRepository.Helper;
using MssqlRepository.ReturnModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MssqlRepository
{
    public abstract class Repository<T> where T : BaseEntity
    {
        private readonly TodoContext _context;
        private readonly IParameter _parameterService;
        public Repository(TodoContext context, IParameter parameterService)
        {
            _context = context;
            _parameterService = parameterService;
        }
        public virtual async Task<BaseResponse> Get()
        {
            try
            {
                var result = await _context.Set<T>().Where(x => x.IsActive).ToListAsync();
                return new SuccessResponse(result);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(State.ConnectionError, "System Error", ex.Message);
            }

        }
        public virtual async Task<BaseResponse> Get(Expression<Func<T, bool>> where)
        {
            try
            {
                var result = await _context.Set<T>().Where(where).Where(x => x.IsActive).ToListAsync();
                return new SuccessResponse(result);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(State.ConnectionError, "System Error", ex.Message);
            }

        }
        public virtual async Task<BaseResponse> Add(T entity, bool isGenerateId = true)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = _parameterService.Id;
            if (isGenerateId)
                entity.Id = Guid.NewGuid().ToString();
            try
            {
                var x = await _context.AddAsync(entity);
                return await SaveChanges(entity);
            }
            catch (Exception ex)
            {
                //todo: Exception messaj türlerine göre handle edilmeli.
                return new ErrorResponse(State.ConnectionError, "System Error", ex.Message);
            }
        }
        public virtual async Task<BaseResponse> Add(List<T> entities, bool isGenerateId = true)
        {
            if (isGenerateId)
                foreach (var item in entities)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = _parameterService.Id;
                }
            try
            {
                await _context.AddRangeAsync(entities);
                return await SaveChanges(entities);
            }
            catch (Exception ex)
            {
                //todo: Exception messaj türlerine göre handle edilmeli.
                return new ErrorResponse(State.ConnectionError, "System Error", ex.Message);
            }
        }
        public virtual async Task<BaseResponse> Find(string id)
        {
            try
            {
                var obj = await _context.Set<T>().FindAsync(id);
                if (obj == null || obj.IsActive == false)
                    return new ErrorResponse(State.NotFound, "Error", "Object not found: " + id);
                return new SuccessResponse(obj);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(State.ConnectionError, "System Error", "Object not found");
            }
        }
        public virtual async Task<BaseResponse> Update(T entity, bool forDelete = false)
        {
            entity.UpdatedBy = _parameterService.Id;
            entity.LastUpdatedDate = DateTime.Now;
            var old = await Find(entity.Id);
            if (old.State != State.Success)
                return old;
            try
            {
                if (forDelete)
                    entity.IsActive = false;
                _context.Entry(old.Result).CurrentValues.SetValues(entity);
                return await SaveChanges(entity);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(State.ConnectionError, "System Error", ex.Message);
            }
        }
        public virtual async Task<BaseResponse> Delete(string id)
        {
            var result = await Find(id);
            if (result.State != State.Success)
                return result;

            return await Update((T)result.Result, true);
        }
        private async Task<BaseResponse> SaveChanges(T data)
        {
            try
            {
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                    return new SuccessResponse(res);
                else
                    return new ErrorResponse(res, State.ProcessUnsuccess, "Process Failed", "");
            }
            catch (Exception ex)
            {
                //todo: Exception messaj türlerine göre handle edilmeli.
                return new ErrorResponse(State.SaveError, "System Error", ex.Message);
            }
        }
        private async Task<BaseResponse> SaveChanges(List<T> datas)
        {
            try
            {
                var res = await _context.SaveChangesAsync();
                if (res == datas.Count)
                    return new SuccessResponse(res);
                else
                    return new ErrorResponse(res, State.ProcessUnsuccess, "Process Failed", "");
            }
            catch (Exception ex)
            {
                //todo: Exception messaj türlerine göre handle edilmeli.
                return new ErrorResponse(State.SaveError, "System Error", ex.Message);
            }
        }
    }
}
