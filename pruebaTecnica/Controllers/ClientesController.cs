using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaTecnica.Models;

namespace pruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public readonly PruebaContext _dbcontext;

        public ClientesController(PruebaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista()
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                lista = _dbcontext.Clientes.ToList();  

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = lista });
            }
        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult guardar([FromBody] Cliente objeto)
        {
            //String fechaNacimiento = (objeto.FechaNacimiento).ToString();

            DateTime now = DateTime.Today;
            int edad = DateTime.Today.Year - DateTime.Parse((objeto.FechaNacimiento).ToString()).Year;

            if (edad < 18)
            {
                return BadRequest("El cliente es menor de edad");
            }

            Cliente fcCliente = _dbcontext.Clientes.Find(objeto.FechaCreacion);

            if(fcCliente == null)
            {
                DateTime fechaCreacion = DateTime.Now;
                objeto.FechaCreacion = fechaCreacion;
            }

            try
            {
                _dbcontext.Clientes.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
