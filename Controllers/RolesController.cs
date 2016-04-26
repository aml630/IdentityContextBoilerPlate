using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectAuth.ViewModels;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http.Internal;
using ProjectAuth.Models;
using System.Security.Claims;
//using System.Data.Entity;
using Microsoft.Data.Entity;

namespace ProjectAuth.Controllers
{
    public class RolesController : Controller
    {
        //private ApplicationDbContext context = new ApplicationDbContext();

        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            context = db;
        }

        public ActionResult Index()
        {
            var roles = context.Roles.ToList();
            return View(roles);
        }


        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Create(string RoleName)
        {
            try
            {
                    context.Roles.Add(new IdentityRole()
                    {
                        Name = RoleName
                    });
                    context.SaveChanges();
                    ViewBag.ResultMessage = "Role created successfully !";
                    return RedirectToAction("Create");
             }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(IdentityRole thisRole)
        {

            var oldRole = thisRole;

            thisRole.Name = Request.Form["Name"];
            context.Roles.Update(oldRole);
            context.SaveChanges();

                return RedirectToAction("Index");
           
        }
    }
}
