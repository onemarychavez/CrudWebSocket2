using CrudWebSocket2.Models;
using Microsoft.AspNetCore.SignalR;

namespace CrudWebSocket2.Hubs
{

    public class ChatHub : Hub
    {
        
//public async Task ClienteActualizado(ClienteModels cliente)
        //{
        //    await Clients.All.SendAsync("ClienteActualizado", cliente);
        //}

        //public async Task ClienteEliminado(int clienteId)
        //{
        //    await Clients.All.SendAsync("ClienteEliminado", clienteId);
        //}

        //public async Task ClienteCreado(ClienteModels nuevoCliente)
        //{
        //    await Clients.All.SendAsync("ClienteCreado", nuevoCliente);
        //}
        public async Task ClienteListar( ClienteModels clientes)
        {
            await Clients.All.SendAsync("ClienteListar", clientes);
        }

    }

}
