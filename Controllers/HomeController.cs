using BotTrader.Models;
using BotTrader.ContextFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BotTrader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("Nick")]
        public IActionResult Nick()
        {
            return View();
        }
        [Route("Show")]
        public User Showuser() => new User() { Login = "Den", Password = "1qazcde3", Balance = 1000, Id = 0};

        [HttpPost]
        public void NickFormat(string name, string psw, string bal)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(psw) || String.IsNullOrEmpty(bal))
            {
                Response.Redirect("/Home/Error");
            }
            else
            {
                using (var db = new DataContext())
                {
                    try
                    {
                        db.Users.Add(
                            new User()
                            {
                                Login = name,
                                Password = psw,
                                Balance = Convert.ToInt32(bal)
                            });
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        Response.Redirect("/Home/Error");
                    }
                }
                Response.Redirect("/Home/Privacy");
            }  
        }
        
        public void Update(string id, string newbalance)
        {
            if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(newbalance))
            {
                Response.Redirect("/Home/Error");
            }
            else
            {
                try
                {
                    using (var db = new DataContext())
                    {
                        var user = db.Users.Where(u => u.Id == Convert.ToInt32(id)).First();
                        user.Balance = Convert.ToInt32(newbalance);
                        db.SaveChanges();
                        Response.Redirect("/Home/Privacy");
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Response.Redirect("/Home/Error");
                }
            }
        }
        public void Delete(int Id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.Users.Where(e => e.Id == Id).First();
                    db.Remove(user);
                    db.SaveChanges();
                    Response.Redirect("/Home/Privacy");
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Response.Redirect("/Home/Error");
            }
        }

        public IActionResult Privacy()
        {
            ViewBag.Users = new DataContext().Users;
            ViewBag.Counted = (new DataContext().Users).Count();
            List<int> ids = new List<int>();
            foreach (var user in ViewBag.Users)
            {
                ids.Add(user.Id);
            }
            ViewBag.Ids = ids;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("Roma")]
        public IActionResult Roma()
        {
            return View();
        }
    }
}
