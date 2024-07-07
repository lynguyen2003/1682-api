using AutoMapper;
using DataServices.Interfaces;
using DataServices.Services.RedisCacheService;
using Microsoft.AspNetCore.Mvc;

namespace _1682_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWorks _unitOfWorks;
        protected readonly IMapper _mapper;
        protected readonly IRedisCacheService _cache;

        public BaseController(IUnitOfWorks unitOfWorks, IMapper mapper, IRedisCacheService cache)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
            _cache = cache;
        }
    }
}
