using DICs_API.Errors;
using DICs_API.Models;
using DICs_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

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
        [SwaggerOperation(Summary = "Recupera processo identificado pelo seu {id}.",
                          Tags = new[] { "Process" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(Process))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponseFilter))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Get([FromRoute][SwaggerParameter("Id do processo.")]int id)
        {
            var model = _repoProcess.Get(id);
            if(model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Todos os processos.",
                          Tags = new[] { "Process" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<Process>))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponseFilter))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult GetAll()
        {
            var list = _repoProcess.GetAll();
            return Ok(list);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui um processo.",
                          Tags = new[] { "Process" })]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponseFilter))]
        [ProducesResponseType(statusCode: 404)]
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
        [SwaggerOperation(Summary = "Altera um processo.",
                          Tags = new[] { "Process" })]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponseFilter))]
        [ProducesResponseType(statusCode: 400)]
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
        [SwaggerOperation(Summary = "Insere um processo.",
                          Tags = new[] { "Process" })]
        [ProducesResponseType(statusCode: 201, Type = typeof(Process))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponseFilter))]
        [ProducesResponseType(statusCode: 400)]
        public IActionResult Insert([FromBody]ProcessUpload process)
        {
            if (ModelState.IsValid)
            {
                var result = _repoProcess.Insert(process);
                var lastResult = _repoProcess.GetLastInserted();
                var uri = Url.Action("Get", new { Id = lastResult.Id, Version = "1.0" });
                return Created(uri, lastResult);
            }
            return BadRequest();
        }

    }
}
