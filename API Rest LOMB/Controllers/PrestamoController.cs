using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccesoDatos.Models;

namespace API_Rest_LOMB.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = Prestamo.GetAll();
            return Ok(JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented }));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            if (Prestamo.Delete(id))
            {
                return Ok("Borrado");
            }
            else return BadRequest();
        }
    }
}
