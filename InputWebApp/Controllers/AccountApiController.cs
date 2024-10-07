using Domain;
using Domain.RequestDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InputWebApp.Controllers
{
    public class AccountApiController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountApiController(IWebHostEnvironment hostingEnvironment, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration) : base(hostingEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getallusers")]
        public async Task<IActionResult> GetAllUsers(string keyword = "")
        {
            var result = await _userManager.Users.Where(o => (o.UserName.Contains(keyword) || o.DisplayName.Contains(keyword)) && o.UserName != "host").ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("createnewaccount")]
        public async Task<IActionResult> CreateNewAccount([FromBody] CreateAccountDto _request)
        {
            AppUser newUser = new AppUser
            {
                UserName = _request.UserName,
                DisplayName = _request.DisplayName,
                Email = _request.Email,
                LockoutEnabled = false,
                LockoutEnd = new DateTime(2000, 12, 31)
            };

            var existUser = await _userManager.FindByNameAsync(newUser.UserName);

            if (existUser != null)
            {
                return BadRequest($"Đã tồn tại người dùng có tên đăng nhập là {newUser.UserName}");
            }

            existUser = await _userManager.FindByEmailAsync(newUser.Email);

            if (existUser != null)
            {
                return BadRequest($"Đã tồn tại người dùng có email là {newUser.Email}");
            }

            var createResult = await _userManager.CreateAsync(newUser, _request.Password);

            if (!createResult.Succeeded)
            {
                List<string> errors = new List<string>();
                foreach (var error in createResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return BadRequest(string.Join(". ", errors));
            }
            //List<string> lstRoles = _request.Roles.Split(',').ToList();
            //await _userManager.AddToRolesAsync(newUser, lstRoles);

            return Ok();
        }

        [HttpPost]
        [Route("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] CreateAccountDto _request)
        {
            AppUser appUser = await _userManager.FindByNameAsync(_request.UserName);

            if (appUser == null)
            {
                return BadRequest($"Không tìm thấy tài khoản có tên đăng nhập là {_request.UserName}");
            }

            var existUser = await _userManager.FindByEmailAsync(_request.Email);
            if (existUser != null && existUser.UserName != appUser.UserName)
            {
                return BadRequest($"Đã tồn tại người dùng có email là {_request.Email}");
            }


            appUser.DisplayName = _request.DisplayName;
            appUser.Email = _request.Email;
            var updateResult = await _userManager.UpdateAsync(appUser);
            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult);
            }

            return Ok();
        }

        [HttpPost, HttpDelete]
        [Route("deleteaccount/{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var curUser = (ClaimsIdentity)User.Identity;
            var curUsername = curUser.Name;
            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return BadRequest("Không tìm thấy thông tin người dùng");
            }

            if (appUser.UserName == curUsername) {
                return BadRequest("Không thể xóa user hiện tại");
            }

            var createResult = await _userManager.DeleteAsync(appUser);

            if (!createResult.Succeeded)
            {
                List<string> errors = new List<string>();
                foreach (var error in createResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return BadRequest(string.Join(". ", errors));
            }

            return Ok();

        }

        [HttpPost]
        [Route("UpdateAccountInfo")]
        public async Task<IActionResult> UpdateAccountInfo([FromBody] CreateAccountDto _request)
        {
            AppUser appUser = await _userManager.FindByNameAsync(_request.UserName);

            if (appUser == null)
            {
                return BadRequest($"Không tìm thấy tài khoản có tên đăng nhập là {_request.UserName}");
            }

            var existUser = await _userManager.FindByEmailAsync(_request.Email);
            if (existUser != null && existUser.UserName != appUser.UserName)
            {
                return BadRequest($"Đã tồn tại người dùng có email là {_request.Email}");
            }


            appUser.DisplayName = _request.DisplayName;
            appUser.Email = _request.Email;
            var updateResult = await _userManager.UpdateAsync(appUser);
            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult);
            }

            if (!string.IsNullOrEmpty(_request.Password)) {
                var pToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                var updatePasswordResult = await _userManager.ResetPasswordAsync(appUser, pToken, _request.Password);

                if (!updatePasswordResult.Succeeded)
                {
                    List<string> errors = new List<string>();
                    foreach (var error in updatePasswordResult.Errors)
                    {
                        errors.Add(error.Description);
                    }
                    return BadRequest(string.Join(". ", errors));
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("UpdateAccountPassword")]
        public async Task<IActionResult> UpdateAccountPassword([FromBody] CreateAccountDto _request)
        {
            AppUser appUser = await _userManager.FindByNameAsync(_request.UserName);

            if (appUser == null)
            {
                return BadRequest($"Không tìm thấy tài khoản có tên đăng nhập là {_request.UserName}");
            }

            var pToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            var updateResult = await _userManager.ResetPasswordAsync(appUser, pToken, _request.Password);
            if (!updateResult.Succeeded)
            {
                List<string> errors = new List<string>();
                foreach (var error in updateResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return BadRequest(string.Join(". ", errors));
            }

            return Ok();
        }

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto _request)
        {
            AppUser appUser = await _userManager.FindByNameAsync(_request.username);

            if (appUser == null)
            {
                return BadRequest($"Không tìm thấy tài khoản có tên đăng nhập là {_request.username}");
            }
            var updateResult = await _userManager.ChangePasswordAsync(appUser, _request.oldpassword, _request.newpassword);

            if (!updateResult.Succeeded)
            {
                List<string> errors = new List<string>();
                foreach (var error in updateResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return BadRequest(string.Join(". ", errors));
            }

            return Ok();
        }

        [HttpPost]
        [Route("DisableUser/{username}")]
        public async Task<IActionResult> DisableUser(string username)
        {
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return BadRequest($"Không tìm thấy tài khoản có tên đăng nhập là {username}");
            }

            var disableResult = await _userManager.SetLockoutEnabledAsync(appUser, true);
            List<string> errors = new List<string>();

            if (!disableResult.Succeeded)
            {
                foreach (var error in disableResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return BadRequest(string.Join(". ", errors));
            }

            return Ok();
        }

        [HttpPost]
        [Route("ActiveUser/{username}")]
        public async Task<IActionResult> ActiveUser(string username)
        {
            AppUser appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return BadRequest($"Không tìm thấy tài khoản có tên đăng nhập là {username}");
            }

            List<string> errors = new List<string>();

            var disableResult = await _userManager.SetLockoutEnabledAsync(appUser, false);

            if (!disableResult.Succeeded)
            {
                foreach (var error in disableResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return BadRequest(string.Join(". ", errors));
            }

            return Ok();
        }
    }
}
