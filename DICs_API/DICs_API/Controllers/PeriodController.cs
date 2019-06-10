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
    public class PeriodController : ControllerBase
    {
        private readonly PeriodRepository _repoPeriod;

        public PeriodController(IConfiguration configuration) {
            _repoPeriod = new PeriodRepository(configuration);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var model = _repoPeriod.Get(id);
            if(model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _repoPeriod.GetAll();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Insert([FromBody]Period period)
        {
            if (ModelState.IsValid)
            {
                var result = _repoPeriod.Insert(period);
                var lastResult = result ? _repoPeriod.GetLastInserted() : null;
                var uri = Url.Action("Get", new { Id = lastResult.Id });
                return Created(uri, lastResult);
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete([FromRoute]int id)
        {
            var period = _repoPeriod.Get(id);
            if(period == null)
            {
                return NotFound();
            }
            _repoPeriod.Delete(id);

            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([Bind("Id, Months, Name")]Period period)
        {
            if (ModelState.IsValid)
            {
                _repoPeriod.Update(period);
                return Ok();
            }
            return BadRequest();
        }
    }
}
