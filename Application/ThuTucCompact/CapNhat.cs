using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Dapper;
using Persistence;
using Application.Core;
using Domain;

namespace Application.ThuTucCompact
{
    public class CapNhat
    {
        public class Command : IRequest<Result<int>>
        {
            public int Id { get; set; }
            public ThuTucHanhChinhCompact Entity { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var tthc = await _context.ThuTucHanhChinhCompact.FindAsync(request.Id);
                    if (tthc == null)
                    {
                        throw new Exception("Khong tim thay doi tuong");
                    }
                    tthc.DiaDiemTiepNhanShort = request.Entity.DiaDiemTiepNhanShort;
                    tthc.ThoiGianTiepNhanShort = request.Entity.ThoiGianTiepNhanShort;
                    tthc.TrinhTuThucHienShort = request.Entity.TrinhTuThucHienShort;
                    tthc.CachThucThucHienShort = request.Entity.CachThucThucHienShort;
                    tthc.ThoiGianGiaiQuyetShort = request.Entity.ThoiGianGiaiQuyetShort;
                    tthc.LePhiShort = request.Entity.LePhiShort;
                    tthc.YeuCauShort = request.Entity.YeuCauShort;
                    var updateResult = await _context.SaveChangesAsync();
                    if (updateResult <= 0)
                    {
                        throw new Exception("Cap nhat không thành công");
                    }
                    return Result<int>.Success(updateResult);
                }
                catch (Exception ex)
                {
                    return Result<int>.Failure(ex.Message);
                }
            }
        }
    }
}
