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
    // Ruta base api/v1/departamento
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Departamento> _logger;

        public DepartamentoController(ILogger<AccesoDatos.Departamento> logger)
        {
            _logger = logger;

        }
        
        // api/v1/departamento/byname?name=x
        [HttpGet("byname")]
        public IActionResult GetByName()
        {
            var Departamentos = AccesoDatos.Departamento.GetByName(Request.Query["name"][0]);
            return Ok(JsonConvert.SerializeObject(Departamentos, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        // api/v1/departamento
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Departamento.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        // api/v1/departamento/1
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var Departamento = AccesoDatos.Departamento.GetById(id);
            if (Departamento != null) return Ok(JsonConvert.SerializeObject(Departamento, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }

        // api/v1/departamento/create
        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Departamento departamento = JsonConvert.DeserializeObject<AccesoDatos.Departamento>(json);
            return AccesoDatos.Departamento.Create(departamento);
        }

        // api/v1/departamento/delete/1
        [HttpDelete("delete/{id}")]
        public bool Delete(int id)
        {
            return AccesoDatos.Departamento.Delete(id);
        }

        // api/v1/departamento/update/1
        [HttpPut("update/{id}")]
        public async Task<bool> Update(int id)
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Departamento Departamento = JsonConvert.DeserializeObject<AccesoDatos.Departamento>(json);
            return AccesoDatos.Departamento.Update(Departamento);
        }
    }
}
