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
    public class CapNhatThuTucHanhChinh
    {
        public class Command : IRequest<Result<Domain.ThuTucHanhChinh>>
        {
            public int Id { get; set; }
            public Domain.ThuTucHanhChinh Entity { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Domain.ThuTucHanhChinh>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<Domain.ThuTucHanhChinh>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var tthc = await _context.ThuTucHanhChinh.FindAsync(request.Id);
                    if (tthc == null)
                    {
                        throw new Exception("Khong tim thay doi tuong");
                    }
                    tthc.IdNhomThuTuc = request.Entity.IdNhomThuTuc;
                    tthc.Idx = request.Entity.Idx;
                    tthc.TenThuTuc = request.Entity.TenThuTuc;
                    tthc.Response = request.Entity.Response;
                    tthc.ShortResponse = request.Entity.ShortResponse;
                    tthc.LinkResponse = request.Entity.LinkResponse;
                    tthc.SubId = request.Entity.SubId;
                    var updateResult = await _context.SaveChangesAsync();
                    if (updateResult <= 0)
                    {
                        throw new Exception("Cap nhat không thành công");
                    }
                    return Result<Domain.ThuTucHanhChinh>.Success(tthc);
                }
                catch(Exception ex)
                {
                    return Result<Domain.ThuTucHanhChinh>.Failure(ex.Message);
                }
            }
        }
    }
}
