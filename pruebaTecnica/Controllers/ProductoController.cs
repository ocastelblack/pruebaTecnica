using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaTecnica.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;


namespace pruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly PruebaContext _dbcontextP;

        public ProductoController(PruebaContext dbcontext)
        {
            _dbcontextP = dbcontext;
        }

        [HttpPost]
        [Route("GuardarProducto")]

        public IActionResult GuardarProducto([FromBody] Producto objeto)
        {
            Cliente oCliente = _dbcontextP.Clientes.Find(objeto.IdCliente);
            String tipoCuenta = (objeto.TipoCuenta).ToString();
            int saldo = Int32.Parse((objeto.Saldo).ToString());

            if (oCliente == null)
            {
                return BadRequest("El cliente no existe por favor crearlo");
            }

            if( tipoCuenta != "cuenta corriente" && tipoCuenta != "cuenta de ahorros")
            {
                return BadRequest("Tipo de cuenta no valida");
            }

            if (tipoCuenta == "cuenta de ahorros" && saldo == 0)
            {
                return BadRequest("Cuenta corriente no puede ir con saldo cero");
            }

            if (tipoCuenta == "cuenta de ahorros")
            {
                //List<Producto> lista = new List<Producto>();
                //lista = _dbcontextP.Productos.ToList();
                long numero = 0;
                numero = Convert.ToInt64(
                        $"{new Random().Next(10000, 49999)}{new Random().Next(50000, 99999)}");

                String numeroCuentaA = "53" + numero;
                objeto.NumeroCuenta = numeroCuentaA;
                objeto.Estado = "activa";
            }

            if (tipoCuenta == "cuenta corriente") 
            {
                //String variable;
                //long numeroAleatorio = 0;
                //do
                //{
                //    numeroAleatorio = Convert.ToInt64(
                //        $"{new Random().Next(10000, 49999)}{new Random().Next(50000, 99999)}");

                //    Producto numero = _dbcontextP.Productos.Contains<numeroAleatorio>;
                //    variable = numero.ToString();

                //} while (variable != null);
                long numeroAleatorio = 0;
                numeroAleatorio = Convert.ToInt64(
                        $"{new Random().Next(10000, 49999)}{new Random().Next(50000, 99999)}");

                String numeroCuentaC = "33" + numeroAleatorio;
                objeto.Saldo = Int32.Parse(numeroCuentaC);
            }

            DateTime fechaCreacion = DateTime.Now;
            objeto.FechaCreacion = fechaCreacion;

            try
            {
                _dbcontextP.Productos.Add(objeto);
                _dbcontextP.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarEstado")]

        public IActionResult EditarEstado([FromBody] Producto objeto)
        {
            Producto oProducto= _dbcontextP.Productos.Find(objeto.IdProductos);

            if (oProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            if (oProducto.Saldo != 0 && objeto.Estado == "Cancelar")
            {
                return BadRequest("No se puede cancelar la cuenta");
            }

            try
            {
                oProducto.Estado = objeto.Estado is null ? oProducto.Estado : objeto.Estado;

                DateTime fechaModificacion = DateTime.Now;
                oProducto.FechaModificacion = fechaModificacion;


                _dbcontextP.Productos.Update(oProducto);
                _dbcontextP.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

    }

    
}
