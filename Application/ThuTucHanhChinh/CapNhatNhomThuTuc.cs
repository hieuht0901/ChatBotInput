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

namespace Application.ThuTucHanhChinh
{
    public class CapNhatNhomThuTuc
    {
        public class Command : IRequest<Result<NhomThuTuc>>
        {
            public int Id { get; set; }
            public NhomThuTuc Entity { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<NhomThuTuc>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<NhomThuTuc>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var tthc = await _context.NhomThuTuc.FindAsync(request.Id);
                    if (tthc == null)
                    {
                        throw new Exception("Khong tim thay doi tuong");
                    }
                    tthc.MaNhom = request.Entity.MaNhom;
                    tthc.MoTa = request.Entity.MoTa;
                    var updateResult = await _context.SaveChangesAsync();
                    if (updateResult <= 0)
                    {
                        throw new Exception("Cap nhat không thành công");
                    }
                    return Result<NhomThuTuc>.Success(tthc);
                }
                catch (Exception ex)
                {
                    return Result<NhomThuTuc>.Failure(ex.Message);
                }
            }
        }
    }
}
