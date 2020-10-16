using System;

namespace TimManager.LogViewer
{
    public class LogItem
    {
        public DateTime DateTime { get; set; }
        public string ThreadName { get; set; }
        public LogEvent LogEvent { get; set; }
        public string Callsite { get; set; }
        public string Message { get; set; }
        
        // public Exception Exception { get; }

    }
}