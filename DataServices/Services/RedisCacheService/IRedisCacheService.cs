using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.RedisCacheService
{
    public interface IRedisCacheService
    {
        Task<bool> SetAsync(string key, string value);
        Task<string> GetAsync(string key);
        Task<bool> DeleteAsync(string key);

    }
}
