using CrudWebSocket2.Hubs;
using CrudWebSocket2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;

namespace CrudWebSocket2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public ClienteController(AppDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> ObtenerLista()
        {
            var clientes = await _context.Cliente.ToListAsync();
            return Ok(clientes);
        }

        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] ClienteModels cliente)
        {
            if (cliente == null)
                return BadRequest("Campos vacíos");

            var clienteExiste = await _context.Cliente.FindAsync(cliente.Id);
            if (clienteExiste != null)
                return BadRequest("El cliente ya existe");

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Notificar a todos los clientes que un nuevo cliente fue creado
            await _hubContext.Clients.All.SendAsync("ClienteCreado", cliente);

            return Ok("El cliente se agregó");
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteModels cliente)
        {
            var existe = await _context.Cliente.FindAsync(id);
            if (existe == null)
                return BadRequest("No encontrado");

            existe.Nombre = cliente.Nombre;
            existe.Email = cliente.Email;

            await _context.SaveChangesAsync();

            // Notificar a todos los clientes que un cliente fue actualizado
            await _hubContext.Clients.All.SendAsync("ClienteActualizado", existe);

            return Ok("El cliente se actualizó");
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
                return BadRequest("No encontrado");

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            // Notificar a todos los clientes que un cliente fue eliminado
            await _hubContext.Clients.All.SendAsync("ClienteEliminado", id);

            return Ok("Cliente eliminado");
        }
    }
}
