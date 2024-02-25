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


namespace Application.CauHoiApplication
{
    public class ThemMoi
    {
        public class Command : IRequest<Result<CauHoi>>
        {
            public CauHoi Entity { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<CauHoi>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<CauHoi>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await _context.CauHoi.AddAsync(request.Entity);
                    var resultInsert = await _context.SaveChangesAsync();
                    if (resultInsert <= 0)
                    {
                        throw new Exception("Thêm mới không thành công");
                    }
                    return Result<CauHoi>.Success(request.Entity);
                }
                catch (Exception ex)
                {
                    return Result<CauHoi>.Failure(ex.Message);
                }
            }
        }
    }
}
