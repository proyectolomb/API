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
    public class LibroController : ControllerBase
    {
        private readonly ILogger<AccesoDatos.Libro> _logger;

        public LibroController(ILogger<AccesoDatos.Libro> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = AccesoDatos.Libro.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpGet("{isbn}")]
        public IActionResult GetByISBN(string isbn)
        {
            var libro = AccesoDatos.Libro.GetByISBN(isbn);
            return Ok(JsonConvert.SerializeObject(libro, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpPost("create")]
        public async Task<bool> Create()
        {
            string json = await new StreamReader(Request.Body).ReadToEndAsync();
            AccesoDatos.Libro libro = JsonConvert.DeserializeObject<AccesoDatos.Libro>(json);
            return AccesoDatos.Libro.Create(libro);       
        }
    }
}
