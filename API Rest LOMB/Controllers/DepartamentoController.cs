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
    public class DepartamentoController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Departamento> _logger;

        public DepartamentoController(ILogger<AccesoDatos.Departamento> logger)
        {
            _logger = logger;

        }

        [HttpGet("byname")]
        public IActionResult GetByName()
        {
            var Departamentos = AccesoDatos.Departamento.GetByName(Request.Query["name"][0]);
            return Ok(JsonConvert.SerializeObject(Departamentos, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Departamento.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var Departamento = AccesoDatos.Departamento.GetById(id);
            if (Departamento != null) return Ok(JsonConvert.SerializeObject(Departamento, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }

        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Departamento departamento = JsonConvert.DeserializeObject<AccesoDatos.Departamento>(json);
            return AccesoDatos.Departamento.Create(departamento);
        }

        [HttpDelete("delete/{id}")]
        public bool Delete(int id)
        {
            return AccesoDatos.Departamento.Delete(id);
        }

        [HttpPut("update/{id}")]
        public async Task<bool> Update(int id)
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Departamento Departamento = JsonConvert.DeserializeObject<AccesoDatos.Departamento>(json);
            return AccesoDatos.Departamento.Update(Departamento);
        }
    }
}
