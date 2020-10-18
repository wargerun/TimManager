using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimManager.LogViewer;
using TimManager.LogViewer.SearchLog;
using TimManager.Models.LogViewer;

namespace TimManager.Controllers
{
    public class LogViewerController : Controller
    {
        private readonly LogViewer.LogViewer _logViewer;
        private const string REGEXP_PATTERN = "(?<DateTime>\\d*\\/\\d*\\/\\d* \\d*:\\d*:.*) \\[(?<LogEvent>.*)\\] \\[(?<ThreadName>.*)\\] (?<Callsite>.*) - (?<Message>.*)";

        public LogViewerController()
        {
            _logViewer = new LogViewer.LogViewer();
        }
        
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Build(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
                throw new ArgumentNullException(nameof(uploadedFile));
            
            RegexBuilderLogItem regexBuilderLogItem = new RegexBuilderLogItem(REGEXP_PATTERN);
            var openReadStream = uploadedFile.OpenReadStream();
            List<LogItem> logItems = await _logViewer.ExtractLines(openReadStream, regexBuilderLogItem);
            
            return View(new LogViewerViewModel(logItems));
        }
    }
}