using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace API_Rest_LOMB.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EncargadoController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Encargado> _logger;

        public EncargadoController(ILogger<AccesoDatos.Encargado> logger)
        {
            _logger = logger;

        }
        [HttpGet("name={name}")]
        public IActionResult Get(string name)
        {
            var encargados = AccesoDatos.Encargado.GetByName(name);
            return Ok(JsonConvert.SerializeObject(encargados, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = AccesoDatos.Encargado.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var encargado = AccesoDatos.Encargado.GetById(id);
            if (encargado != null) return Ok(JsonConvert.SerializeObject(encargado, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }

        

    }

}
