using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using API_Rest_LOMB.Utils;

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
        [HttpGet("byname")]
        public IActionResult GetByName()
        { 
            var encargados = AccesoDatos.Encargado.GetByName(Request.Query["name"][0]);
            return Ok(JsonConvert.SerializeObject(encargados, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Encargado.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var encargado = AccesoDatos.Encargado.GetById(id);
            if (encargado != null) return Ok(JsonConvert.SerializeObject(encargado, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
            else return BadRequest("ID no válido");
        }
        
        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Encargado encargado = JsonConvert.DeserializeObject<AccesoDatos.Encargado>(json);
            encargado.contraseña = Encryptor.MD5Hash(encargado.contraseña);
            return AccesoDatos.Encargado.Create(encargado);
        }
    
        [HttpDelete("delete/{id}")]
        public bool Delete(int id)
        {
            return AccesoDatos.Encargado.Delete(id);
        }

        [HttpPut("update/{id}")]
        public async Task<bool> Update(int id)
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Encargado encargado = JsonConvert.DeserializeObject<AccesoDatos.Encargado>(json); 
            return AccesoDatos.Encargado.Update(encargado);
        }

        [HttpGet("check")]
        public async Task<bool> CheckPassword()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            var data = (JObject)JsonConvert.DeserializeObject(json);
            string username = data.SelectToken("username").Value<string>();
            string password = data.SelectToken("password").Value<string>();
            var list = AccesoDatos.Encargado.GetAll();
            foreach(var e in list)
            {
                if(e.nombre_usuario == username)
                {
                    return password == e.contraseña;
                }
            }
            return false;

        }

    }

}


