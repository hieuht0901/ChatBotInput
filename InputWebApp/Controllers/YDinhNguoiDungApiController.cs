using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InputWebApp.Controllers
{
    public class YDinhNguoiDungApiController : BaseApiController
    {
        public YDinhNguoiDungApiController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {
            
        }

        [HttpGet("danhsach")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSach()
        {
            var result = await Mediator.Send(new Application.YDinhNguoiDung.DanhSach.Query());
            return HandlerResult(result);
        }

        [HttpPost("themmoi")]
        [AllowAnonymous]
        public async Task<IActionResult> themmoi([FromBody] YDinhNguoiDung yDinhNguoiDung)
        {
            var result = await Mediator.Send(new Application.YDinhNguoiDung.ThemMoi.Command { Entity = yDinhNguoiDung });
            return HandlerResult(result);
        }

        [HttpPut("capnhat")]
        [AllowAnonymous]
        public async Task<IActionResult> capnhat([FromBody] YDinhNguoiDung yDinhNguoiDung)
        {
            var result = await Mediator.Send(new Application.YDinhNguoiDung.CapNhat.Command { Entity = yDinhNguoiDung });
            return HandlerResult(result);
        }

        [HttpDelete("xoa/{id}")]
        public async Task<IActionResult> xoa(Guid id)
        {
            var result = await Mediator.Send(new Application.YDinhNguoiDung.Xoa.Command { Id = id });
            return HandlerResult(result);
        }
    }
}
