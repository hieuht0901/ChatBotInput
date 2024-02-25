using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InputWebApp.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //var user = (ClaimsIdentity)User.Identity;
            //string uname = user.Name;
            //DropdownUserViewModel vm = new DropdownUserViewModel { Username = uname };
            //if (!string.IsNullOrEmpty(uname))
            //    return View(vm);
            //else
            //    return View("Empty");

            return View();
        }
    }
}
