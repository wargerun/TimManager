using System;
using System.Text.RegularExpressions;

namespace TimManager.LogViewer.SearchLog
{
    public class RegexBuilderLogItem : IBuilderLogItem
    {
        private readonly Regex _regex;
        private LogItem _lastLogItem;
        public RegexBuilderLogItem(string pattern)
        {
            _regex = new Regex(pattern);
            // (?<DateTime>\d*\/\d*\/\d* \d*:\d*:.*) \[(?<LogEvent>.*)\] \[(?<ThreadName>.*)\] (?<Callsite>.*) - (?<Message>.*)
        }
        
        public LogItem GetLogItem(string line)
        {
            Match match = _regex.Match(line);

            if (!match.Success)
            {
                _lastLogItem.FullLog.AppendLine(line);
                return null;
            }
            
            var logItem = new LogItem(line);
            _lastLogItem = logItem; 
            
            foreach (string groupName in _regex.GetGroupNames())
            {
                Group matchGroup = match.Groups[groupName];
                
                switch (groupName)
                {
                    case nameof(LogItem.DateTime):
                        logItem.DateTime = getDateTime(matchGroup.Value);
                        break;
                    case nameof(LogItem.LogEvent):
                        logItem.LogEvent = getLogEvent(matchGroup.Value);
                        break;
                    case nameof(LogItem.ThreadName):
                        logItem.ThreadName = matchGroup.Value;
                        break;
                    case nameof(LogItem.Callsite):
                        logItem.Callsite = matchGroup.Value;
                        break;
                    case nameof(LogItem.Message):
                        logItem.Message = matchGroup.Value;
                        break;
                }
            }

            return logItem;
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