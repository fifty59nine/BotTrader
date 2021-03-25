using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotTrader.Controllers
{
    public class DenController : Controller
    {
        // GET: DenController
        public ActionResult Index()
        {
            return View();
        }
    }
}
