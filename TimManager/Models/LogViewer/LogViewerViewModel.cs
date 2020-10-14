using System.Collections.Generic;

namespace TimManager.Models.LogViewer
{
    public class LogViewerViewModel
    {
        public ICollection<LogItem> LogItems { get; }

        public LogViewerViewModel(ICollection<LogItem> logItems = null)
        {
            LogItems = logItems ?? new List<LogItem>();
        }
    }
}