using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CauHoiApplication
{
    public class DanhSachTheoThuTuc
    {
        public class Query : IRequest<Result<IEnumerable<CauHoi>>>
        {
            public int id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<IEnumerable<CauHoi>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public async Task<Result<IEnumerable<CauHoi>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var tthc = await _context.CauHoi.Where(c => c.IdThuTuc == request.id).ToListAsync();
                    if (tthc == null)
                    {
                        throw new Exception("Khong tim thay doi tuong");
                    }
                    return Result<IEnumerable<CauHoi>>.Success(tthc);
                }
                catch (Exception ex)
                {
                    return Result<IEnumerable<CauHoi>>.Failure(ex.Message);
                }
            }
        }
    }
}
