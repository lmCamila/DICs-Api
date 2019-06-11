using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DICs_API.Errors;
using DICs_API.Models;
using DICs_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace DICs_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository _repoUsers;
        public UsersController(IConfiguration configuration)
        {
            _repoUsers = new UsersRepository(configuration);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Recupera Usuário identificado por seu {id}.",
                          Tags = new[] { "Users" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(Users))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Get([FromRoute][SwaggerParameter("Id do usuário que será obtido.")]int id)
        {
            var model = _repoUsers.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera TODOS os Usuários.",
                          Tags = new[] { "Users" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<Users>))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult GetAll()
        {
            var list = _repoUsers.GetAll();
            return Ok(list);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Insere um novo usuário..",
                          Tags = new[] { "Users" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 201, Type = typeof(Users))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 400)]
        public IActionResult Insert([Bind("Name, Avatar, Email, Department, Process, IsLeaderDepartment, IsLeaderProcess")]UsersUpload users)
        {
            if (ModelState.IsValid)
            {
                var result = _repoUsers.Insert(users);
                var lastResult = result ? _repoUsers.GetLastInserted() : null;
                var uri = Url.Action("Get", new { Id = lastResult.Id, Version = "1.0" });
                return Created(uri, lastResult);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui um usuário.",
                          Tags = new[] { "Users" })]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult Delete([FromRoute] int id)
        {
            var department = _repoUsers.Get(id);
            if (department == null)
            {
                return NotFound();
            }
            _repoUsers.Delete(id);
            return NoContent();
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Altera um usuário.",
                          Tags = new[] { "Users" })]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 400)]
        public IActionResult Update([Bind("Id, Name, Avatar, Email, Department, Process, IsLeaderDepartment, IsLeaderProcess")]UsersUpload users)
        {
            if (ModelState.IsValid)
            {
                _repoUsers.Update(users);
                return Ok();
            }

            return BadRequest();
        }
    }

}