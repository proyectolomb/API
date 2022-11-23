using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccesoDatos.Models;
using Microsoft.Extensions.Logging;

namespace API_Rest_LOMB.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EjemplarController : ControllerBase
    {
        private readonly ILogger<Ejemplar> _logger;

        public EjemplarController(ILogger<Ejemplar> logger)
        {
            _logger = logger;

        }

        [HttpGet("byisbn/{isbn}")]
        public IActionResult GetByISBN(string isbn)
        {
            var lista = Ejemplar.GetByISBN(isbn);
            return Ok(JsonConvert.SerializeObject(lista, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            var list = Ejemplar.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        
    }
}
