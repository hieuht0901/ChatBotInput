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

namespace Application.ThuTucHanhChinh
{
    public class DanhSachNhomThuTuc
    {
        public class Query : IRequest<Result<List<NhomThuTuc>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<NhomThuTuc>>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<List<NhomThuTuc>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ds = await _context.NhomThuTuc.ToListAsync<NhomThuTuc>();
                return Result<List<NhomThuTuc>>.Success(ds);
            }
        }
    }
}
