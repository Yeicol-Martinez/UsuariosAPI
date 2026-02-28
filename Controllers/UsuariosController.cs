using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;
using UsuariosAPI.Models;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET /api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}." });

            return Ok(usuario);
        }

        // POST /api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validación de correo duplicado
            bool correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo.ToLower() == usuario.Correo.ToLower());

            if (correoExiste)
                return BadRequest(new { mensaje = $"El correo '{usuario.Correo}' ya está en uso por otro usuario." });

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT /api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuarioActualizado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}." });

            // Validación de correo duplicado (ignorando el usuario actual)
            bool correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo.ToLower() == usuarioActualizado.Correo.ToLower() && u.Id != id);

            if (correoExiste)
                return BadRequest(new { mensaje = $"El correo '{usuarioActualizado.Correo}' ya está en uso por otro usuario." });

            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.Correo = usuarioActualizado.Correo;
            usuario.FechaDeNacimiento = usuarioActualizado.FechaDeNacimiento;

            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        // DELETE /api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {id}." });

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Usuario con ID {id} eliminado correctamente." });
        }
    }
}
