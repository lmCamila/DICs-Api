using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DICs_API.Rerpositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DICs_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly StatusRepository _repoStatus;
        public StatusController(IConfiguration configuration)
        {
            _repoStatus = new StatusRepository(configuration);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _repoStatus.GetAll();
            return Ok(list);
        }
    }
}