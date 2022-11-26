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
    // Ruta base api/v1/editorial
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EditorialController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Editorial> _logger;

        public EditorialController(ILogger<AccesoDatos.Editorial> logger)
        {
            _logger = logger;

        }
        
        // api/v1/editorial/byname?name=x
        [HttpGet("byname")]
        public IActionResult GetByName()
        {
            var Editorials = AccesoDatos.Editorial.GetByName(Request.Query["name"][0]);
            return Ok(JsonConvert.SerializeObject(Editorials, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        // api/v1/editorial
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Editorial.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        // api/v1/editorial/1
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var Editorial = AccesoDatos.Editorial.GetById(id);
            if (Editorial != null) return Ok(JsonConvert.SerializeObject(Editorial, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }

        // api/v1/editorial/create
        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Editorial Editorial = JsonConvert.DeserializeObject<AccesoDatos.Editorial>(json);
            return AccesoDatos.Editorial.Create(Editorial);
        }


        // api/v1/editorial/delete/1
        [HttpDelete("delete/{id}")]
        public bool Delete(int id)
        {
            return AccesoDatos.Editorial.Delete(id);
        }

        // api/v1/editorial/update/1
        [HttpPut("update/{id}")]
        public async Task<bool> Update(int id)
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Editorial Editorial = JsonConvert.DeserializeObject<AccesoDatos.Editorial>(json);
            return AccesoDatos.Editorial.Update(Editorial);
        }
    }
}
