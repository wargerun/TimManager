using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TimManager.LogViewer
{
    public class LogViewer
    {
        public async IAsyncEnumerable<LogItem> ExtractLines(Stream openReadStream, string pattern)
        {
            Regex regex = new Regex(pattern);
            using (var streamReader = new StreamReader(openReadStream))
            {
                string line;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    // // stack trace и "--- Конец трассировка стека из предыдущего расположения, где возникло исключение --- "
                    // if (line.StartsWith("   ") || line.StartsWith("---"))
                    // {
                    //     continue;
                    // }
                    LogItem logItem = ExtractLine(line, regex);

                    if (logItem is null)
                    {
                        continue;
                    }
                    
                    yield return logItem;
                }
            }
        }
        
        private static LogItem ExtractLine(string fileLine, Regex regex)
        {
            // (?<DateTime>\d*\/\d*\/\d* \d*:\d*:.*) \[(?<LogEvent>.*)\] \[(?<ThreadName>.*)\] (?<Callsite>.*-) (?<Message>.*)
            Match match = regex.Match(fileLine);
            
            if (!match.Success)
            {
                return null;
            }
            
            LogItem item = new LogItem();
            
            foreach (string groupName in regex.GetGroupNames())
            {
                Group matchGroup = match.Groups[groupName];
                
                switch (groupName)
                {
                    case nameof(LogItem.DateTime):
                    {
                        item.DateTime = getDateTime(matchGroup.Value);
                        break;
                    }
                    case nameof(LogItem.LogEvent):
                    {
                        item.LogEvent = getLogEvent(matchGroup.Value);
                        break;
                    }
                    case nameof(LogItem.ThreadName):
                    {
                        item.ThreadName = matchGroup.Value;
                        break;
                    }
                    case nameof(LogItem.Callsite):
                    {
                        item.Callsite = matchGroup.Value;
                        break;
                    }
                    case nameof(LogItem.Message):
                    {
                        item.Message = matchGroup.Value;
                        break;
                    }
                }
            }

            return item;
        }
        
        private static LogEvent getLogEvent(string matchValue)
        {
            if (!Enum.TryParse(matchValue, out LogEvent result))
            {
                result = LogEvent.Undefined;
            }

            return result;
        }

        private static DateTime getDateTime(string matchValue)
        {
            if (!DateTime.TryParse(matchValue, out DateTime result))
            {
                ;
            }

            return result;
        }
    }
}