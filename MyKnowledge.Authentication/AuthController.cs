using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyKnowledge.Authentication {
    public class AuthController : Controller {
        //private readonly IAuthorizationManager manager;

        //// Manager being injected by Unity.
        //// container.RegisterType<IAuthorizationManager, AuthorizationManager>();
        //public AuthController(IAuthorizationManager manager) {
        //    this.manager = manager;
        //}

        //// Receives a LogInViewModel with all data needed to allow users to log in
        //[HttpPost]
        //public async Task<ActionResult> LogIn(LogInViewModel viewModel) {
        //    // FindAsync it's a method inherited from UserManager, that's
        //    // using the Mongo repository passed to the base class 
        //    // in the AuthorizationManager constructor
        //    var user = this.manager.FindAsync(viewModel.Email, viewModel.Password);

        //    if (user != null) { 
        //        // Log in user and create user session }
        //    } else { // Wrong username or password }

        //    }
        //}
    }//class
}
