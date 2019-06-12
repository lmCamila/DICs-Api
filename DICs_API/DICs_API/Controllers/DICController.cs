using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DICs_API.Errors;
using DICs_API.Models;
using DICs_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Recupera DIC identificado por seu {id}.",
                          Tags = new[] { "DICs" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(DIC))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404)]
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
        [SwaggerOperation(Summary = "Recupera todos DICs.",
                          Tags = new[] { "DICs" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(List<DIC>))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404)]
        public IActionResult GetAll()
        {
            var list = _repoDIC.GetAll();
            return Ok(list);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Insere um novo dic..",
                          Tags = new[] { "DICs" },
                          Produces = new[] { "application/json" })]
        [ProducesResponseType(statusCode: 201, Type = typeof(DIC))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 400)]
        public IActionResult Insert([Bind("IdUser,IdStatus, IdPeriod,Description,FinishedDate")]DICUpload dic)
        {
            if (ModelState.IsValid)
            {
                var result = _repoDIC.Insert(dic);
                if (!result)
                {
                    return BadRequest();
                }
                var lastDic = _repoDIC.GetLastInserted();
                var uri = Url.Action("Get", new { Id = lastDic.Id, Version = "1.0" });
                return Created(uri, lastDic);
            }
            return BadRequest();
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Altera um DIC.",
                          Tags = new[] { "Dics" })]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 400)]
        public IActionResult Update([Bind("Id,IdUser,IdStatus, IdPeriod,Description,FinishedDate")] DICUpload dic)
        {
            if (ModelState.IsValid)
            {
                _repoDIC.Update(dic);
                return Ok();
            }
            return BadRequest();
        }

    }
}