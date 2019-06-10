using DICs_API.Models;
using DICs_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DICs_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _repoDepartment;

        public DepartmentController(IConfiguration configuration)
        {
            _repoDepartment = new DepartmentRepository(configuration);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var model = _repoDepartment.Get(id);
            if(model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _repoDepartment.GetAll();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Insert([FromBody]Department department)
        {
            if (ModelState.IsValid)
            {
                var result = _repoDepartment.Insert(department);
                var lastResult = result ? _repoDepartment.GetLastInserted() : null;
                var uri = Url.Action("Get", new { Id = lastResult.Id });
                return Created(uri, lastResult);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var department = _repoDepartment.Get(id);
            if(department == null)
            {
                return NotFound();
            }
            _repoDepartment.Delete(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([Bind("Id, Name")]Department department)
        {
            if (ModelState.IsValid)
            {
                _repoDepartment.Update(department);
                return Ok();
            }

            return BadRequest();
        }
    }
}
