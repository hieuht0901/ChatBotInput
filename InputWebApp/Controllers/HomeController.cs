using Dapper;
using Domain;
using InputWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System.Data.SqlClient;
using System.Diagnostics;

namespace InputWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext context, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult nhomthutuc()
        {
            return View();
        }

        public IActionResult nhomthutuccon()
        {
            return View();
        }

        public IActionResult dsthutuchanhchinh()
        {
            //return View("testgrid");
            return View();
        }
        public async Task<IActionResult> thutuchanhchinh(int id)
        {
            //Domain.ThuTucHanhChinh vm = await _context.ThuTucHanhChinh.FindAsync(id);
            //return View(vm);
            using (var connettion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connettion.OpenAsync();
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@pId", id);
                    Domain.ResponseEntities.ChiTietThuTucHanhChinh result = await connettion.QueryFirstOrDefaultAsync<Domain.ResponseEntities.ChiTietThuTucHanhChinh>("spu_ThuTucHanhChinh_ChiTiet", parameters, commandType: System.Data.CommandType.StoredProcedure);

                    return View(result);
                }
                catch (Exception ex)
                {
                    return View(null);
                }
                finally
                {
                    await connettion.CloseAsync();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //login functionality
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                //sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}