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
    public class ThemMoiThuTucHanhChinh
    {
        public class Command : IRequest<Result<Domain.ThuTucHanhChinh>>
        {
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
                    await _context.ThuTucHanhChinh.AddAsync(request.Entity);
                    var resultInsert = await _context.SaveChangesAsync();
                    if (resultInsert <= 0)
                    {
                        throw new Exception("Thêm mới không thành công");
                    }
                    return Result<Domain.ThuTucHanhChinh>.Success(request.Entity);
                }
                catch(Exception ex)
                {
                    return Result<Domain.ThuTucHanhChinh>.Failure(ex.Message);
                }
            }
        }
    }
}
