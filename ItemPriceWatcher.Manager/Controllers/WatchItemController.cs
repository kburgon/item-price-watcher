using System;
using System.Collections.Generic;
using System.Diagnostics;
using ItemPriceWatcher.Manager.BLL;
using ItemPriceWatcher.Manager.Models;
using ItemPriceWatcher.Manager.Models.WatchItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WatchItemData;

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

        [HttpPost]
        public IActionResult AddWatchItem(string itemNameInput, string urlInput, string itemPathInput, string contactFirstNameInput, string contactLastNameInput, string contactEmailInput)
        {
            Console.WriteLine($"Name: {itemNameInput}");
            var watchItem = new WatchItem
            {
                WatchItemName = itemNameInput,
                WebsiteUrl = urlInput,
                ItemPath = itemPathInput,
                Contacts = new HashSet<Contact>()
            };

            var contact = new Contact
            {
                FirstName = contactFirstNameInput,
                Surname = contactLastNameInput,
                Email = contactEmailInput
            };

            _watchItemManager.AddWatchItem(watchItem, contact);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}