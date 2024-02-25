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

namespace Application.ThuTucHanhChinh
{
    public class ChiTiet
    {
        public class Query : IRequest<Result<Domain.ThuTucHanhChinh>>
        {
            public int id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Domain.ThuTucHanhChinh>>
        {
            private readonly IConfiguration _configuration;
            private readonly DataContext _context;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;

            }

            public async Task<Result<Domain.ThuTucHanhChinh>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var tthc = await _context.ThuTucHanhChinh.FindAsync(request.id);
                    if (tthc == null)
                    {
                        throw new Exception("Khong tim thay doi tuong");
                    }
                    return Result<Domain.ThuTucHanhChinh>.Success(tthc);
                }
                catch (Exception ex)
                {
                    return Result<Domain.ThuTucHanhChinh>.Failure(ex.Message);
                }
            }
        }
    }
}
