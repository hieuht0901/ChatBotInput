using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InputWebApp.Controllers
{
    public class ThuThuHanhChinhController : BaseApiController
    {
        public ThuThuHanhChinhController(IWebHostEnvironment hostingEnvironment) : base(hostingEnvironment)
        {

        }

        [HttpGet("danhsach")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSach()
        {
            var result = await Mediator.Send(new Application.ThuTucHanhChinh.DanhSach.Query());
            return HandlerResult(result);
        }

        [HttpGet("danhsachnhomthutuc")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachNhomThuTuc()
        {
            var result = await Mediator.Send(new Application.ThuTucHanhChinh.DanhSachNhomThuTuc.Query());
            return HandlerResult(result);
        }

        [HttpGet("danhsachnhomthutuccon/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DanhSachNhomThuTucCon(int id)
        {
            var result = await Mediator.Send(new Application.NhomThuTucCon.DanhSachTheoNhom.Query { id = id });
            return HandlerResult(result);
        }

        [HttpPost("themmoinhomthutuccon")]
        public async Task<IActionResult> ThemMoiNhomThuTucCon([FromBody] SubNhomThuTuc nhomThuTuc)
        {
            var result = await Mediator.Send(new Application.NhomThuTucCon.ThemMoi.Command { Entity = nhomThuTuc });
            return HandlerResult(result);
        }

        [HttpPost("capnhatnhomthutuccon/{id}")]
        public async Task<IActionResult> CapNhatNhomThuTucCon([FromBody] SubNhomThuTuc nhomThuTuc, int id)
        {
            var result = await Mediator.Send(new Application.NhomThuTucCon.CapNhat.Command { Entity = nhomThuTuc, Id = id });
            return HandlerResult(result);
        }

        [HttpPost("themmoinhomthutuc")]
        [AllowAnonymous]
        public async Task<IActionResult> ThemMoiNhomThuTuc([FromBody] NhomThuTuc nhomThuTuc)
        {
            var insertResult = await Mediator.Send(new Application.ThuTucHanhChinh.ThemMoiNhomThuTuc.Command { Entity = nhomThuTuc });
            return HandlerResult(insertResult);
        }

        [HttpPost("capnhatnhomthutuc/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> capnhatnhomthutuc([FromBody] NhomThuTuc nhomThuTuc, int id)
        {
            var insertResult = await Mediator.Send(new Application.ThuTucHanhChinh.CapNhatNhomThuTuc.Command { Entity = nhomThuTuc, Id = id });
            return HandlerResult(insertResult);
        }

        [HttpPost("themmoithutuchanhchinh")]
        [AllowAnonymous]
        public async Task<IActionResult> themmoithutuchanhchinh([FromBody] Domain.ThuTucHanhChinh _request)
        {

            var insertResult = await Mediator.Send(new Application.ThuTucHanhChinh.ThemMoiThuTucHanhChinh.Command { Entity = _request });
            return HandlerResult(insertResult);
        }

        [HttpPost("capnhatthutuchanhchinh/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> capnhatthutuchanhchinh([FromBody] Domain.ThuTucHanhChinh _request, int id)
        {

            var insertResult = await Mediator.Send(new Application.ThuTucHanhChinh.CapNhatThuTucHanhChinh.Command { Entity = _request, Id = id });
            return HandlerResult(insertResult);
        }

        [HttpPost("capnhatthutuchanhchinhcompact/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> capnhatthutuchanhchinhcompact([FromBody] ThuTucHanhChinhCompact _request, int id)
        {

            var insertResult = await Mediator.Send(new Application.ThuTucCompact.CapNhat.Command { Entity = _request, Id = id });
            return HandlerResult(insertResult);
        }

        [HttpGet("danhsachcauhoi/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> danhsachcauhoi(int id)
        {
            var lst = await Mediator.Send(new Application.CauHoiApplication.DanhSachTheoThuTuc.Query { id = id });
            return HandlerResult(lst);
        }

        [HttpPost("themoicauhoi")]
        [AllowAnonymous]
        public async Task<IActionResult> themoicauhoi([FromBody] CauHoi _request)
        {
            var lst = await Mediator.Send(new Application.CauHoiApplication.ThemMoi.Command { Entity = _request });
            return HandlerResult(lst);
        }

        [HttpPost("capnhatcauhoi")]
        [AllowAnonymous]
        public async Task<IActionResult> capnhatcauhoi([FromBody] CauHoi _request)
        {
            var updateResult = await Mediator.Send(new Application.CauHoiApplication.CapNhat.Command { Id = _request.Id, NoiDung = _request.NoiDung });

            return HandlerResult(updateResult);
        }

        [HttpDelete("xoacauhoi/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> xoacauhoi(Guid id)
        {
            var updateResult = await Mediator.Send(new Application.CauHoiApplication.Xoa.Command { Id = id });

            return HandlerResult(updateResult);
        }
    }
}
