using System.Threading.Tasks;

namespace Logger.API.Contracts
{
    public interface IFileService
    {
        Task WriteLogsAsync(string content);
    }
}
