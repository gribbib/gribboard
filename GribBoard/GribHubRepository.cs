using System.Collections.Generic;

namespace GribBoard
{
    internal class GribHubRepository : IGribHubRepository
    {
        public Dictionary<string, string> Clients { get; set; }

        public GribHubRepository()
        {
            Clients = new Dictionary<string, string>();
        }
    }
}