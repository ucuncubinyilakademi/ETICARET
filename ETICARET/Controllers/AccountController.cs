using ETICARET.Identity;
using ETICARET.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETICARET.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Kayıt İşlemleri

                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Surname;
                user.UserName = model.Username;

                var result = UserManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    //Kullanıcı oluştu ve kullanıcıya bir role atayabiliriz.
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }

                    return RedirectToAction("Login");
                }
                else
                {
                    result.Errors.ToList().ForEach(x => ModelState.AddModelError("", x));
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturma hatası");
                }
              
            }
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // Login İşlemleri
                var user = UserManager.Find(model.Username, model.Password);

                if (user != null) 
                {
                    // varolan kullanıcı sisteme dahil et.
                    // ApplicationCookie oluşturup sisteme bırak.

                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe; // Tarayıca tutulsun mu?
                    authManager.SignIn(authProperties, identityclaims);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else{
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "böyle bir kullanıcı yok");
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}