using _2022E_WebApp.Models;
using _2022E_WebApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace _2022E_WebApp.Controllers
{
    public class UserController : Controller
    {
        AppDbContext _dbcontext = new AppDbContext();

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel uvm)
        {
            try
            {
                ViewBag.Error = string.Empty;
                if (_dbcontext.Users.Where(u => u.UserName == uvm.UserName).Any())
                {
                    ViewBag.Error = "User with this username already exists";
                    return View();
                }
                else
                {
                    var _usr = new User()
                    {
                        UserName = uvm.UserName,
                        Email = uvm.Email,
                        PhoneNumber = uvm.PhoneNumber,
                        Password = uvm.Password,
                        CreatedDate = DateTime.UtcNow

                    };

                    _dbcontext.Users.Add(_usr);
                    _dbcontext.SaveChanges();
                    return RedirectToAction(nameof(Login));
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel uvm)
        {
            var user = _dbcontext.Users.
                Where(u=>u.UserName == uvm.UserName).FirstOrDefault();
            if (user != null && user.Password == uvm.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                };
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();


                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );
                return RedirectToAction("Index", "Job");


            }
            else
            {
                //return error message
                ViewBag.Error = "Failed to login. Invalid Username/Password";
                return View();
            }
            
        }
        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
