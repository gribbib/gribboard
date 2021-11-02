using System.Threading.Tasks;

namespace GribBoard
{
    public interface IGribboardClient
    {
        Task GribboardEntryAdded(string text, bool autoOpen);
        Task ClientAdded(string id);
        Task ClientRemoved(string id);
    }
}