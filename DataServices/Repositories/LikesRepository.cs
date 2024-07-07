using DataServices.Data;
using DataServices.Interfaces;
using Microsoft.Extensions.Logging;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repositories
{
    public class LikesRepository : GenericRepository<Likes>, ILikesRepository
    {
        public LikesRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }

        // ... (Các method khác nếu cần)
    }
}
