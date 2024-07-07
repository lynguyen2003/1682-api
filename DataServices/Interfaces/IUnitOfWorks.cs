using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Interfaces
{
    public interface IUnitOfWorks
    {
        IAuthsRepository Auths { get; }
        IVideosRepository Videos { get; }
        ILikesRepository Likes { get; }
        Task<bool> CompleteAsync();

    }
}
