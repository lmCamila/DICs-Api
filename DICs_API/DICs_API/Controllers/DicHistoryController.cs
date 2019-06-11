using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DICs_API.Models;
using DICs_API.Repositories;
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

    public class DicHistoryController : ControllerBase
    {
        private readonly DicHistoryRepository _repoHistory;
        private readonly DICRepository _repoDic;
        public DicHistoryController(IConfiguration configuration)
        {
            _repoHistory = new DicHistoryRepository(configuration);
            _repoDic = new DICRepository(configuration);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var model = _repoHistory.GetAll(id).Select(l => l).ToDicHistoryConfig(_repoDic.Get(id));
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Insert([Bind("IdDic,IdStatus,Note,Type")]DicHistoryUpload dic)
        {
            if (ModelState.IsValid)
            {
                var result = _repoHistory.Insert(dic);
                
                var lastDic = _repoHistory.GetLastInserted();
                var uri = Url.Action("Get", new { Id = lastDic.Id, Version = "1.0" });
                return Created(uri, lastDic);
            }
            return BadRequest();
        }
    }
}