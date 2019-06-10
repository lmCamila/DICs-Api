using DICs_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DICs_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationRepository _repoConfiguration;
        public ConfigurationController(IConfiguration configuration){
            _repoConfiguration = new ConfigurationRepository(configuration);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var model = _repoConfiguration.Get(id);
            if(model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}
