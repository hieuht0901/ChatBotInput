using Application.Core;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ThucTheDinhDanh
{
    public class CapNhat
    {
        public class Command : IRequest<Result<Domain.ThucTheDinhDanh>>
        {
            public Domain.ThucTheDinhDanh Entity { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<Domain.ThucTheDinhDanh>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }
            public async Task<Result<Domain.ThucTheDinhDanh>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var thucthe = await _context.ThucTheDinhDanh.FindAsync(request.Entity.Id);
                    if (thucthe == null)
                    {
                        throw new Exception("Không tìm thấy dữ liệu");
                    }
                    thucthe.TenThucThe = request.Entity.TenThucThe;
                    thucthe.MoTa = request.Entity.MoTa;
                    thucthe.GhiChu = request.Entity.GhiChu;
                    thucthe.SystemId = request.Entity.SystemId;
                    thucthe.TuKhoa = request.Entity.TuKhoa;
                    thucthe.TraLoi = request.Entity.TraLoi;
                    thucthe.UpdatedDate = DateTime.Now;
                    thucthe.KichHoat = request.Entity.KichHoat;
                    int rowUpdated = await _context.SaveChangesAsync();
                    if (rowUpdated <= 0)
                    {
                        throw new Exception("Cập nhật không thành công");
                    }
                    return Result<Domain.ThucTheDinhDanh>.Success(thucthe);
                }
                catch (Exception ex)
                {
                    return Result<Domain.ThucTheDinhDanh>.Failure(ex.Message);
                }
            }
        }
    }
}
