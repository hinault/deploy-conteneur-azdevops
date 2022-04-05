using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestApp.MVC.Models;

namespace TestApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IDistributedCache _distributedCache;

        public HomeController(ILogger<HomeController> logger, IDistributedCache distributedCache)
        {
            _logger = logger;

            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> CacheRedis()
        {
            string cacheDatetime = await _distributedCache.GetStringAsync("cacheDateTime");

            if (cacheDatetime == null)
            {
                cacheDatetime = DateTime.Now.ToString();

                var options = new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(10),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120),
                };

                _ = _distributedCache.SetStringAsync("cacheDateTime", cacheDatetime, options);
            }

            ViewBag.CacheDatetime = cacheDatetime;  

            return View();
        }

        public IActionResult GenererException()
        {
            throw new NotImplementedException();

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Message"] = "Condition d'utilisation.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
