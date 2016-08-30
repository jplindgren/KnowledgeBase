using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace KnowledgeBase.Controllers {
    public class BaseController : Controller{
        protected Guid GetUserId() {
            if (!Request.IsAuthenticated)
                throw new Exception("No authenticated!");

            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            var userId = claim.Value;
            return Guid.Parse(userId);
        }
    } //class
}