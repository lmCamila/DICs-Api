using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DICs_API.Models;
using DICs_API.Rerpositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        public IActionResult Get([FromRoute]int id)
        {
            var model = _repoUsers.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _repoUsers.GetAll();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Insert([FromBody]Users users)
        {
            if (ModelState.IsValid)
            {
                var result = _repoUsers.Insert(users);
                var lastResult = result ? _repoUsers.GetLastInserted() : null;
                var uri = Url.Action("Get", new { Id = lastResult.Id });
                return Created(uri, lastResult);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
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
        public IActionResult Update([Bind("Id, Name, Avatar, Email, Department, Process, IsLeaderDepartment, IsLeaderProcess")]Users users)
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
}