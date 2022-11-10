using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Rest_LOMB.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdiomaController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Idioma> _logger;

        public IdiomaController(ILogger<AccesoDatos.Idioma> logger)
        {
            _logger = logger;

        }
        [HttpGet("byname")]
        public IActionResult GetByName()
        {
            var Idiomas = AccesoDatos.Idioma.GetByName(Request.Query["name"][0]);
            return Ok(JsonConvert.SerializeObject(Idiomas, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Idioma.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var Idioma = AccesoDatos.Idioma.GetById(id);
            if (Idioma != null) return Ok(JsonConvert.SerializeObject(Idioma, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }

        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Idioma Idioma = JsonConvert.DeserializeObject<AccesoDatos.Idioma>(json);
            return AccesoDatos.Idioma.Create(Idioma);
        }

        [HttpDelete("delete/{id}")]
        public bool Delete(int id)
        {
            return AccesoDatos.Idioma.Delete(id);
        }

        [HttpPut("update/{id}")]
        public async Task<bool> Update(int id)
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Idioma Idioma = JsonConvert.DeserializeObject<AccesoDatos.Idioma>(json);
            return AccesoDatos.Idioma.Update(Idioma);
        }
    }
}
