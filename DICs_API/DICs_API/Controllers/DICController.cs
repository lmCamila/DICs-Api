using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DICs_API.Models;
using DICs_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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

        [HttpPost]
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