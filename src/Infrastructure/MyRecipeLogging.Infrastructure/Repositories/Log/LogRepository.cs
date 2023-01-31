
using AutoMapper;
using MyRecipeLogging.Contracts.Log;

namespace MyRecipeLogging.Infrastructure.Repositories.Log
{
    public class LogRepository : ILogRepository
    {
        private readonly MyRecipeLoggingDbContext _context;
        private readonly IMapper _mapper;


        public LogRepository(MyRecipeLoggingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public void AddLog(LogDto logDto)
        {
            try
            {
                _context.Logs.Add(new Domain.Log
                {
                    Message = logDto.Message,
                    MessageType = logDto.MessageType,
                    DateTime = logDto.DateTime,
                });
                _context.SaveChanges();
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }
        }
    }
}
