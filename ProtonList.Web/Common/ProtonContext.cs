using ProtonList.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ProtonList.Web.Common
{
    public class ProtonContext
    {
        public ApplicationUser CurrentUser { get; set; }


        public ProtonContext()
        {
            ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            CurrentUser = manager.FindById(HttpContext.Current.User.Identity.GetUserId());
        }
    }
}