using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccesoDatos.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API_Rest_LOMB.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LectorController : ControllerBase
    {
        private readonly ILogger<Lector> _logger;

        public LectorController(ILogger<Lector> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = Lector.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }
    }
}
