using Business.Interfaces;
using Core.ModelsView;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BiblioNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroServices _services;

        public LibroController(ILibroServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listaLibros = _services.ConsultarServicios();
                return Ok(listaLibros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                var libro = _services.Buscar(id);
                if (libro == null)
                {
                    return NotFound("Libro no encontrado");
                }
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LibroView libro)
        {
            try
            {
                var nuevoLibro = _services.Agregar(libro.LibroId, libro.Titulo, libro.Autor, libro.Genero, libro.Editorial, libro.AnoPublicacion);
                return Ok(new { message = "Agregado con éxito", libro = nuevoLibro });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] LibroView libro)
        {
            try
            {
                var libroActualizado = _services.Actualizar(id, libro);
                return Ok(libroActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
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
