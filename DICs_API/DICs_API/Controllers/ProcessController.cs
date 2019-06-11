using DICs_API.Models;
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
    public class ProcessController : ControllerBase
    {
        private readonly ProcessRepository _repoProcess;
        public ProcessController(IConfiguration configuration)
        {
            _repoProcess = new ProcessRepository(configuration);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var model = _repoProcess.Get(id);
            if(model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _repoProcess.GetAll();
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var process = _repoProcess.Get(id);
            if(process == null)
            {
                return NotFound();
            }

            _repoProcess.Delete(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([Bind("Id, Name, IdDepartment")]ProcessUpload process)
        {
            if (ModelState.IsValid)
            {
                _repoProcess.Update(process);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Insert([FromBody]ProcessUpload process)
        {
            if (ModelState.IsValid)
            {
                var result = _repoProcess.Insert(process);
                var lastResult = result ? _repoProcess.GetLastInserted() : null;
                var uri = Url.Action("Get", new { Id = lastResult.Id });
                return Created(uri, lastResult);
            }
            return BadRequest();
        }

    }
}
