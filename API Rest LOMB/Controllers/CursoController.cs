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
    // Ruta base api/v1/curso
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Curso> _logger;

        public CursoController(ILogger<AccesoDatos.Curso> logger)
        {
            _logger = logger;

        }
        
        // api/v1/curso/byname?name=x
        [HttpGet("byname")]
        public IActionResult GetByName()
        {
            var Cursos = AccesoDatos.Curso.GetByName(Request.Query["name"][0]);
            return Ok(JsonConvert.SerializeObject(Cursos, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        // api/v1/curso
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Curso.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }
        
        // api/v1/curso/1
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var Curso = AccesoDatos.Curso.GetById(id);
            if (Curso != null) return Ok(JsonConvert.SerializeObject(Curso, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }

        // api/v1/curso/create
        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Curso Curso = JsonConvert.DeserializeObject<AccesoDatos.Curso>(json);
            return AccesoDatos.Curso.Create(Curso);
        }

        // api/v1/curso/delete/1
        [HttpDelete("delete/{id}")]
        public bool Delete(int id)
        {
            return AccesoDatos.Curso.Delete(id);
        }

        // api/v1/curso/update/1
        [HttpPut("update/{id}")]
        public async Task<bool> Update(int id)
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Curso Curso = JsonConvert.DeserializeObject<AccesoDatos.Curso>(json);
            return AccesoDatos.Curso.Update(Curso);
        }
    }
}
