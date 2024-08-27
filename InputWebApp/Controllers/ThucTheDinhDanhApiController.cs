using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InputWebApp.Controllers
{
    public class ThucTheDinhDanhApiController : BaseApiController
    {
        public ThucTheDinhDanhApiController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        [HttpGet("danhsach")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSach()
        {
            var result = await Mediator.Send(new Application.ThucTheDinhDanh.DanhSach.Query());
            return HandlerResult(result);
        }

        [HttpPost("themmoi")]
        [AllowAnonymous]
        public async Task<IActionResult> themmoi([FromBody] ThucTheDinhDanh thucthe)
        {
            var result = await Mediator.Send(new Application.ThucTheDinhDanh.ThemMoi.Command { Entity = thucthe });
            return HandlerResult(result);
        }

        [HttpPost("capnhat")]
        [AllowAnonymous]
        public async Task<IActionResult> capnhat([FromBody] ThucTheDinhDanh thucthe)
        {
            var result = await Mediator.Send(new Application.ThucTheDinhDanh.CapNhat.Command { Entity = thucthe });
            return HandlerResult(result);
        }

        [HttpDelete("xoa/{id}")]
        public async Task<IActionResult> xoa(Guid id)
        {
            var result = await Mediator.Send(new Application.ThucTheDinhDanh.Xoa.Command { Id = id });
            return HandlerResult(result);
        }
    }
}
