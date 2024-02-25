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

namespace Application.NhomThuTucCon
{
    public class DanhSachTheoNhom
    {
        public class Query : IRequest<Result<IEnumerable<SubNhomThuTuc>>>
        {
            public int id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<SubNhomThuTuc>>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public async Task<Result<IEnumerable<SubNhomThuTuc>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var tthc = await _context.SubNhomThuTuc.Where(c => c.ParentId == request.id || request.id == 0).ToListAsync();
                    if (tthc == null)
                    {
                        throw new Exception("Khong tim thay doi tuong");
                    }
                    return Result<IEnumerable<SubNhomThuTuc>>.Success(tthc);
                }
                catch (Exception ex)
                {
                    return Result<IEnumerable<SubNhomThuTuc>>.Failure(ex.Message);
                }
            }
        }
    }
}
