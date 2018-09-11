using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportingTool.DataAccessLayer.EntityFramework;
using ReportingTool.DataAccessLayer.Models;

namespace ReportingTool.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserInfoController : Controller
    {
        private ReportingToolDbContext _context;
        public UserInfoController(ReportingToolDbContext context)
        {
            _context = context;
        }
        public IEnumerable<string> GetOrganizations(int userId)
        {
            IQueryable<string> organizations;
            using (_context)
            {
                organizations = _context.Users.Where(usr => usr.UserId.Equals(userId))
                    .Select(usr=>usr.Organization.Name);
            }
                return organizations.ToList();
        }
    }
}