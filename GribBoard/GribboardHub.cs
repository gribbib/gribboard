using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GribBoard
{
    internal class GribboardHub : Hub<IGribboardClient>
    {
        private readonly GribHubRepository _repository;

        public GribboardHub(GribHubRepository repository)
        {
            _repository = repository;
        }

        public override async Task OnConnectedAsync()
        {
            var rand = new Random();
            var id = rand.Next().ToString();
            _repository.Clients[id] = Context.ConnectionId;
            await Clients.Others.ClientAdded(id);
            await Clients.Caller.MeAdded(id, _repository.Clients.Select(k => k.Key));
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
                await Clients.Client(_repository.Clients[targetId]).GribboardEntryAdded(text, autoOpen);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var removedId = string.Empty;
            // find and remove client entry
            foreach (var client in _repository.Clients)
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

            _repository.Clients.Remove(removedId);

            await Clients.All.ClientRemoved(removedId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}