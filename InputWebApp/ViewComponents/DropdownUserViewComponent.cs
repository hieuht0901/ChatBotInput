using InputWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InputWebApp.ViewComponents
{
    public class DropdownUserViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user = (ClaimsIdentity)User.Identity;
            string uname = user.Name;
            if (string.IsNullOrEmpty(uname))
            {
                uname = "test";
            }
            DropdownUserViewModel vm = new DropdownUserViewModel { Username = uname };
            return View(vm);

            //if (!string.IsNullOrEmpty(uname))
            //    return View(vm);
            //else
            //    return View("Empty");

            //return View();
        }
    }
}
