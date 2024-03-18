using Application.Core;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.YDinhNguoiDung
{
    public class CapNhat
    {
        public class Command : IRequest<Result<Domain.YDinhNguoiDung>>
        {
            public Domain.YDinhNguoiDung Entity { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Domain.YDinhNguoiDung>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }
            public async Task<Result<Domain.YDinhNguoiDung>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var ydinhnguoidung = await _context.YDinhNguoiDung.FindAsync(request.Entity.Id);
                    if (ydinhnguoidung == null)
                    {
                        throw new Exception("Không tìm thấy dữ liệu");
                    }
                    ydinhnguoidung.TieuDe = request.Entity.TieuDe;
                    ydinhnguoidung.NoiDung = request.Entity.NoiDung;
                    ydinhnguoidung.UpdatedDate = DateTime.Now;
                    ydinhnguoidung.KichHoat = request.Entity.KichHoat;
                    int rowUpdated = await _context.SaveChangesAsync();
                    if (rowUpdated <= 0)
                    {
                        throw new Exception("Cập nhật không thành công");
                    }
                    return Result<Domain.YDinhNguoiDung>.Success(ydinhnguoidung);
                }
                catch (Exception ex)
                {
                    return Result<Domain.YDinhNguoiDung>.Failure(ex.Message);
                }
            }
        }
    }
}
