using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimManager.Common;

namespace TimManager.LogViewer
{
    public class LogItemRepository
    {
        private const string DIR_NAME_UPLOADS = "uploads";
        private readonly string _webRootPath;

        public LogItemRepository(string webRootPath)
        {
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                throw new System.ArgumentException($"'{nameof(webRootPath)}' cannot be null or whitespace", nameof(webRootPath));
            }

            _webRootPath = webRootPath;
        }

        public async Task SaveAsync(string fileName, List<LogItem> logItems)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new System.ArgumentException($"'{nameof(fileName)}' cannot be null or whitespace", nameof(fileName));
            }

            if (logItems.All(l => l.Id == 0))
            {
                logItems.Foreach((log, index) => log.Id = index);
            }

            string jsonContentFile = JsonConvert.SerializeObject(logItems, Formatting.Indented);

            var fullFilename = GetFullFileName(fileName);
            await File.WriteAllTextAsync(fullFilename, jsonContentFile);
        }

        private string GetFullFileName(string inputFileName)
        {
            string uploadsDir = Path.Combine(_webRootPath, DIR_NAME_UPLOADS);

            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            var jsonFileName = Path.GetFileNameWithoutExtension(inputFileName);
            string path = Path.Combine(uploadsDir, $"{jsonFileName}.json");

            return path;
        }
    }
}