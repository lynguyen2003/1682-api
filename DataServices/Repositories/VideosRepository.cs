using DataServices.Data;
using DataServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repositories
{
    public class VideosRepository : GenericRepository<Videos>, IVideosRepository
    {
        public VideosRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Videos>> GetAll()
        {
            try
            {
                return await _dbSet
                    .Include(v => v.Users) 
                    .Include(v => v.Comments) 
                    .Include(v => v.Likes)
                    .Include(v => v.Favourites)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetAll Fuction error", typeof(VideosRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Videos entity)
        {
            try
            {
                _dbSet.Update(entity);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Update Fuction error", typeof(VideosRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                _dbSet.Remove(entity);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Delete Fuction error", typeof(VideosRepository));
                return false;
            }
        }
    }
}
