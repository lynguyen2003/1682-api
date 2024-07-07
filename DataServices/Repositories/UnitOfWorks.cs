using DataServices.Data;
using DataServices.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repositories
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;
        
        public IVideosRepository Videos { get; private set; }
        public IAuthsRepository Auths { get; private set; }
        public ILikesRepository Likes { get; private set; }

        public UnitOfWorks(DataContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("log");

            Videos = new VideosRepository(_context, logger);
            Auths = new AuthsRepository(_context, logger);
            Likes = new LikesRepository(_context, logger);
        }


        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
