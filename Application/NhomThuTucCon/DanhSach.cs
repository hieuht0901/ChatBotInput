using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Persistence;
using Application.Core;
using Domain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Application.NhomThuTucCon
{
    public class DanhSach
    {
        public class Query : IRequest<Result<List<SubNhomThuTuc>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<SubNhomThuTuc>>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<List<SubNhomThuTuc>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var ds = await _context.SubNhomThuTuc.ToListAsync<SubNhomThuTuc>();
                    return Result<List<SubNhomThuTuc>>.Success(ds);

                }
                catch (Exception ex)
                {
                    return Result<List<SubNhomThuTuc>>.Failure(ex.Message);
                }
            }
        }
    }
}
