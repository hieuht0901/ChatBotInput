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
    public class DanhSach
    {
        public class Query : IRequest<Result<List<Domain.ThuTucHanhChinh>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<Domain.ThuTucHanhChinh>>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Result<List<Domain.ThuTucHanhChinh>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ds = await _context.ThuTucHanhChinh.ToListAsync<Domain.ThuTucHanhChinh>();
                return Result<List<Domain.ThuTucHanhChinh>>.Success(ds);
            }
        }
    }
}
