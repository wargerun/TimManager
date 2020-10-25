using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TimManager.LogViewer;
using TimManager.LogViewer.SearchLog;
using TimManager.Models.LogViewer;

namespace TimManager.Controllers
{
    public class LogViewerController : Controller
    {
        private readonly LogViewer.LogViewer _logViewer;
        private readonly LogItemRepository _logItemRepository;
        private const string REGEXP_PATTERN = "(?<DateTime>\\d*\\/\\d*\\/\\d* \\d*:\\d*:.*) \\[(?<LogEvent>.*)\\] \\[(?<ThreadName>.*)\\] (?<Callsite>.*) - (?<Message>.*)";

        public LogViewerController(IWebHostEnvironment environment)
        {
            _logViewer = new LogViewer.LogViewer();
            _logItemRepository = new LogItemRepository(environment.WebRootPath);
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

            await _logItemRepository.SaveAsync(uploadedFile.FileName, logItems);

            return View(new LogViewerViewModel(logItems));
        }
    }
}