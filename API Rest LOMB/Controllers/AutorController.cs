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
    public class AutorController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Autor> _logger;

        public AutorController(ILogger<AccesoDatos.Autor> logger)
        {
            _logger = logger;

        }
        [HttpGet("byname")]
        public IActionResult GetByName()
        {
            var Autors = AccesoDatos.Autor.GetByName(Request.Query["name"][0]);
            return Ok(JsonConvert.SerializeObject(Autors, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Autor.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var Autor = AccesoDatos.Autor.GetById(id);
            if (Autor != null) return Ok(JsonConvert.SerializeObject(Autor, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }

        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Autor Autor = JsonConvert.DeserializeObject<AccesoDatos.Autor>(json);
            return AccesoDatos.Autor.Create(Autor);
        }

        [HttpDelete("delete/{id}")]
        public bool Delete(int id)
        {
            return AccesoDatos.Autor.Delete(id);
        }

        [HttpPut("update/{id}")]
        public async Task<bool> Update(int id)
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Autor Autor = JsonConvert.DeserializeObject<AccesoDatos.Autor>(json);
            return AccesoDatos.Autor.Update(Autor);
        }
    }
}
