using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pruebaTecnica.Models;
using System;
using System.Text.RegularExpressions;

namespace pruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        public readonly PruebaContext _dbcontext;

        public TransaccionesController(PruebaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpPost]
        [Route("CosignacionRetiro")]

        public IActionResult CosignacionRetiro([FromBody] Transaccione objeto)
        {
            Producto oProducto = _dbcontext.Productos.Find(objeto.IdProducto);
            Cliente oCliente = _dbcontext.Clientes.Find(objeto.IdCliente);

            String tipo = (objeto.TipoTransaccion).ToString();

            int saldoAnterior = Int32.Parse((oProducto.Saldo).ToString());

            if (tipo.Equals("Consignación"))
            {
                oProducto.Saldo = saldoAnterior + objeto.montoTransaccion;
                objeto.DescripcionTransaccion = "Cosignacion exitosa para la " + oProducto.TipoCuenta + " numero " + oProducto.NumeroCuenta + " del cliente " + oCliente.Nombres + " " + oCliente.Apellido;
            }


            if (tipo.Equals("Retiro"))
            {
                oProducto.Saldo = saldoAnterior - objeto.montoTransaccion;
                objeto.DescripcionTransaccion = "Cosignacion exitosa para la " + oProducto.TipoCuenta + " numero " + oProducto.NumeroCuenta + " del cliente " + oCliente.Nombres + " " + oCliente.Apellido;
            }

            try
            {
                _dbcontext.Transacciones.Add(objeto);
                _dbcontext.Productos.Update(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = objeto.DescripcionTransaccion });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
