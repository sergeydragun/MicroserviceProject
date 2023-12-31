using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;

        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _contextAccessor;

        public BaseController(ILogger<BaseController> logger, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

    }
}
