using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security;
using Owin.Security.Providers.GitHub;
using Microsoft.Owin.Security.Cookies;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Helpers;
using KnowledgeBase.Controllers;

[assembly: OwinStartup(typeof(KnowledgeBase.Startup))]

namespace KnowledgeBase {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            //app.SetDefaultSignInAsAuthenticationType("ExternalCookie");
            //app.UseCookieAuthentication(new CookieAuthenticationOptions {
            //    AuthenticationType = "ExternalCookie",
            //    AuthenticationMode = AuthenticationMode.Passive,
            //    CookieName = ".AspNet.ExternalCookie",
            //    ExpireTimeSpan = TimeSpan.FromMinutes(5),
            //});

            app.SetDefaultSignInAsAuthenticationType("ApplicationCookie");
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/account/login")
            });

            //var options = new GitHubAuthenticationOptions {
            //    ClientId = "7e59121ce4475cdbd8b9",
            //    ClientSecret = "9478030995a19ff41971bad3c416fe1bd465e39b",
            //    Provider = new GitHubAuthenticationProvider {
            //        OnAuthenticated = context =>
            //        {
            //            context.Identity.AddClaim(new Claim("urn:token:github", context.AccessToken));

            //            return Task.FromResult(true);
            //        }
            //    }
            //};
            //app.UseGitHubAuthentication(options);


            app.UseGitHubAuthentication(
                clientId: "7e59121ce4475cdbd8b9",
                clientSecret: "9478030995a19ff41971bad3c416fe1bd465e39b");
            

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    } //class
}
