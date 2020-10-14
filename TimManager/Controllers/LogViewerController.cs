using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimManager.Models.LogViewer;

namespace TimManager.Controllers
{
    public class LogViewerController : Controller
    {
        private readonly LogViewer _logViewer;

        public LogViewerController()
        {
            _logViewer = new LogViewer();
        }
        
        // GET
        public IActionResult Index()
        {
            return View(new LogViewerViewModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile uploadedFile)
        {
            var logItems = new List<LogItem>();

            if (uploadedFile != null)
            {
                await using var openReadStream = uploadedFile.OpenReadStream();
                using (var streamReader = new StreamReader(openReadStream))
                {
                    string line;
                    while ((line = await streamReader.ReadLineAsync()) != null)
                    {
                        LogItem extractLine = _logViewer.ExtractLine(line);
                        logItems.Add(extractLine);
                    }
                        
                }
            }

            return View(new LogViewerViewModel(logItems));
        }
    }
}