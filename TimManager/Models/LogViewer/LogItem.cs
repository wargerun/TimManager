using System;

namespace TimManager.Models.LogViewer
{
    public class LogItem
    {
        public DateTime DateTime { get; }
        
        public string ThreadName { get; }
        
        public LogEvent LogEvent { get; }
        
        public string Message { get; }
        
        // public Exception Exception { get; }

        public LogItem(DateTime dateTime, string threadName, LogEvent logEvent, string message)
        {
            DateTime = dateTime;
            ThreadName = threadName;
            LogEvent = logEvent;
            Message = message;
        }
    }
}