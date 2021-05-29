using Logger.API.Contracts;
using Logger.API.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace Logger.API.Service
{
    public class FileService : IFileService
    {
        private readonly string _filePath;
        private readonly IWebHostEnvironment _hostEnvironment;
        //private readonly ILogger _logger;

        public FileService(IOptions<FileSettings> fileSettings, IWebHostEnvironment hostEnvironment/*, ILogger logger*/)
        {
            _filePath = fileSettings.Value.Path;
            _hostEnvironment = hostEnvironment;
            //_logger = logger;
        }

        public async Task WriteLogsAsync(string content)
        {
            var path = Path.Combine(_hostEnvironment.ContentRootPath, _filePath);
            await using var fs = new FileStream(path, FileMode.Append);
            await using var writer = new StreamWriter(fs);
            //_logger.ForContext(content);
            await writer.WriteLineAsync(content);
        }
    }
}