using Business.Interfaces;
using Core.ModelsView;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiblioNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoServices _services;

        public PrestamoController(IPrestamoServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listaPrestamos = _services.ConsultarServicios();
                return Ok(listaPrestamos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var prestamo = _services.Buscar(id);
                if (prestamo == null)
                {
                    return NotFound("Préstamo no encontrado");
                }
                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PrestamoView prestamo)
        {
            try
            {
                var nuevoPrestamo = _services.Agregar(prestamo.UsuarioId, prestamo.LibroId, prestamo.FechaPrestamo, prestamo.FechaDevolucion, prestamo.Estado);
                return Ok(new { message = "Agregado con éxito", prestamo = nuevoPrestamo });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PrestamoView prestamo)
        {
            try
            {
                var prestamoActualizado = _services.Actualizar(id, prestamo);
                return Ok(prestamoActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resultado = _services.Eliminar(id);
                return Ok(new { message = "Eliminado con éxito", id = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
