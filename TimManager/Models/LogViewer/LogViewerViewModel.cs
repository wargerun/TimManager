using System.Collections.Generic;
using System.Linq;
using TimManager.LogViewer;

namespace TimManager.Models.LogViewer
{
    public class LogViewerViewModel
    {
        public ICollection<LogItem> LogItems { get; }

        public int LogItemsCount => LogItems.Count;
        
        public int LogItemsUndefinedCount => LogItems.Count(l => l.LogEvent == LogEvent.Undefined);
        public int LogItemsTraceCount => LogItems.Count(l => l.LogEvent == LogEvent.Trace);
        public int LogItemsDebugCount => LogItems.Count(l => l.LogEvent == LogEvent.Debug);
        public int LogItemsInfoCount => LogItems.Count(l => l.LogEvent == LogEvent.Info);
        public int LogItemsWarnCount => LogItems.Count(l => l.LogEvent == LogEvent.Warn);
        public int LogItemsErrorCount => LogItems.Count(l => l.LogEvent == LogEvent.Error);
        public int LogItemsFatalCount => LogItems.Count(l => l.LogEvent == LogEvent.Fatal);

        public LogViewerViewModel(ICollection<LogItem> logItems = null)
        {
            LogItems = logItems ?? new List<LogItem>();
            
        }
    }
}