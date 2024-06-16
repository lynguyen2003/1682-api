using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Interfaces
{
    public interface IUnitOfWorks
    {
        IVideosRepository Videos { get; }
        IAuthsRepository Auths { get; }
        Task<bool> CompleteAsync();

    }
}
