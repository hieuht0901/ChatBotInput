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
    public class CapNhat
    {
        public class Command : IRequest<Result<CauHoi>>
        {
            public Guid Id { get; set; }
            public string NoiDung { get; set; }
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
                    var tthc = await _context.CauHoi.FindAsync(request.Id);
                    if (tthc == null)
                    {
                        throw new Exception("Khong tim thay doi tuong");
                    }
                    tthc.NoiDung = request.NoiDung;
                    var updateResult = await _context.SaveChangesAsync();
                    if (updateResult <= 0)
                    {
                        throw new Exception("Cap nhat không thành công");
                    }
                    return Result<CauHoi>.Success(tthc);
                }
                catch (Exception ex)
                {
                    return Result<CauHoi>.Failure(ex.Message);
                }
            }
        }
    }
}
