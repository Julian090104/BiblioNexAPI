using Business.Interfaces;
using Core.ModelsView;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _services;

        public UsuarioController(IUsuarioServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listaUsuarios = _services.ConsultarServicios();
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var usuario = _services.Buscar(id);
                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioView usuario)
        {
            try
            {
                var nuevoUsuario = _services.Agregar(usuario.UsuarioId, usuario.Nombre, usuario.Apellido, usuario.Email, usuario.Telefono, usuario.Direccion, usuario.FechaRegistro);
                return Ok(new { message = "Agregado con éxito", usuario = nuevoUsuario });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioView usuario)
        {
            try
            {
                var usuarioActualizado = _services.Actualizar(id, usuario);
                return Ok(usuarioActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UsuarioController>/5
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