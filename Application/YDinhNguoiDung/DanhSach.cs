using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.YDinhNguoiDung
{
    public class DanhSach
    {
        public class Query : IRequest<Result<List<Domain.YDinhNguoiDung>>> { }
        public class Handler : IRequestHandler<Query, Result<List<Domain.YDinhNguoiDung>>>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;

            public Handler(DataContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }
            public async Task<Result<List<Domain.YDinhNguoiDung>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var ds = await _context.YDinhNguoiDung.ToListAsync<Domain.YDinhNguoiDung>();
                return Result<List<Domain.YDinhNguoiDung>>.Success(ds);
            }
        }
    }
}
