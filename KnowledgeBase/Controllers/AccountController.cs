using KnowledgeBase.Data;
using KnowledgeBase.Models;
using Microsoft.Owin.Security;
using MyKnowledge.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Controllers {
    public class AccountController : Controller {
        private UserService userService;

        public AccountController(UserService userService) {
            this.userService = userService;
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            SignInStatus result = SignInStatus.Failure;

            var loggedUser = userService.Login(model.Email, model.Password);
            result = loggedUser != null ? SignInStatus.Success : SignInStatus.Failure;
            
            switch (result) {
                case SignInStatus.Success:
                    IdentitySignin(loggedUser, null, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                           DefaultAuthenticationTypes.ExternalCookie);
            return RedirectToLocal(string.Empty);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                var userId = Guid.NewGuid();
                var result = userService.Register(userId, model.Name, model.Email, model.Password);                
                if (result != null) {
                    IdentitySignin(result, null, false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToLocal(string.Empty);
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //private void AddErrors(IdentityResult result) {
        //    foreach (var error in result.Errors) {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Knowledge");
        }


        public void IdentitySignin(CustomUser user, string providerKey = null, bool isPersistent = false) {
            //var claims = new List<Claim>();

            //// create required claims
            //claims.Add(new Claim(ClaimTypes.NameIdentifier, appUserState.UserId));
            //claims.Add(new Claim(ClaimTypes.Name, appUserState.Name));

            //// custom – my serialized AppUserState object
            //claims.Add(new Claim("userState", appUserState.ToString()));

            //var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ExternalCookie);

            //AuthenticationManager.SignIn(new AuthenticationProperties() {
            //    AllowRefresh = true,
            //    IsPersistent = isPersistent,
            //    ExpiresUtc = DateTime.UtcNow.AddDays(7)
            //}, identity);

            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            },
            "ApplicationCookie");

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignIn(new AuthenticationProperties() {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                            DefaultAuthenticationTypes.ExternalCookie);
        }

        private IAuthenticationManager AuthenticationManager {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        //
        // Summary:
        //     Default authentication types values
        public static class DefaultAuthenticationTypes {
            //
            // Summary:
            //     Default value for the main application cookie used by UseSignInCookies
            public const string ApplicationCookie = "ApplicationCookie";
            //
            // Summary:
            //     Default value used by the UseOAuthBearerTokens method
            public const string ExternalBearer = "ExternalBearer";
            //
            // Summary:
            //     Default value used for the ExternalSignInAuthenticationType configured by UseSignInCookies
            public const string ExternalCookie = "ExternalCookie";
            //
            // Summary:
            //     Default value for authentication type used for two factor partial sign in
            public const string TwoFactorCookie = "TwoFactorCookie";
            //
            // Summary:
            //     Default value for authentication type used for two factor remember browser
            public const string TwoFactorRememberBrowserCookie = "TwoFactorRememberBrowser";
        }
    } //class


    //
    // Summary:
    //     Possible results from a sign in attempt
    public enum SignInStatus {
        //
        // Summary:
        //     Sign in was successful
        Success = 0,
        //
        // Summary:
        //     User is locked out
        LockedOut = 1,
        //
        // Summary:
        //     Sign in requires addition verification (i.e. two factor)
        RequiresVerification = 2,
        //
        // Summary:
        //     Sign in failed
        Failure = 3
    }
}