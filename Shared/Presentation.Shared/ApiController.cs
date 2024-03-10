using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly ISender _Sender;
        public ApiController(ISender sender)
        {
            _Sender = sender;
        }
    }
}
