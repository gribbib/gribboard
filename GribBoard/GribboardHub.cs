using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GribBoard
{
    internal class GribboardHub : Hub<IGribboardClient>
    {
        private Dictionary<string, string> clients = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var id = "id";
            clients[id] = Context.ConnectionId;
            await Clients.All.ClientAdded(id);
            await base.OnConnectedAsync();
        }

        public async Task AddGribboardEntry(string text, string targetId, bool autoOpen)
        {
            // _logger.LogTrace("Game added");
            if (string.IsNullOrWhiteSpace(targetId))
            {
                await Clients.All.GribboardEntryAdded(text, false);
            }
            else
            {
                await Clients.Client(clients[targetId]).GribboardEntryAdded(text, autoOpen);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var removedId = string.Empty;
            // find and remove client entry
            foreach (var client in clients)
            {
                if (client.Value == Context.ConnectionId)
                {
                    removedId = client.Key;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(removedId))
            {
                return;
            }

            clients.Remove(removedId);

            await Clients.All.ClientRemoved(removedId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}