using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        protected readonly ISender _Sender;
        public ApiController(ISender sender)
        {
            _Sender = sender;
        }
    }
}
