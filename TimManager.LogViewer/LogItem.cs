using System;
using System.Runtime;
using System.Text;

namespace TimManager.LogViewer
{
    public class LogItem
    {
        public DateTime DateTime { get; set; }
        public string ThreadName { get; set; }
        public LogEvent LogEvent { get; set; }
        public string Callsite { get; set; }
        public string Message { get; set; }

        public StringBuilder FullLog { get; } 

        public LogItem(string line)
        {
            FullLog = new StringBuilder();
            FullLog.AppendLine(line);
        }

        public override string ToString() => FullLog.ToString();
    }
}