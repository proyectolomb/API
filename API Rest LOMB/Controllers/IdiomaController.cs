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
    // Ruta base api/v1/idioma
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdiomaController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Idioma> _logger;

        public IdiomaController(ILogger<AccesoDatos.Idioma> logger)
        {
            _logger = logger;

        }
        
        // api/v1/idioma/byname?name=X
        [HttpGet("byname")]
        public IActionResult GetByName()
        {
            var Idiomas = AccesoDatos.Idioma.GetByName(Request.Query["name"][0]);
            return Ok(JsonConvert.SerializeObject(Idiomas, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        // api/v1/idioma
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Idioma.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        // api/v1/idioma/1
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var Idioma = AccesoDatos.Idioma.GetById(id);
            if (Idioma != null) return Ok(JsonConvert.SerializeObject(Idioma, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }

        // api/v1/idioma/create
        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Idioma Idioma = JsonConvert.DeserializeObject<AccesoDatos.Idioma>(json);
            return AccesoDatos.Idioma.Create(Idioma);
        }

        // api/v1/idioma/delete/1
        [HttpDelete("delete/{id}")]
        public bool Delete(int id)
        {
            return AccesoDatos.Idioma.Delete(id);
        }
        
        // api/v1/idioma/update/1
        [HttpPut("update/{id}")]
        public async Task<bool> Update(int id)
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Idioma Idioma = JsonConvert.DeserializeObject<AccesoDatos.Idioma>(json);
            return AccesoDatos.Idioma.Update(Idioma);
        }
    }
}
