using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReportingTool.DataAccessLayer;
using ReportingTool.DataAccessLayer.EntityFramework;
using ReportingTool.DataAccessLayer.Models;

namespace ReportingTool.UI.Controllers
{
    public class HomeController : Controller
    {
        public ReportingToolDbContext DbContext { get; set; }
        public HomeController(ReportingToolDbContext _dbContext)
        {
            DbContext = _dbContext;
        }
        public IActionResult Index()
        {
                return View();
        }
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties()
            {
                RedirectUri = "/home/index"
            }, "oidc");
        }
        public IActionResult LogOut()
        {
            return SignOut(new AuthenticationProperties()
            {
                RedirectUri = "/home/index"
            }, "Cookies", "oidc");
        }
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
