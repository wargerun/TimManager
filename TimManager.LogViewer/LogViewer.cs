using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using TimManager.LogViewer.SearchLog;

namespace TimManager.LogViewer
{
    public class LogViewer
    {
        public async Task<List<LogItem>> ExtractLines(Stream openReadStream, IBuilderLogItem builderLogItem)
        {
            var logItems = new List<LogItem>();

            using (var streamReader = new StreamReader(openReadStream))
            {
                string line;

                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    LogItem logItem = builderLogItem.GetLogItem(line);

                    if (logItem != null)
                    {
                        logItems.Add(logItem);
                    }
                }

                return logItems;
            }
        }
    }
}