using DAL;
using Entity;
using MssqlRepository.Helper;

namespace MssqlRepository
{
    public sealed class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly TodoContext _context;
        private readonly IParameter _parameterService;
        public JobRepository(TodoContext context, IParameter parameterService) : base(context, parameterService)
        {
            _context = context;
            _parameterService = parameterService;
        }
    }
    public interface IJobRepository : IRepository<Job>
    {

    }
}
