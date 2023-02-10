using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaTecnica.Models;
using System.Text.RegularExpressions;

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
            String longitudNombre = (objeto.Nombres).ToString();
            String longitudApellido = (objeto.Apellido).ToString();

            if (longitudNombre.Length<2)
            {
                return BadRequest("El nombre no es valido");
            }

            if (longitudApellido.Length < 2)
            {
                return BadRequest("El nombre no es valido");
            }

            //var addr = new System.Net.Mail.MailAddress((objeto.Corre).ToString());

            if(Regex.IsMatch((objeto.Corre).ToString(), 
                @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$") == false)
            {
                return BadRequest("El correo no es valido");
            }

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
