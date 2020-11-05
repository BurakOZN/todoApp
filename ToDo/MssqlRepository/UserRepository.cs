using DAL;
using Entity;
using MssqlRepository.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MssqlRepository
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {
        private readonly TodoContext _context;
        private readonly IParameter _parameterService;
        public UserRepository(TodoContext context, IParameter parameterService) : base(context, parameterService)
        {
            _context = context;
            _parameterService = parameterService;
        }
    }
    public interface IUserRepository : IRepository<User>
    {

    }
}
