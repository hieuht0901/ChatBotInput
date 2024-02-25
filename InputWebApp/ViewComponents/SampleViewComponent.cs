using InputWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InputWebApp.ViewComponents
{
    public class SampleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user = (ClaimsIdentity)User.Identity;
            string uname = user.Name;
            SampleComponentViewModel vm = new SampleComponentViewModel { mgretting = "hello component" };
            if (!string.IsNullOrEmpty(uname))
                return View(vm);
            else
                return View("Empty");
        }
    }
}
