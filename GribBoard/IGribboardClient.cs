using System.Collections.Generic;
using System.Threading.Tasks;

namespace GribBoard
{
    public interface IGribboardClient
    {
        Task GribboardEntryAdded(string text, bool autoOpen);
        Task MeAdded(string id, IEnumerable<string> enumerable);
        Task ClientAdded(string id);
        Task ClientRemoved(string id);
    }
}