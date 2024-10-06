using Dapper;
using Domain;
using InputWebApp.DTOs;
using InputWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Claims;

namespace InputWebApp.Controllers
{
    [Authorize]
    public class HomeController : BaseViewController
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
            return View("dsthutuchanhchinh");
        }

        public IActionResult Privacy()
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
            //using (var connettion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            //{
            //    await connettion.OpenAsync();
            //    try
            //    {
            //        DynamicParameters parameters = new DynamicParameters();
            //        parameters.Add("@pId", id);
            //        Domain.ResponseEntities.ChiTietThuTucHanhChinh result = await connettion.QueryFirstOrDefaultAsync<Domain.ResponseEntities.ChiTietThuTucHanhChinh>("spu_ThuTucHanhChinh_ChiTiet", parameters, commandType: System.Data.CommandType.StoredProcedure);

            //        return View(result);
            //    }
            //    catch (Exception ex)
            //    {
            //        return View(null);
            //    }
            //    finally
            //    {
            //        await connettion.CloseAsync();
            //    }
            //}

            var chitietThuTucHanhChinhResult = await Mediator.Send(new Application.ThuTucHanhChinh.ChiTietThuTucHanhChinh.Query { Id = id });
            if (!chitietThuTucHanhChinhResult.IsSuccess)
                return View(new Domain.ResponseEntities.ChiTietThuTucHanhChinh());

            return View(chitietThuTucHanhChinhResult.Value.Clone());
        }

        public IActionResult danhsachnguoidung()
        {
            return View();
        }

        public IActionResult danhsachydinhnguoidung()
        {
            return View();
        }

        public IActionResult danhsachthucthedinhdanh()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> capnhattaikhoan()
        {
            var curUser = (ClaimsIdentity)User.Identity;
            AppUser user = curUser != null && curUser.Name != null ? await _userManager.FindByNameAsync(curUser.Name) : null;
            return View(user);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            var curUser = (ClaimsIdentity)User.Identity;
            var user = curUser != null && curUser.Name != null ? await _userManager.FindByNameAsync(curUser.Name) : null;
            if (user == null)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            //login functionality
            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null)
            {
                ViewData["ErrorMessage"] = $"Không tồn tại tài khoản có tên đăng nhập là {dto.Username}";
                return View();
            }

            if (user != null)
            {
                //sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

                if (signInResult.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "Tài khoản đã bị khóa, xin vui lòng liên hệ quản trị viên để mở khóa tài khoản!";
                    return View();
                }

                if (!signInResult.Succeeded)
                {
                    ViewData["ErrorMessage"] = "Mật khẩu không chính xác, vui lòng thử lại!";
                    return View();
                }

                if (signInResult.Succeeded)
                {
                    //return RedirectToAction("Index", "Home", new { area = "AdminTool" });
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