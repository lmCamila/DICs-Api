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
    public class DICController : ControllerBase
    {
        private readonly DICRepository _repoDIC;
        public DICController(IConfiguration configuration)
        {
            _repoDIC = new DICRepository(configuration);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var model = _repoDIC.Get(id);
            if(model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _repoDIC.GetAll();
            return Ok(list);
        }
    }
}