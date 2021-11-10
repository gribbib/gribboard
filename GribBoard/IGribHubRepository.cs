using System.Collections.Generic;

namespace GribBoard
{
    internal interface IGribHubRepository
    {

        Dictionary<string, string> Clients { get; set; }
    }
}