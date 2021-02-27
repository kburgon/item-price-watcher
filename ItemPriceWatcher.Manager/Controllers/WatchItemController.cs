using System.Diagnostics;
using ItemPriceWatcher.Manager.BLL;
using ItemPriceWatcher.Manager.Models;
using ItemPriceWatcher.Manager.Models.WatchItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItemPriceWatcher.Manager.Controllers
{
    public class WatchItemController : Controller
    {
        private readonly ILogger<WatchItemController> _logger;
        private readonly IWatchItemManager _watchItemManager;

        public WatchItemController(ILogger<WatchItemController> logger, IWatchItemManager manager)
        {
            _logger = logger;
            _watchItemManager = manager;
        }

        public IActionResult Index()
        {
            return View(model: _watchItemManager.GetWatchItems());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}