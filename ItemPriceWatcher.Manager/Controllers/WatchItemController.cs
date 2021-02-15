using System.Diagnostics;
using ItemPriceWatcher.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItemPriceWatcher.Manager.Controllers
{
    public class WatchItemController : Controller
    {
        private readonly ILogger<WatchItemController> _logger;

        public WatchItemController(ILogger<WatchItemController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}